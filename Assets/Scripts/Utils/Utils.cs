using System;
using System.Collections.Generic;
using UnityEngine;

static class Utils
{
    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        else if (val.CompareTo(max) > 0) return max;
        else return val;
    }

    /// <summary>
    /// Searches upwards thru the hierarchy for the first game object that has this behavior.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="val"></param>
    /// <returns></returns>
    public static GameObject Find<T>(this MonoBehaviour val) where T : MonoBehaviour
    {
        if (val == null)
            return null;
        var go = val.gameObject;
        return go.Find<T>();
    }

    /// <summary>
    /// Searches upwards thru the hierarchy for the first game object that has this behavior.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pGO"></param>
    /// <returns></returns>
    public static GameObject Find<T>(this GameObject pGO) {
        if (pGO == null)
            return null;
        var component = pGO.GetComponent(typeof(T));
        if (component != null)
            return pGO;
        else if(pGO.transform.parent != null) {
            return pGO.transform.parent.gameObject.Find<T>();
        }
        else
            return null;
    }

    static public T LoadResource<T>(string path, bool log = true) {
        var resource = Resources.Load(path);
        if (log && resource == null) {
            Debug.LogError("Resource was not found at path: " + path);
        }
        return (T) (object) resource;
    }

    static public List<T> LoadResources<T>(params string[] paths ) {
        List<T> result = new List<T>();
        foreach (var path in paths) {
            result.Add(LoadResource<T>(path));
        }
        return result;
    }
}

