using UnityEngine;

public class MainScreen : MonoBehaviour {

    private TileMap _tileMap;

    public void Start() {
        _tileMap = new TileMap(100);
        var tileFactory = this.AddComponent<TileFactory>();
        tileFactory.CreateGameObjects(_tileMap);
        Camera.main.gameObject.AddComponent<TileMapCameraWatcher>().TileMap = _tileMap;
    }

}