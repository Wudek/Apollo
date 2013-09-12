using System.Collections.Generic;
using UnityEngine;

public class TileMap {
    private readonly int _size;
    private Tile[,] _tiles;

    public delegate void TileCreatedHandler(int x, int z, Tile tile);

    public event TileCreatedHandler TileCreated;

    private void OnTileCreated(int x, int z, Tile tile) {
        var handler = TileCreated;
        if (handler != null)
            handler(x, z, tile);
    }

    public TileMap(int size) {
        _size = size;
        _tiles = new Tile[size, size];
    }

    public Tile getTile(int x, int z) {
        if (validateCoordinate(x, z)) {
            return _tiles[x, z] ?? createTile(x, z);
        } else {
            return null;
        }
    }

    private Tile createTile(int x, int z) {
        var tile = new Tile();
        _tiles[x, z] = tile;
        OnTileCreated(x, z, tile);
        return tile;
    }

    private bool validateCoordinate(int x, int z) {
        return x >= 0 && z >= 0 && x <= _size && z <= _size;
    }
}