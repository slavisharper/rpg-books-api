namespace System.Reflection;

/// <summary>
/// <see cref="Assembly"/> extensions for working with embedded resources.
/// </summary>
public static class EmbededResourceExtensions
{
    /// <summary>
    /// Finds all embedded resources in the assembly matching conditions in a given predicate filter function.
    /// </summary>
    /// <param name="assembly">Assembly that will be searched</param>
    /// <param name="predicate">Predicate that filters the result.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown when predicate function is null.</exception>
    public static IEnumerable<string> FindEmbededResources(
        this Assembly assembly,
        Func<string, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));

        return
            assembly.GetEmbededResourceNames()
                .Where(predicate)
                .Select(name => assembly.ReadEmbededResource(name))
                .Where(x => !string.IsNullOrEmpty(x));
    }

    /// <summary>
    /// Gets all embedded resource names in the assembly.
    /// </summary>
    /// <param name="assembly">Assembly that contains the resources.</param>
    /// <returns>Gets list</returns>
    public static IEnumerable<string> GetEmbededResourceNames(this Assembly assembly)
    {
        return assembly.GetManifestResourceNames();
    }

    /// <summary>
    /// Reads embedded resource content from the assembly.
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string ReadEmbededResource(this Assembly assembly, string name)
    {
        using var resourceStream = assembly.GetManifestResourceStream(name);
        if (resourceStream is null)
        {
            return string.Empty;
        }

        using var streamReader = new StreamReader(resourceStream);
        return streamReader.ReadToEnd();
    }
}