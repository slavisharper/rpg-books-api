namespace RpgBooks.Modules.Identity.Application.Commands.Login;

using RpgBooks.Modules.Identity.Domain.Services;

/// <summary>
/// Login response model.
/// </summary>
/// <param name="AuthenticationToken">Authentication JWT.</param>
/// <param name="RefreshToken">Refresh token.</param>
public sealed record LoginResponseModel(string AuthenticationToken, TokenModel RefreshToken);