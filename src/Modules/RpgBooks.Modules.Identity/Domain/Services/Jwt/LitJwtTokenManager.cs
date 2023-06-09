namespace RpgBooks.Modules.Identity.Domain.Services.Jwt;

using Cysharp.Text;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Settings;
using RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

using LitJWT;
using LitJWT.Algorithms;

using Microsoft.Extensions.Options;

using System.Security.Claims;
using System.Text;

using SecurityClaim = System.Security.Claims.Claim;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// JWT token manager that uses LitJWT library.
/// </summary>
internal sealed class LitJwtTokenManager : IJwtTokenManager, IJwtDecoder
{
    private readonly JwtEncoder encoder;
    private readonly JwtDecoder decoder;

    private readonly int tokenTimeSpanInMinutes;
    private readonly string audience;
    private readonly string issuer;

    public LitJwtTokenManager(IOptions<LoginSettings> options, IOptions<ApplicationSecrets> secretsOptions)
    {
        this.encoder = new JwtEncoder(new HS256Algorithm(secretsOptions.Value.GetAuthenticationSecurityKey()));
        this.decoder = new JwtDecoder(this.encoder.SignAlgorithm);

        this.tokenTimeSpanInMinutes = options.Value.AuthTokenTimeSpanInMinutes;
        this.audience = options.Value.ValidAudience;
        this.issuer = options.Value.ValidIssuer;
    }

    public LitJwtTokenManager(byte[] secret, string audience, string issuer, int tokenTimeSpanInMinutes)
    {
        this.encoder = new JwtEncoder(new HS256Algorithm(secret));
        this.decoder = new JwtDecoder(this.encoder.SignAlgorithm);

        this.tokenTimeSpanInMinutes = tokenTimeSpanInMinutes;
        this.audience = audience;
        this.issuer = issuer;
    }

    /// <inheritdoc/>
    public string GenerateToken(User user, string? sessionId = null)
    {
        var expirationTime = DateTimeOffset.UtcNow.AddMinutes(this.tokenTimeSpanInMinutes);
        var payload = new JwtPayload
        {
            JwtId = Ulid.NewUlid().ToString(),
            SessionId = sessionId ?? Ulid.NewUlid().ToString(),
            SecurityStamp = user.SecurityStamp,
            Audience = this.audience,
            Issuer = this.issuer,
            Email = user.Email,
            EmailVerified = user.EmailConfirmed,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Title = user.HonorificTitle,
            Uid = user.Id.ToString(),
            PhoneNumberVerified = user.PhoneNumberConfirmed,
            Roles = user.Roles.Select(r => r.Name).ToArray(),
            NotBefore = EpochTime.GetIntDate(DateTimeOffset.UtcNow.DateTime),
            IssuedAt = EpochTime.GetIntDate(DateTimeOffset.UtcNow.DateTime),
            CustomClaims = new Dictionary<string, string?>(),
        };

        foreach (var claim in user.Claims)
        {
            payload.CustomClaims.Add(claim.Type, claim.Value);
        }

        return this.encoder.Encode(payload, TimeSpan.FromMinutes(this.tokenTimeSpanInMinutes));
    }

    /// <inheritdoc/>
    public JwtPayload? ReadToken(string token)
    {
        var result = this.decoder.TryDecode(token, out JwtPayload payload);
        if (result == DecodeResult.Success || result == DecodeResult.FailedVerifyExpire)
        {
            return payload;
        }

        return null;
    }

    /// <summary>
    /// Reads the token and returns a <see cref="ClaimsPrincipal"/> with the claims.
    /// </summary>
    /// <param name="token">Token value.</param>
    /// <returns><see cref="ClaimsPrincipal"/> containing the user claims.</returns>
    public ClaimsPrincipal? Decode(string token)
    {
        var payload = this.ReadToken(token);
        if (payload is null)
        {
            return null;
        }

        var claims = new List<SecurityClaim>
        {
            new SecurityClaim(UserClaimTypes.UId, payload.Uid),
            new SecurityClaim(UserClaimTypes.Email, payload.Email),
            new SecurityClaim(UserClaimTypes.JwtId, payload.JwtId)
        };

        if (payload.SessionId is not null)
        {
            claims.Add(new SecurityClaim(UserClaimTypes.SessionId, payload.SessionId));
        }

        if (payload.SecurityStamp is not null)
        {
            claims.Add(new SecurityClaim(UserClaimTypes.SecurityStamp, payload.SecurityStamp));
        }

        if (payload.FirstName is not null)
        {
            claims.Add(new SecurityClaim(UserClaimTypes.FirstName, payload.FirstName));
        }

        if (payload.LastName is not null)
        {
            claims.Add(new SecurityClaim(UserClaimTypes.LastName, payload.LastName));
        }

        if(payload.FirstName is not null && payload.LastName is not null)
        {
            claims.Add(new SecurityClaim(UserClaimTypes.FullName, ZString.Format("{0}{1}{2}", payload.FirstName, payload.MiddleName ?? " ", payload.LastName)));
        }

        if (payload.Roles is not null)
        {
            foreach (var role in payload.Roles)
            {
                claims.Add(new SecurityClaim(UserClaimTypes.Roles, role));
            }
        }

        if (payload.CustomClaims is not null)
        {
            foreach (var claim in payload.CustomClaims)
            {
                claims.Add(new SecurityClaim(claim.Key, claim.Value ?? string.Empty));
            }
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims));
    }
}
