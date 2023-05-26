namespace Ultima.Extensions.DataValidation;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Specifies that the item in a collection must be unique.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public sealed class UniqueAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not IEnumerable target)
        {
            return true;
        }

        var set = new HashSet<object>();

        foreach (var item in target)
        {
            if (!set.Add(item))
            {
                return false;
            }
        }

        return true;
    }
}
