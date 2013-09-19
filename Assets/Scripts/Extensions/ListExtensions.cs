using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Collections;

internal static class ListExtensions
{
    /// <summary>
    ///     Returns true if all of the items are in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pList"></param>
    /// <param name="pItems"></param>
    /// <returns></returns>
    public static bool ContainsAll<T>(this List<T> pList, params T[] pItems)
    {
        return !pItems.Any(item => pList.Contains(item) == false);
    }

    /// <summary>
    ///     Returns true if any of the items are in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pList"></param>
    /// <param name="pItems"></param>
    /// <returns></returns>
    public static bool ContainsAny<T>(this List<T> pList, params T[] pItems)
    {
        return pItems.Any(pList.Contains);
    }

    /// <summary>
    ///     Gets all the indices of items that match the item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pList"></param>
    /// <param name="pItem"></param>
    /// <returns></returns>
    public static int[] GetAllIndices<T>(this List<T> pList, T pItem)
    {
        var indices = new List<int>();
        for (int i = 0; i < pList.Count; i++)
        {
            if (pList[i].Equals(pItem))
                indices.Add(i);
        }
        return indices.ToArray();
    }

    /// <summary>
    ///     Gets a random element in the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pList"></param>
    /// <returns></returns>
    public static T Random<T>(this List<T> pList)
    {
        if (pList.Count == 0)
            throw new Exception("Can't get a random item of an empty list.");
        return pList[RandomGenerator.Rng.Next(pList.Count)];
    }

    public static T RemoveRandom<T>(this List<T> pList)
    {
        T random = pList.Random();
        pList.Remove(random);
        return random;
    }

    public static string ToCSV<T>(this List<T> pList)
    {
        return pList.ToArray().ToCSV();
    }

    public static T Pop<T>(this List<T> pList)
    {
        T first = pList.First();
        pList.RemoveAt(0);
        return first;
    }

    /// <summary>
    /// Returns true if the list is empty.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsEmpty(this IList list)
    {
        return list.Count == 0;
    }
}