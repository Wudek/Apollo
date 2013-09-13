public struct Tile {
    private readonly int _x;
    private readonly int _z;

    public Tile(int x, int z) {
        _x = x;
        _z = z;
    }

    public int Z {
        get { return _z; }
    }

    public int X {
        get { return _x; }
    }
}
