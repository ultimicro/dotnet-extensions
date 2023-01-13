namespace Ultima.Extensions.Collections;

using System.Runtime.InteropServices;

/// <summary>
/// Provides extension methods for <see cref="Dictionary{TKey, TValue}"/>.
/// </summary>
public static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory) where TKey : notnull
    {
        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out var exists);

        if (!exists)
        {
            try
            {
                value = factory();
            }
            catch
            {
                dictionary.Remove(key);
                throw;
            }
        }

        return value!;
    }
}
