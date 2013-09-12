using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Debug = UnityEngine.Debug;

public class TileFactory {
    private TileMap _tileMap;

    public TileFactory(TileMap tileMap) {
        attachTileMap(tileMap);
    }

    public void attachTileMap(TileMap tileMap) {
        if (_tileMap != null) {
            _tileMap.TileCreated -= TileMapOnTileCreated;
        }
        _tileMap = tileMap;
        _tileMap.TileCreated += TileMapOnTileCreated;
    }

    private void TileMapOnTileCreated(int i, int j, Tile tile) {
        Debug.Log(i + ", " + j + " created");
    }
}

