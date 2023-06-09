namespace RpgBooks.Modules.Identity.Application.Commands.RefreshToken;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;

/// <summary>
/// Refresh authentication token command request.
/// </summary>
/// <param name="AuthenticationToken">Authentication JWT.</param>
/// <param name="RefreshToken">Refresh token value.</param>
public sealed record RefreshTokenCommand(string AuthenticationToken, string RefreshToken) : ICommand;
