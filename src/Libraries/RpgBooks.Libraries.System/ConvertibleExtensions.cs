namespace System;

public static class ConvertibleExtensions
{
    public static T To<T>(this IConvertible obj)
    {
        return (T)Convert.ChangeType(obj, typeof(T));
    }
}