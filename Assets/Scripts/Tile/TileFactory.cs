#region

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#endregion

public class TileFactory : MonoBehaviour {
    private GameObject _parent;
    private IEnumerator _tilesLeft;

    public void CreateGameObjects(TileMap tileMap) {
        if (_parent)
            Destroy(_parent);
        _parent = new GameObject("TileMap");
        _tilesLeft = tileMap.Tiles.GetEnumerator();
    }

    public void Update() {
        var sw = Stopwatch.StartNew();
        var count = 0;
        while (count < 100 && _tilesLeft.MoveNext()) {
            count++;
            Tile tile = (Tile) _tilesLeft.Current ;
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.RemoveComponent<MeshCollider>();
            quad.transform.position = new Vector3(tile.X, 0, tile.Z);
            quad.transform.Rotate(90, 0, 0);
            _parent.AddChild(quad);
        }
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);
    }
}