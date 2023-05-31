namespace System;

/// <summary>
/// Extension methods of <see cref="DateTime"/> and <see cref="DateOnly"/> for calculating age.
/// </summary>
public static class DateAgeExtensions
{
    /// <summary>
    /// Calculate age.
    /// </summary>
    /// <param name="dateOfBirth">Date of birth.</param>
    /// <returns>Calculated age.</returns>
    public static int Age(this DateOnly dateOfBirth)
        => CalculateAge(dateOfBirth, DateOnly.FromDateTime(DateTime.Now));

    /// <summary>
    /// Calculate age based on UTC time.
    /// </summary>
    /// <param name="dateOfBirth">Date of birth.</param>
    /// <returns>Calculated age.</returns>
    public static int UtcAge(this DateOnly dateOfBirth)
        => CalculateAge(dateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow));

    /// <summary>
    /// Calculate age.
    /// </summary>
    /// <param name="dateOfBirth">Date of birth.</param>
    /// <returns>Calculated age.</returns>
    public static int Age(this DateTime dateOfBirth)
        => CalculateAge(DateOnly.FromDateTime(dateOfBirth), DateOnly.FromDateTime(DateTime.Now));

    /// <summary>
    /// Calculate age based on UTC time.
    /// </summary>
    /// <param name="dateOfBirth">Date of birth.</param>
    /// <returns>Calculated age.</returns>
    public static int UtcAge(this DateTime dateOfBirth)
        => CalculateAge(DateOnly.FromDateTime(dateOfBirth), DateOnly.FromDateTime(DateTime.UtcNow));

    /// <summary>
    /// Calculate age.
    /// </summary>
    /// <param name="dateOfBirth">Date of birth.</param>
    /// <returns>Calculated age. Returns 0 if null.</returns>
    public static int Age(this DateTime? dateOfBirth)
    {
        if (dateOfBirth is null)
        {
            return default;
        }

        return CalculateAge(DateOnly.FromDateTime(dateOfBirth.Value), DateOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>
    /// Calculate age based on UTC time.
    /// </summary>
    /// <param name="dateOfBirth">Date of birth.</param>
    /// <returns>Calculated age. Returns 0 if null.</returns>
    public static int UtcAge(this DateTime? dateOfBirth)
    {
        if (dateOfBirth is null)
        {
            return default;
        }

        return CalculateAge(DateOnly.FromDateTime(dateOfBirth.Value), DateOnly.FromDateTime(DateTime.UtcNow));
    }

    private static int CalculateAge(DateOnly birth, DateOnly now)
    {
        int year = now.Year - birth.Year;
        if (now.Month < birth.Month || now.Month == birth.Month && now.Day < birth.Day)
        {
            year--;
        }

        return year;
    }
}
