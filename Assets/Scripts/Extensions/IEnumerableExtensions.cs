#region

using System;
using System.Collections.Generic;

#endregion

// ReSharper disable once InconsistentNaming
internal static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (var item in enumeration)
        {
            action(item);
        }
    }
}