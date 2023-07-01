namespace RpgBooks.Libraries.Module.Application.Queries;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RpgBooks.Libraries.Module.Application.Commands;
using RpgBooks.Libraries.Module.Application.Queries.Contracts;

using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Query requests dispatcher that calls only the first found query handler.
/// </summary>
public sealed class SingleQueryHandlerDispatcher : IQueryHandlerDispatcher
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleQueryHandlerDispatcher"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public SingleQueryHandlerDispatcher(IServiceProvider serviceProvider)
        => this.serviceProvider = serviceProvider;

    /// <inheritdoc/>
    public async Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery
    {
        var logger = this.serviceProvider.GetRequiredService<ILogger>();

        var timeStamp = Stopwatch.GetTimestamp();
        var result = await this.serviceProvider
            .GetRequiredService<IQueryHandler<TQuery, TQueryResult>>()
            .Handle(query, cancellation);

        logger.LogRequestHandlingTime(typeof(TQuery).Name, Stopwatch.GetElapsedTime(timeStamp).Milliseconds);
        return result;
    }
}
