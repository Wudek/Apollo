using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class GameObjectExtensions
{
    public static bool Has<T>(this GameObject pGO) where T : Component {
        return pGO.GetComponent<T>() != null;
    }

    public static T GetOrCreate<T>(this GameObject pGO) where T : Component {
        var comp = pGO.GetComponent<T>();
        if (comp != null)
            return comp;
        return pGO.AddComponent<T>();
    }

    public static void DestroyChildren(this GameObject pGo) {
        foreach (Transform child in pGo.transform)
        {
           GameObject.Destroy(child.gameObject);
        }
    }
}

