namespace RpgBooks.Modules.Identity.Application.Commands.ChangePassword;

using RpgBooks.Libraries.Module.Application.Commands;
using RpgBooks.Libraries.Module.Application.Commands.Extensions;
using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using System.Threading;
using System.Threading.Tasks;

internal sealed class ChangePasswordCommandHandler : BaseCommandHandler<ChangePasswordCommand>
{
    private readonly IUserDomainRepository userRepository;
    private readonly IPasswordHasher passwordHasher;
    private readonly ICurrentUser currentUser;

    public ChangePasswordCommandHandler(
        IUserDomainRepository userRepository,
        ICurrentUserService currentUserService,
        IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.currentUser = currentUserService.User!;
    }

    public override async Task<IAppResult> HandleCommand(ChangePasswordCommand request, CancellationToken cancellation)
    {
        var user = await this.userRepository.GetAsync(this.currentUser.Id, cancellation);
        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        bool isPasswordValid = this.passwordHasher.VerifyPassword(request.OldPassword, user.PasswordHash);
        if (!isPasswordValid)
        {
            return this.ValidationFailed(Messages.InvalidPassword);
        }

        user.SetPasswordHash(this.passwordHasher.HashPassword(request.Password));
        await this.userRepository.SaveAsync(cancellation);

        return this.Success(Messages.PasswordChanged);
    }
}
