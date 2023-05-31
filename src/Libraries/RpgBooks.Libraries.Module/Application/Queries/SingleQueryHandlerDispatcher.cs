namespace RpgBooks.Libraries.Module.Application.Queries;

using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;

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
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery
        => this.serviceProvider
            .GetRequiredService<IQueryHandler<TQuery, TQueryResult>>()
            .Handle(query, cancellation);
}
