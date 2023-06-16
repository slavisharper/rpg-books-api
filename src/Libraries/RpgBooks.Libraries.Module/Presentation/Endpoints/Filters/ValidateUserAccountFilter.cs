namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Filters;

using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Libraries.Module.Infrastructure.Persistence;
using RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

using System.Threading.Tasks;

/// <summary>
/// Validates whether given user account is valid and active.
/// <para>This filter is applied to all authorized requests.</para>
/// <para>This will eliminate the need of user checks in each endpoint and request handler.</para>
/// <para>This checks if the user exists in the database, if the security stamp is valid, if the user is blocked or locked out.</para>
/// </summary>
public sealed class ValidateUserAccountFilter : IEndpointFilter
{
    private const string GetUserDataQuery = @"
        Select 
            SecurityStamp,
            Blocked,
            LockoutEnd
        From IDT_Users
        WHERE Id = @Id";

    private readonly ICurrentUserService currentUserService;
    private readonly DefaultDapperContext dapperContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidateUserAccountFilter"/> class.
    /// </summary>
    /// <param name="currentUserService">Current user service.</param>
    /// <param name="dapperContext">SQL connection.</param>
    public ValidateUserAccountFilter(ICurrentUserService currentUserService, DefaultDapperContext dapperContext)
    {
        this.currentUserService = currentUserService;
        this.dapperContext = dapperContext;
    }

    /// <inheritdoc/>
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (this.currentUserService.User is null)
        {
            return Results.Unauthorized();
        }

        var currentUserDbData = await this.dapperContext.Connection
            .QueryFirstAsync<UserData>(GetUserDataQuery, new { this.currentUserService.User.Id });

        if (currentUserDbData is null)
        {
            return Results.Unauthorized();
        }

        if (currentUserDbData.LockoutEnd is not null && currentUserDbData.LockoutEnd > DateTimeOffset.UtcNow)
        {
            return Results.Unauthorized();
        }

        if (currentUserDbData.Blocked)
        {
            return Results.Unauthorized();
        }

        if (!this.currentUserService.User.Claims(UserClaimTypes.SecurityStamp, currentUserDbData.SecurityStamp))
        {
            return Results.Unauthorized();
        }

        return await next(context);
    }

    private record UserData(string SecurityStamp, bool Blocked, DateTimeOffset? LockoutEnd);
}
