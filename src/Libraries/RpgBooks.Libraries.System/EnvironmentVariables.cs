namespace System;

using Cysharp.Text;

/// <summary>
/// Class containing all environment variables for the current application instance.
/// </summary>
public static class EnvironmentVariables
{
    /// <summary>
    /// Gets Environment name key.
    /// </summary>
    public const string EnvironmentNameKey = "ASPNETCORE_ENVIRONMENT";

    /// <summary>
    /// Gets Application name key.
    /// </summary>
    public const string TestingKey = "APPLICATION_TESTING";

    /// <summary>
    /// Gets the current environment name.
    /// </summary>
    public static readonly string EnvironmentName =
        Environment.GetEnvironmentVariable(EnvironmentNameKey) ??
        throw new ApplicationException(ZString.Format("{0} is not set!", EnvironmentNameKey));

    /// <summary>
    /// Gets a value indicating whether the application is in development.
    /// </summary>
    public static readonly bool IsInDevelopment = EnvironmentName == Environments.Development;

    /// <summary>
    /// Gets a value indicating whether the application is in staging.
    /// </summary>
    public static readonly bool IsInStaging = EnvironmentName == Environments.Staging;

    /// <summary>
    /// Gets a value indicating whether the application is in production.
    /// </summary>
    public static readonly bool IsInProduction = EnvironmentName == Environments.Production;
}
