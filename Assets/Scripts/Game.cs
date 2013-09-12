#region

using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

public class Game : GameBehaviour
{
    private MainScreen _mainScreen;

    public void Start() {
        var go = new GameObject("MainScreen");
        go.hideFlags = HideFlags.HideInHierarchy;
        _mainScreen = go.AddComponent<MainScreen>();
    }

    private void CleanUpScene()
    {
        FindSceneObjectsOfType(typeof (GameObject)).Where(o => o != gameObject).ForEach(Destroy);
    }
}