#region

using UnityEngine;

#endregion

internal static class GameObjectExtensions {

    static public T AddComponent<T>(this MonoBehaviour monoBehaviour) where T : Component {
        return monoBehaviour.gameObject.AddComponent<T>();
    }
    public static void AddChild(this GameObject parent, GameObject child) {
        child.transform.parent = parent.transform;
    }

    public static void RemoveComponent<T>(this GameObject parent) where T : Component {
        var comp = parent.GetComponent<T>();
        if(comp)
            Object.Destroy(comp);
    }

    public static void ClearChildren(this GameObject parent) {
        while (parent.transform.childCount > 0)
            Object.Destroy(parent.transform.GetChild(0));
    }

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
        foreach (Transform child in pGo.transform) Object.Destroy(child.gameObject);
    }
}