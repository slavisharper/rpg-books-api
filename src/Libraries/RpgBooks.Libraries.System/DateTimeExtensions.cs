namespace System;

/// <summary>
/// Defines date and time related extension methods.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Determines whether a <see cref="DateTimeOffset"/> instance is in the specified period.
    /// </summary>
    /// <param name="source">Source date.</param>
    /// <param name="start">Start date.</param>
    /// <param name="end">End date.</param>
    /// <returns>A boolean variable, indicating whether the source instance fits in the specified period.</returns>
    public static bool IsBetween(this DateTimeOffset source, DateTimeOffset start, DateTimeOffset end)
    {
        if (start >= end)
        {
            throw new ArgumentException("The start date must be earlier that the end date", nameof(start));
        }

        return source >= start && source <= end;
    }
}
