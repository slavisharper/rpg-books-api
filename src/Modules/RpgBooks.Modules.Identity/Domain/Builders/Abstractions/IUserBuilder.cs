namespace RpgBooks.Modules.Identity.Domain.Builders.Abstractions;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Libraries.Module.Domain.Abstractions;

/// <summary>
/// Builder interface for <see cref="User"/>.
/// </summary>
internal interface IUserBuilder : IBuilder<User>
{
    /// <summary>
    /// Adds the user email to the builder.
    /// </summary>
    /// <param name="email">Users email.</param>
    /// <returns>User builder instance.</returns>
    IUserBuilder WithEmail(string email);

    /// <summary>
    /// Adds the user password to the builder.
    /// </summary>
    /// <param name="password">Users password.</param>
    /// <returns>User builder instance.</returns>
    IUserBuilder WithPassword(string password);
}
