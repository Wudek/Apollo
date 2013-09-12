using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour {

    public void AddChild(GameObject go) {
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    ///     Destroys all children
    /// </summary>
    public void ClearChildren() {
        while (transform.childCount > 0)
            Destroy(transform.GetChild(0));
    }
}