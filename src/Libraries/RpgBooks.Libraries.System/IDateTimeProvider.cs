namespace System;

/// <summary>
/// Date time provider interface that should be used in our application instead of <see cref="DateTime"/>.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets <see cref="IDateTimeProvider"/> instance.
    /// </summary>
    static IDateTimeProvider Instance { get; } = default!;

    /// <summary>
    /// Gets a DateTime representing the current date and time.
    /// </summary>
    DateTimeOffset Now { get; }

    /// <summary>
    /// Gets a DateTime representing the current date.
    /// </summary>
    DateTimeOffset UtcNow { get; }
}
