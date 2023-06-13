namespace RpgBooks.Modules.Identity.Application.Commands.UpdateDetails;

using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;

internal sealed class UpdateDetailsComandHandler : BaseCommandHandler<UpdateDetailsComand>
{
    private readonly IUserDomainRepository userDomainRepository;
    private readonly ICurrentUser currentUser;

    public UpdateDetailsComandHandler(IUserDomainRepository userDomainRepository, ICurrentUserService currentUserService)
    {
        this.userDomainRepository = userDomainRepository;
        this.currentUser = currentUserService.User!;
    }

    public override async Task<IAppResult> HandleCommand(UpdateDetailsComand request, CancellationToken cancellation)
    {
        var user = await this.userDomainRepository.GetAsync(this.currentUser.Id, cancellation);
        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        user.SetUserTitle(request.HonorificTitle)
            .SetFirstName(request.FirstName)
            .SetLastName(request.LastName)
            .SetMiddleName(request.MiddleName);

        if (request.PhoneNumber is not null)
        {
            user.SetPhoneNumber(request.PhoneNumber);
        }

        await this.userDomainRepository.SaveAsync(cancellation);
        return this.Success(Messages.UserDetailsUpdated);
    }
}
