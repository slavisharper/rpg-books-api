namespace RpgBooks.Libraries.Module.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using RpgBooks.Libraries.Module.Application.Exceptions;
using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Libraries.Module.Domain.Events;
using RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;
using RpgBooks.Libraries.Module.Infrastructure.Persistence.Extensions;

/// <summary>
/// Base DB context.
/// </summary>
/// <typeparam name="TContext">Type of the DB context implementation.</typeparam>
public abstract class BaseDbContext<TContext> : DbContext, IDbContext
    where TContext : DbContext, IDbContext
{
    private readonly IDomainEventDispatcher eventDispatcher;
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDbContext{TContext}"/> class.
    /// </summary>
    /// <param name="options">DB context options.</param>
    /// <param name="currentUserService">Current user service.</param>
    /// <param name="eventDispatcher">Custom event dispatcher.</param>
    /// <param name="logger">Logger instance.</param>
    public BaseDbContext(
        DbContextOptions<TContext> options,
        ICurrentUserService currentUserService,
        IDomainEventDispatcher eventDispatcher,
        ILogger logger)
        : base(options)
    {
        this.CurrentUserService = currentUserService;
        this.eventDispatcher = eventDispatcher;
        this.logger = logger;
    }

    /// <summary>
    /// Gets the current user service.
    /// </summary>
    public ICurrentUserService? CurrentUserService { get; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A Task that represents the Save operation. The task result contains the number of state entries written to the database.</returns>
    public int SaveChanges(CancellationToken cancellationToken = default)
         => AsyncHelper.RunSync(() => this.SaveChangesAsync(cancellationToken));

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await this.DispatchEvents(this.eventDispatcher, this.logger);
            }

            return result;
        }
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            this.ChangeTracker.Clear(); 
            throw new ApplicationConcurrencyException(concurrencyException);
        }
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
    }
}
