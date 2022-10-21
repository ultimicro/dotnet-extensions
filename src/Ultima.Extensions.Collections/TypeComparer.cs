namespace Ultima.Extensions.Collections;

using System;
using System.Collections.Generic;

/// <summary>
/// Provides implementations of <see cref="IComparer{T}"/> for <see cref="Type"/>.
/// </summary>
public abstract class TypeComparer : IComparer<Type>
{
    internal TypeComparer()
    {
    }

    /// <summary>
    /// Gets a comparer for <see cref="Type"/> that treat a derived type equal to base type.
    /// </summary>
    public static TypeComparer Derived { get; } = new DerivedTypeComparer();

    /// <summary>
    /// Gets a comparer for <see cref="Type"/> that use exactly match.
    /// </summary>
    public static TypeComparer Exact { get; } = new ExactTypeComparer();

    public abstract int Compare(Type? x, Type? y);
}
