namespace System;

/// <summary>
/// Boolean extension methods.
/// </summary>
public static class BooleanExtensions
{
    private const string Yes = "Yes";
    private const string No = "No";

    /// <summary>
    /// Converts true/false to Yes/No string.
    /// </summary>
    /// <param name="value">Boolean value.</param>
    /// <returns>Yes or No.</returns>
    public static string ToYesNo(this bool value)
    {
        return value ? Yes : No;
    }
}
