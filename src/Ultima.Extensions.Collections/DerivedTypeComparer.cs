namespace Ultima.Extensions.Collections;

using System;

internal sealed class DerivedTypeComparer : TypeComparer
{
    public override int Compare(Type? x, Type? y)
    {
        if (x is null)
        {
            return y is null ? 0 : -1;
        }
        else if (y is null)
        {
            return 1;
        }
        else if (x.IsAssignableFrom(y) || y.IsAssignableFrom(x))
        {
            return 0;
        }
        else
        {
            return string.CompareOrdinal(x.FullName, y.FullName);
        }
    }
}
