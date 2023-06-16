namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories;

using Dapper;

using RpgBooks.Libraries.Module.Infrastructure.Persistence;
using RpgBooks.Modules.Identity.Application.Repositories.User;
using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories.DapperQueries;

internal sealed class UserReadOnlyRepository : IUserReadOnlyRepository
{
    private readonly DapperContext<IdentityDbContext> dapperContext;

    public UserReadOnlyRepository(DapperContext<IdentityDbContext> dapperContext)
    {
        this.dapperContext = dapperContext;
    }

    public async Task<SecurityTokenReadModel?> GetActualToken(int userId, SecurityTokenType type, CancellationToken cancellation = default)
        => await this.dapperContext.Connection
            .QueryFirstOrDefaultAsync<SecurityTokenReadModel>(
                UserDapperQueries.GetActualSecurityToken,
                new { UserId = userId, TokenType = type.Value });

    public async Task<SecurityTokenReadModel?> GetActualToken(int userId, SecurityTokenType type, string? sessionId, CancellationToken cancellation = default)
        => await this.dapperContext.Connection
            .QueryFirstOrDefaultAsync<SecurityTokenReadModel>(
                UserDapperQueries.GetActualSecurityTokenWithSession,
                new { UserId = userId, TokenType = type.Value, SessionId = sessionId });

    /// <inheritdoc/>
    public async Task<UserDetailsReadModel?> GetDetails(int id, CancellationToken cancellationToken = default)
        => await this.dapperContext.Connection
            .QueryFirstOrDefaultAsync<UserDetailsReadModel>(UserDapperQueries.GetDetailsQuery, new { Id = id });
}
