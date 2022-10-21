namespace Ultima.Extensions.Collections;

using System;
using System.Collections;
using System.Collections.Generic;

public class KeyedByTypeCollection<T> : IKeyedByTypeCollection<T> where T : class
{
    private readonly SortedDictionary<Type, T> items;

    public KeyedByTypeCollection()
    {
        this.items = new(TypeComparer.Exact);
    }

    public KeyedByTypeCollection(IComparer<Type> comparer)
    {
        this.items = new(comparer);
    }

    public int Count => this.items.Count;

    public void Add<TItem>(TItem item) where TItem : T
    {
        try
        {
            this.items.Add(typeof(TItem), item);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException($"The item with type {typeof(TItem)} is already exists.", ex);
        }
    }

    public void Add(T item, Type type)
    {
        try
        {
            this.items.Add(type, item);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"The item with type {type} is already exists.", nameof(type), ex);
        }
    }

    public TItem? Get<TItem>() where TItem : T
    {
        return this.items.TryGetValue(typeof(TItem), out var item) ? (TItem)item : default;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var (_, value) in this.items)
        {
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
