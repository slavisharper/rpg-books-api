namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Repositories;

using Microsoft.Data.SqlClient;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;

using System.Data.Common;

/// <summary>
/// Base read only repository implementation.
/// </summary>
public abstract class DapperReadOnlyRepository : IReadOnlyRepository
{
    /// <summary>
    /// Dapper db connection.
    /// </summary>
    protected readonly SqlConnection connection;


    /// <summary>
    /// Initializes a new instance of the <see cref="DapperReadOnlyRepository"/> class.
    /// </summary>
    /// <param name="dbConnection"></param>
    public DapperReadOnlyRepository(SqlConnection dbConnection)
    {
        this.connection = dbConnection;
    }
}
