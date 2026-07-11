#region Summary
/// <summary>
/// Represents a grid of tiles in the game.
/// Manages the underlying data structure of the tiles and provides methods to access individual tiles.
/// </summary>
#endregion
#region Phase 1 Sprint 2 - Tile Grid
public class TileGrid
{
    public int width;
    public int height;

    private TileData[,] tiles;

    public TileGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        tiles = new TileData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new TileData(x, y);
            }
        }
    }

    public TileData GetTile(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;

        return tiles[x, y];
    }
}
#endregion