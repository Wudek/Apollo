public class TileMap {
    public delegate void TileCreatedHandler(int x, int z, Tile tile);

    private readonly int _size;
    private readonly Tile[] _tiles;

    public TileMap(int size) {
        _size = size;
        _tiles = new Tile[size*size];
        Initialize();
    }

    public Tile[] Tiles {
        get { return _tiles; }
    }

    private void Initialize() {
        var index = 0;
        for (var i = 0; i < _size; i++) {
            for (var j = 0; j < _size; j++) {
                Tiles[index] = new Tile(i, j);
                index++;
            }
        }
    }

    public event TileCreatedHandler TileCreated;

    private int GetIndex(int x, int z) {
        return x*_size + z;
    }

    private void OnTileCreated(int x, int z, Tile tile) {
        var handler = TileCreated;
        if (handler != null)
            handler(x, z, tile);
    }

    public Tile GetTile(int x, int z) {
        return Tiles[GetIndex(x, z)];
    }

    private Tile CreateTile(int x, int z) {
        var tile = new Tile();
        Tiles[GetIndex(x, z)] = tile;
        OnTileCreated(x, z, tile);
        return tile;
    }


    public bool AreValidCoordinates(int x, int z) {
        return x >= 0 && z >= 0 && x < _size && z < _size;
    }
}