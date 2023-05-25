namespace System.Reflection;

public static class EmbededResourceExtensions
{
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

    public static IEnumerable<string> GetEmbededResourceNames(this Assembly assembly)
    {
        return assembly.GetManifestResourceNames();
    }

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