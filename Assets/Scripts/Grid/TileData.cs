#region Summary
/// <summary>
/// Defines the types of tiles in the game and the data structure for each tile.
/// The TileType enum categorizes the different types of tiles, such as Empty, Occupied, NPCCamp, and Resource.
/// The TileData class contains the grid coordinates (gridX and gridY) and the type of tile (tileType) for each tile in the grid.
/// This structure allows for easy management and access to tile information, enabling the game to determine how to interact with each tile based on its type and position in the grid.
/// The TileData class can be extended in the future to include additional properties or methods as needed, such as references to game objects, resources, or NPCs associated with the tile.
/// Overall, this code provides a foundational structure for representing and managing the tiles in the game, allowing for efficient access and manipulation of tile data as the game progresses.
/// </summary>
#endregion
#region Phase 1 Sprint 2 - Tile Data Structure
//public enum TileType
//{
//    Empty,
//    Occupied,
//    NPCCamp,
//    Resource
//}

//public class TileData
//{
//    public int gridX;
//    public int gridY;
//    public TileType tileType;

//    public TileData(int x, int y)
//    {
//        gridX = x;
//        gridY = y;
//        tileType = TileType.Empty;
//    }
//}
#endregion
#region Phase 1 Sprint 2 - Tile Data Structure
public enum TileType
{
    Empty,
    Occupied,
    NPCCamp,
    Resource
}

public class TileData
{
    public int gridX;
    public int gridY;
    public TileType tileType;
    public BuildingState occupant;

    public TileData(int x, int y)
    {
        gridX = x;
        gridY = y;
        tileType = TileType.Empty;
    }
}
#endregion