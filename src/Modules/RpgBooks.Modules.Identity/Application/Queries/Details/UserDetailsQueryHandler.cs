namespace RpgBooks.Modules.Identity.Application.Queries.Details;

using RpgBooks.Modules.Identity.Application.Repositories.User;
using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
using RpgBooks.Modules.Identity.Application.Resources;

internal sealed class UserDetailsQueryHandler : IResultQueryHandler<UserDetailsQuery, UserDetailsReadModel>
{
    private readonly ICurrentUser? currentUser;
    private readonly IUserReadOnlyRepository userReadOnlyRepository;

    public UserDetailsQueryHandler(
        ICurrentUserService currentUserService,
        IUserReadOnlyRepository userReadOnlyRepository)
    {
        this.currentUser = currentUserService.User;
        this.userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<IAppResult<UserDetailsReadModel>> Handle(UserDetailsQuery query, CancellationToken cancellation)
    {
        var user = await this.userReadOnlyRepository.GetDetails(this.currentUser!.Id, cancellation);
        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        return this.Success(Messages.UserDetailsFetched, user);
    }
}
