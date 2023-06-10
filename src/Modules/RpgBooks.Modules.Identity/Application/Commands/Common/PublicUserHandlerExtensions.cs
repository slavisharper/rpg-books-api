namespace RpgBooks.Modules.Identity.Application.Commands.Common;

using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Entities;

internal static class PublicUserHandlerExtensions
{
    public static IAppResult<TResponseData>? ValidateUser<TCommand, TResponseData>(
        this ICommandHandler<TCommand, IAppResult<TResponseData>> handler, User user, string? securityStamp = null)
        where TCommand : ICommand
    {
        if (user.Blocked)
        {
            return handler.ValidationFailed(Messages.AccountBlocked);
        }

        if (user.LockedOut)
        {
            return handler.ValidationFailed(ZString.Format(Messages.LockedOut, user.LockedPeriodInMinutes));
        }

        if (securityStamp is not null && user.SecurityStamp != securityStamp)
        {
            return handler.ValidationFailed(Messages.AuthorityModifiedFailure);
        }

        return null;
    }

    public static IAppResult? ValidateUser<TCommand>(
        this ICommandHandler<TCommand, IAppResult> handler, User user, string? securityStamp = null)
        where TCommand : ICommand
    {
        if (user.Blocked)
        {
            return handler.ValidationFailed(Messages.AccountBlocked);
        }

        if (user.LockedOut)
        {
            return handler.ValidationFailed(ZString.Format(Messages.LockedOut, user.LockedPeriodInMinutes));
        }

        if (securityStamp is not null && user.SecurityStamp != securityStamp)
        {
            return handler.ValidationFailed(Messages.AuthorityModifiedFailure);
        }

        return null;
    }
}
