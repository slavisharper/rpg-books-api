namespace System.Threading.Tasks;

/// <summary>
/// Used to run async methods synchronously in cases when await cannot be used.
/// </summary>
public static class AsyncHelper
{
    private static readonly TaskFactory TaskFactory;

    static AsyncHelper()
    {
        TaskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);
    }

    /// <summary>
    /// Executes an async method with a void result type synchronously.
    /// </summary>
    /// <param name="task">Async method to execute.</param>
    public static void RunSync(Func<Task> task)
        => TaskFactory
            .StartNew(task)
            .Unwrap()
            .GetAwaiter()
            .GetResult();

    /// <summary>
    /// Executes an async method with a result of type <typeparamref name="TResult"/> synchronously.
    /// </summary>
    /// <typeparam name="TResult">Type of the result returned from the task.</typeparam>
    /// <param name="task">Async method to execute.</param>
    /// <returns>Synchronous data.</returns>
    public static TResult RunSync<TResult>(Func<Task<TResult>> task)
        => TaskFactory
            .StartNew(task)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
}
