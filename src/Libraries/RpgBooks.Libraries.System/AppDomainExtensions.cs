namespace System;

using Reflection;

/// <summary>
/// <see cref="AppDomain"/> extension methods.
/// </summary>
public static class AppDomainExtensions
{
    private const string ApplicationLayerName = "Application";

    /// <summary>
    /// Gets all application layer assemblies.
    /// </summary>
    /// <param name="appDomain">Current application domain.</param>
    /// <returns>Fund application layer assemblies.</returns>
    public static Assembly[] GetApplicationLayerAssemblies(this AppDomain appDomain)
    {
        return appDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly
                .GetReferencedAssemblies()
                .Where(name => name.FullName.Contains(ApplicationLayerName))
                .Select(name => Assembly.Load(name)))
            .Distinct()
            .ToArray();
    }
}
