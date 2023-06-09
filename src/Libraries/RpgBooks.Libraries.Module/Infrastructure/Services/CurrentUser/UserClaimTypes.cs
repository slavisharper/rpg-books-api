﻿namespace RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

/// <summary>
/// User claim types used in the application and the JWT tokens.
/// </summary>
public static class UserClaimTypes
{
    /// <summary>
    /// Unique JWT identifier.
    /// <para>Can be used to prevent the JWT from being replayed (allows a token to be used only once)</para>
    /// </summary>
    public const string JwtId = "jti";

    /// <summary>
    /// Issuer claim type.
    /// </summary>
    public const string Issuer = "iss";

    /// <summary>
    /// Audience claim type.
    /// </summary>
    public const string Audience = "aud";

    /// <summary>
    /// Issued at claim type.
    /// </summary>
    public const string IssuedAt = "iat";

    /// <summary>
    /// Expiration claim type.
    /// </summary>
    public const string Expiration = "exp";

    /// <summary>
    /// Not before claim type.
    /// </summary>
    public const string NotBefore = "nbf";

    /// <summary>
    /// Session id claim type.
    /// <para>This is useful to track uses session across multiple authentication tokens.</para>
    /// <para>JWT tokens will have same session id if they are generated by a refresh token.</para>
    /// </summary>
    public const string SessionId = "sid";

    /// <summary>
    /// Security stamp claim type.
    /// <para>The security stamp is used as an additional security measure to invalidate tokens when certain conditions, such as a change in user credentials or permissions, occur.</para>
    /// </summary>
    public const string SecurityStamp = "sts";

    /// <summary>
    /// Unique user identifier claim type.
    /// </summary>
    public const string UId = "uid";

    /// <summary>
    /// Title claim type.
    /// </summary>
    public const string Title = "title";

    /// <summary>
    /// First name claim type.
    /// </summary>
    public const string FirstName = "given_name";

    /// <summary>
    /// Last name claim type.
    /// </summary>
    public const string LastName = "family_name";

    /// <summary>
    /// Middle name claim type.
    /// </summary>
    public const string MiddleName = "middle_name";

    /// <summary>
    /// Full name claim type.
    /// </summary>
    public const string FullName = "name";

    /// <summary>
    /// Nick name claim type.
    /// </summary>
    public const string NickName = "nickname";

    /// <summary>
    /// Email claim type.
    /// </summary>
    public const string Email = "email";

    /// <summary>
    /// Email verified claim type.
    /// </summary>
    public const string EmailVerified = "email_verified";

    /// <summary>
    /// Phone number claim type.
    /// </summary>
    public const string PhoneNumber = "phone_number";

    /// <summary>
    /// Phone number verified claim type.
    /// </summary>
    public const string PhoneNumberVerified = "phone_number_verified";
    
    /// <summary>
    /// Role claim type.
    /// </summary>
    public const string Role = "role";

    /// <summary>
    /// Roles claim type.
    /// </summary>
    public const string Roles = "roles";
    
    /// <summary>
    /// Custom claims claim type.
    /// </summary>
    public const string CustomClaims = "custom_claims";
}
