namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories;

using Dapper;

using Microsoft.Data.SqlClient;

using RpgBooks.Libraries.Module.Infrastructure.Persistence.Repositories;
using RpgBooks.Modules.Identity.Application.Repositories.User;
using RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories.DapperQueries;

using System;
using System.Threading;
using System.Threading.Tasks;

internal sealed class UserReadOnlyRepository : DapperReadOnlyRepository, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(SqlConnection dbConnection)
        : base(dbConnection)
    {
    }

    /// <inheritdoc/>
    public async Task<UserDetailsReadModel?> GetDetails(int id, CancellationToken cancellationToken = default)
        => await this.connection
            .QueryFirstOrDefaultAsync<UserDetailsReadModel>(UserDapperQueries.GetDetailsQuery, new { Id = id });
    
}
