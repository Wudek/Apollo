using System;
using System.Collections.Generic;

internal static class ArrayExtensions {
    /// <summary>
    /// Gets the next element after pItem in the array.
    /// </summary>
    public static T Next<T>(this T[] pArray, T pItem) {
        int index = Array.IndexOf(pArray, pItem);
        index = (index + 1) % pArray.Length;
        return pArray[index];
    }

    /// <summary>
    /// Gets the pervious element before pItem in the array.
    /// </summary>
    public static T Previous<T>(this T[] pArray, T pItem) {
        int index = Array.IndexOf(pArray, pItem);
        index--;
        if (index < 0)
            index = pArray.Length - 1;
        return pArray[index];
    }

    /// <summary>
    /// Unwraps a string array and adds a seperator between each element of the array.
    /// </summary>
    public static string Unwrap(this string[] pArray, string pSeperator = "") {
        string result = "";
        for (int i = 0; i < pArray.Length; i++) {
            if (i != 0)
                result += pSeperator;
            result += pArray[i];
        }
        return result;
    }

    /// <summary>
    /// Gets a random element in the array.
    /// </summary>
    public static T Random<T>(this T[] pItems)
    {
        if (pItems.Length == 0)
            throw new Exception("Can't get a random item of an empty array.");
        return pItems[RandomGenerator.Rng.Next(pItems.Length)];
    }

    public static List<T> ToList<T>(this IEnumerable<T> pArray) {
        return new List<T>(pArray);
    }

    public static string ToCSV<T>(this T[] pArray)
    {
        if (pArray.Length == 0)
            return "";
        var result = pArray[0].ToString();
        for (var i = 1; i < pArray.Length; i++)
        {
            result += ", " + pArray[i].ToString();
        }
        return result;
    }
}