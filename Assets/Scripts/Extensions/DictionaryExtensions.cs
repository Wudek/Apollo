using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class DictionaryExtensions {
    
    public static bool ContainsKeys<TKey,TValue>(this Dictionary<TKey, TValue> pDictionary, params TKey[] pKeys) {
        return pKeys.All(pDictionary.ContainsKey);
    }

    public static bool ContainsAnyKey<TKey, TValue>(this Dictionary<TKey, TValue> pDictionary, params TKey[] pKeys)
    {
        return pKeys.Any(pDictionary.ContainsKey);
    }
}
