namespace Ultima.Extensions.Collections;

using System;

internal sealed class ExactTypeComparer : TypeComparer
{
    public override int Compare(Type? x, Type? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }
        else if (x is null)
        {
            return -1;
        }
        else if (y is null)
        {
            return 1;
        }
        else
        {
            return string.CompareOrdinal(x.FullName, y.FullName);
        }
    }
}
