namespace RpgBooks.Modules.Identity.Domain.Builders.Abstractions;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Libraries.Module.Domain.Abstractions;

internal interface IUserBuilder : IAsyncBuilder<User>
{
    IUserBuilder WithEmail(string email);

    IUserBuilder WithPassword(string password);
}
