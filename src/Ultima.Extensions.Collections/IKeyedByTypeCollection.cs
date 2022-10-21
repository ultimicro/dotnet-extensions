namespace Ultima.Extensions.Collections;

using System.Collections.Generic;

public interface IKeyedByTypeCollection<T> : IReadOnlyCollection<T> where T : class
{
    /// <summary>
    /// Get item with a specified type.
    /// </summary>
    /// <typeparam name="TItem">
    /// Type of item to lookup.
    /// </typeparam>
    /// <returns>
    /// An item with type specified in <typeparamref name="TItem"/> or <see langword="null"/> if no item with the specified type.
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// An item with type <typeparamref name="TItem"/> has been found but it cannot be converted to <typeparamref name="TItem"/>.
    /// </exception>
    TItem? Get<TItem>() where TItem : T;
}
