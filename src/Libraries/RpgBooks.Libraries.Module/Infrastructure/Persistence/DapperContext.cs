namespace RpgBooks.Libraries.Module.Infrastructure.Persistence;

using Microsoft.Data.SqlClient;

using System.Data;

/// <summary>
/// Dapper SQL context. This is a wrapper around the IDbConnection interface.
/// For each EF Db Context we get a Dapper Context. This is used to execute raw SQL queries when EF is not enough.
/// <para>This wrapper removes the need to create different dapper contexts for each app module.</para>
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public class DapperContext<TDbContext> : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DapperContext{TDbContext}"/> class.
    /// </summary>
    /// <param name="connectionString">Db connection string.</param>
    public DapperContext(string connectionString)
    {
        this.Connection = new SqlConnection(connectionString);
    }

    /// <summary>
    /// DB connection.
    /// </summary>
    public IDbConnection Connection { get; init; }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Connection.Dispose();
    }
}

/// <summary>
/// Default dapper context that uses the default connection string.
/// <para>Default connection should have access to the most critical DB tables. For example Identity tables.</para>
/// </summary>
public class DefaultDapperContext : DapperContext<DefaultDapperContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultDapperContext"/> class.
    /// </summary>
    /// <param name="connectionString">DB connection string.</param>
    public DefaultDapperContext(string connectionString) : base(connectionString)
    {
    }
}
