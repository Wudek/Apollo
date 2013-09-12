using UnityEngine;

public static class TransformExtensions {

    /// <summary>
    /// The bottom left of the screen is (0,0), top right is (100,100)
    /// </summary>
    public static void MoveTo(this Transform transform, float x, float y) {
        var camera = Camera.mainCamera;
        x /= 100f;
        y /= 100f;
        var newDestination = camera.ScreenToWorldPoint(new Vector3(x * camera.pixelWidth, y * camera.pixelHeight, 0));
        newDestination.z = 0;
        transform.position = newDestination;
    }
}
