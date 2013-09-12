using UnityEngine;

public class MainScreen : GameBehaviour {

    private TileMap _tileMap;
    private TileFactory _tileFactory;

    public void Start() {
        _tileMap = new TileMap(1000);
        _tileFactory = new TileFactory(_tileMap);
        Camera.main.gameObject.AddComponent<TileMapCameraWatcher>().TileMap = _tileMap;
    }

}