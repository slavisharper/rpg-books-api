namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using RpgBooks.Modules.Identity.Domain.Entities;

public interface ISecurityTokensService
{
    ValueTask<TokenModel> GenerateEmailConfirmationToken(User user, CancellationToken cancellation = default);

    ValueTask<TokenModel> GeneratePhoneConfirmationToken(User user, CancellationToken cancellation = default);

    ValueTask<TokenModel> GenerateResetPasswordToken(User user, CancellationToken cancellation = default);

    ValueTask<TokenModel> GenerateRefreshToken(User user, CancellationToken cancellation = default);
}
