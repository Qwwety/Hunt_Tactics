using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class CustomGrid
{
    [Header("Main")]
    private int Height;
    private int Width;
    private int[,] GridArray; 
    [Space]
    private Grid Grid;
    [Space]
    private Tile[] Tiles;
    [Space]
    private Tilemap FloorTilemap;
    private Tilemap ObjectTilemap;
     


    public CustomGrid(int Width, int Height, Grid Grid, Tile[] Tiles, Tilemap FloorTilemap, Tilemap ObjectTilemap)
    {
        this.Width = Width;
        this.Height = Height;
        this.Grid = Grid;
        this.Tiles = Tiles;
        this.FloorTilemap = FloorTilemap;
        this.ObjectTilemap = ObjectTilemap;
        GridArray = new int[Width, Height];

        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int y = 0; y < GridArray.GetLength(1); y++)
            {
                SetFloorTile(x, y);
            }
        }
    }
    /// <summary>
    /// Возвращает Ширину
    /// </summary>
    /// <returns></returns>
    public int GetWidth()
    {
        return Width;
    }
    public int GetHeight()
    {
        return Height;
    }

    /// <summary>
    /// Возвращете карту земли
    /// </summary>
    /// <returns></returns>
    public Tilemap GetFloorTileMap()
    {
        return FloorTilemap;
    }
    /// <summary>
    /// Возвращете карту объектов
    /// </summary>
    /// <returns></returns>
    public Tilemap GetObjectTileMap()
    {
        return ObjectTilemap;
    }
    /// <summary>
    /// Создате все тайлы земли
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void SetFloorTile(int x, int y)
    {
        for (int i=0;i< Tiles.Length; i++)
        {
            FloorTilemap.SetTile(new Vector3Int(x,y,0), Tiles[1]);
        }
    }
    /// <summary>
    /// Добавляет на карту земли новые тайлы
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="tile"></param>
    public void AddToFloorTileMap(int x,int y,Tile tile)
    {
        Vector3Int MousePosition = new Vector3Int(x, y,0);
        if (MousePosition.x>=0 && MousePosition.y >= 0 && MousePosition.x < Width && MousePosition.y< Height)
        {
            FloorTilemap.SetTile(MousePosition, tile);
        }
    }

    public void DelFromFloorTileMap(int x, int y)
    {
        Vector3Int MousePosition = new Vector3Int(x, y, 0);
        if (MousePosition.x >= 0 && MousePosition.y >= 0 && MousePosition.x < Width && MousePosition.y < Height)
        {
            FloorTilemap.SetTile(MousePosition, null);
        }
    }
    /// <summary>
    /// Добавляет тайл на карту объектов
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="tile"></param>
    public void AddToObjectTile(int x, int y, Tile tile)
    {
        Vector3Int MousePosition = new Vector3Int(x, y, 0);
        if (MousePosition.x >= 0 && MousePosition.y >= 0 && MousePosition.x < Width && MousePosition.y < Height)
        {
            ObjectTilemap.SetTile(MousePosition, tile);
        }
    }

    public void AddToTileMap(int x, int y, Tile tile, Tilemap Tilemap)
    {
        Vector3Int MousePosition = new Vector3Int(x, y, 0);
        if (MousePosition.x >= 0 && MousePosition.y >= 0 && MousePosition.x < Width && MousePosition.y < Height)
        {
            Tilemap.SetTile(MousePosition, tile);
        }
    }
}
