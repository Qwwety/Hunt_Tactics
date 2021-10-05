using UnityEngine;

public class TileMap : MonoBehaviour
{
    public TileType[] tileTypes;

    int[,] tiles;
   [SerializeField] private int XSize;
   [SerializeField] private int YSize;
    [SerializeField] private float CellSize;

    void Start()
    {
        tiles = new int[XSize, YSize];

        

        tiles[4, 4] = 0;
        tiles[5, 4] = 0;
        tiles[6, 4] = 0;
        tiles[7, 4] = 0;
        tiles[8, 4] = 0;

        tiles[4, 5] = 1;
        tiles[4, 6] = 1;
        tiles[4, 5] = 1;
        tiles[4, 6] = 1;
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < XSize; x++)
        {
            for (int y = 0; y < YSize; y++)
            {
                TileType tileType = tileTypes[tiles[x, y]];
                tiles[x, y] = 0;
                Instantiate(tileType.Tile, XYToIsometric(x,y), Quaternion.identity);
            }
        }
    }

    private Vector2 XYToIsometric(float x, float y)// переводит из координат grid в мировые
    {
        return new Vector2(x + y, 0.5f * (y - x)* CellSize);
    }
}
