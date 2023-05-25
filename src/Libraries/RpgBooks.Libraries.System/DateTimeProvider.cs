namespace System;

/// <summary>
/// Date time provider using official <see cref="DateTimeOffset"/>.
/// </summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    private static readonly AsyncLocal<Func<DateTimeOffset>?> NowOverride = new();
    private static readonly AsyncLocal<Func<DateTimeOffset>?> UtcNowOverride = new();

    private static readonly IDateTimeProvider Provider = new DateTimeProvider();

    /// <summary>
    /// Gets <see cref="DateTimeProvider"/> instance.
    /// </summary>
    public static IDateTimeProvider Instance => Provider;

    /// <inheritdoc/>
    public DateTimeOffset Now => NowOverride.Value is null ? DateTimeOffset.Now : NowOverride.Value();

    /// <inheritdoc/>
    public DateTimeOffset UtcNow => UtcNowOverride.Value is null ? DateTimeOffset.UtcNow : UtcNowOverride.Value();

    /// <summary>
    /// Overrides current <see cref="Instance"/>.Now behavior.
    /// </summary>
    /// <param name="date">Overridden date.</param>
    public static void OverrideNow(DateTime date)
    {
        NowOverride.Value = () => new DateTimeOffset(date);
    }

    /// <summary>
    /// Overrides current <see cref="Instance"/>.Now behavior.
    /// </summary>
    /// <param name="year">Overridden year.</param>
    /// <param name="month">Overridden month.</param>
    /// <param name="day">Overridden day.</param>
    public static void OverrideNow(int year, int month, int day)
    {
        NowOverride.Value = () => new DateTimeOffset(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Local));
    }

    /// <summary>
    /// Overrides current <see cref="Instance"/>.Now behavior.
    /// </summary>
    /// <param name="year">Overridden year.</param>
    /// <param name="month">Overridden month.</param>
    /// <param name="day">Overridden day.</param>
    /// <param name="hour">Overridden hour.</param>
    /// <param name="minute">Overridden minute.</param>
    /// <param name="second">Overridden second.</param>
    public static void OverrideNow(int year, int month, int day, int hour, int minute, int second)
    {
        NowOverride.Value = () => new DateTimeOffset(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local));
    }

    /// <summary>
    /// Overrides current <see cref="Instance"/>.UtcNow behavior.
    /// </summary>
    /// <param name="date">Overridden date.</param>
    public static void OverrideUtcNow(DateTime date)
    {
        UtcNowOverride.Value = () => new DateTimeOffset(date);
    }

    /// <summary>
    /// Overrides <see cref="Instance"/>.UtcNow behavior.
    /// </summary>
    /// <param name="year">Overridden year.</param>
    /// <param name="month">Overridden month.</param>
    /// <param name="day">Overridden day.</param>
    public static void OverrideUtcNow(int year, int month, int day)
    {
        UtcNowOverride.Value = () => new DateTimeOffset(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc));
    }

    /// <summary>
    /// Overrides <see cref="Instance"/>.UtcNow behavior.
    /// </summary>
    /// <param name="year">Overridden year.</param>
    /// <param name="month">Overridden month.</param>
    /// <param name="day">Overridden day.</param>
    /// <param name="hour">Overridden hour.</param>
    /// <param name="minute">Overridden minute.</param>
    /// <param name="second">Overridden second.</param>
    public static void OverrideUtcNow(int year, int month, int day, int hour, int minute, int second)
    {
        UtcNowOverride.Value = () => new DateTimeOffset(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc));
    }

    /// <summary>
    /// Reset all date time overrides.
    /// </summary>
    public static void ResetOverrides()
    {
        NowOverride.Value = null;
        UtcNowOverride.Value = null;
    }
}
