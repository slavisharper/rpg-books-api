﻿namespace RpgBooks.Modules.Identity.Application.Commands.ChangePassword;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;

/// <summary>
/// Change password endpoint.
/// </summary>
[AuthorizeEndpoint]
public sealed class ChangePasswordEndpoint : ApiEndpoint<ChangePasswordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordEndpoint"/> class.
    /// </summary>
    public ChangePasswordEndpoint()
        : base(
            "/api/account/change-password",
            nameof(ChangePasswordEndpoint),
            "account",
            EndpointTypes.Put,
            EndpointDelegates.CreateCommandHandlerDelegate<ChangePasswordCommand>())
    {
    }
}
