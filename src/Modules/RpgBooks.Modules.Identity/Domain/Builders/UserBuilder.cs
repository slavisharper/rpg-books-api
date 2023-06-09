﻿namespace RpgBooks.Modules.Identity.Domain.Builders;

using RpgBooks.Libraries.Module.Application.Services;
using RpgBooks.Modules.Identity.Domain.Builders.Abstractions;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Events;
using RpgBooks.Modules.Identity.Domain.Exceptions;
using RpgBooks.Modules.Identity.Domain.Services;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using System.Security.Policy;

internal sealed class UserBuilder : IUserBuilder
{
    private readonly IPasswordHasher passwordHasher;
    private readonly ISecurityTokensService securityTokensService;
    private readonly IHttpUtilities httpUtilities;

    private string? email;
    private string? paswordHash;

    public UserBuilder(
        IPasswordHasher passwordHasher,
        ISecurityTokensService securityTokensService,
        IHttpUtilities httpUtilities)
    {
        this.passwordHasher = passwordHasher;
        this.securityTokensService = securityTokensService;
        this.httpUtilities = httpUtilities;
    }

    /// <inheritdoc/>
    public IUserBuilder WithEmail(string email)
    {
        this.email = email;
        return this;
    }

    /// <inheritdoc/>
    public IUserBuilder WithPassword(string password)
    {
        this.paswordHash = this.passwordHasher.HashPassword(password);
        return this;
    }

    /// <inheritdoc/>
    public async Task<User> Build(CancellationToken cancellation = default)
    {
        if (email is null || paswordHash is null)
        {
            throw new InvalidUserException();
        }

        var user = new User(email, paswordHash);
        TokenModel emailConfirmationToken =
            await this.securityTokensService.GenerateEmailConfirmationToken(user, cancellation);

        user.AddEvent(new UserRegisteredEvent(email, this.httpUtilities.UrlEncode(emailConfirmationToken.Value)!));
        return user;
    }
}
