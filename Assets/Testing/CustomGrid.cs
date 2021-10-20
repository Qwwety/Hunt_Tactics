using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class CustomGrid : MonoBehaviour
{
    [Header("Main")]
    private int Height;
    private int Width;
    private int[,] GridArray; 
    private Grid Grid;
    private Tile[] Tiles;
    private Tilemap FloorTilemap;
    private Tilemap ObjectTilemap;
    //[Header("HeatMap")]
    //private const int HeatMapMaxValue = 100;
    //private const int HeatMapMinValue = 0;



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
                // GetText(GridArray[x, y].ToString(), null, GetGridPosition(x,y), 5, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, 50000);
                //Debug.DrawLine(XYToIsometric(x, y), XYToIsometric(x, y + 1), Color.white, 100f);// отрисовка линий
                //Debug.DrawLine(XYToIsometric(x, y), XYToIsometric(x + 1, y), Color.white, 100f);
            }

        }
        //Debug.DrawLine(XYToIsometric(0, Hight), XYToIsometric(Width, Hight), Color.white, 100f);
        //Debug.DrawLine(XYToIsometric(Width, 0), XYToIsometric(Width, Hight), Color.white, 100f);//дорисовкалинни в самом верху и справа

        //SetValue(1, 1, 40);
    }

    public int GetWidth()
    {
        return Width;
    }
    public int GetHeight()
    {
        return Height;
    }
    public Grid GetGrid()
    {
        return Grid;
    }

    public Tilemap GetFloorTileMap()
    {
        return FloorTilemap;
    }
    public Tilemap GetObjectTileMap()
    {
        return ObjectTilemap;
    }


    private void SetFloorTile(int x, int y)
    {
        for (int i=0;i< Tiles.Length; i++)
        {
            FloorTilemap.SetTile(new Vector3Int(x,y,0), Tiles[1]);
        }
    }
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

    public void AddToObjectTile(int x, int y, Tile tile)
    {
        Vector3Int MousePosition = new Vector3Int(x, y, 0);
        if (MousePosition.x >= 0 && MousePosition.y >= 0 && MousePosition.x < Width && MousePosition.y < Height)
        {
            ObjectTilemap.SetTile(MousePosition, tile);
        }
    }

    private Vector3Int GetGridPosition(int x, int y)
    {
        return new Vector3Int(x, y, 0);
        //return GetGridObject(x, y).x;
    }

    public Vector3Int GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height)
        {
            return new Vector3Int(x, y, 0);//Grid.WorldToCell(new Vector3Int(x, y, 0));
            //return Grid.WorldToCell(new Vector3Int(x, y, 0));
        }
        else
        {
            return default(Vector3Int);
        }
    }

    //public void SetHeatMap(Vector3Int MousePosition,int range, Tile tile)
    //{
    //    for (int x = 0; x < range; x++)
    //    {
    //        for (int y = 0; y < range - x; y++)
    //        {
    //            AddToFloorTileMap(MousePosition.x + x, MousePosition.y + y, tile);
    //            if (x != 0)
    //            {
    //                AddToFloorTileMap(MousePosition.x - x, MousePosition.y + y, tile);
    //            }
    //            if (y != 0)
    //            {
    //                AddToFloorTileMap(MousePosition.x + x, MousePosition.y - y, tile);
    //                if (x != 0)
    //                {
    //                    AddToFloorTileMap(MousePosition.x - x, MousePosition.y - y, tile);
    //                }
    //            }
    //        }
    //    }
    //}



    //public GameObject SetTile(Vector2 Position, GameObject Tile)
    //{
    //    GameObject gameObject = instance(Tile);
    //    gameObject.transform.position = Position;

    //    return gameObject;
    //}

    //private Vector2 XYToIsometric(float x, float y)// переводит из координат grid в мировые
    //{
    //    return (new Vector2(x + y, 0.5f * (y - x)) * CellSize) + OriginPosition;
    //}
    //private Vector2 IsometricToXY(float x, float y)// переводит из координат grid в мировые
    //{
    //    return new Vector2(.5f * x - y, .5f * x + y);
    //}

    //public void SetGridObject(int x, int y, TGridObject value)
    //{
    //    if (x >= 0 && y >= 0 && x < Width && y < Hight)
    //    {
    //        GridArray[x, y] = value;
    //        //DebugTextArray[x, y].text = GridArray[x, y].ToString();
    //    }

    //}

    //public void TriggerGridObjectChahed(int x, int y)
    //{
    //    //if (OnGridValueChanged != null)
    //    //{
    //    //    OnG
    //    //}
    //}

    //private Vector2Int GetXY(Vector3 WorldPosition)
    //{
    //    int x = Mathf.FloorToInt(IsometricToXY(WorldPosition.x - OriginPosition.x, WorldPosition.y - OriginPosition.y).x / CellSize);
    //    int y = Mathf.FloorToInt(IsometricToXY(WorldPosition.x - OriginPosition.x, WorldPosition.y - OriginPosition.y).y / CellSize);
    //    return new Vector2Int(x, y);
    //}
    //public void SetGridObject(Vector3 WorldPosition, TGridObject value)
    //{
    //    Vector2Int TakeXY = GetXY(WorldPosition);
    //    SetGridObject(TakeXY.x, TakeXY.y, value);
    //}



    //}
    //public void GetGridObject(Vector3 GridPosition)
    //{
    //    return GetGridObject(GetXY(GridPosition).x, GetXY(GridPosition).y);
    //}

    //public void AddValue(int x, int y, int value)
    //{
    //    SetValue(x, y, GetValue(x, y) + value);
    //}

    //public void AddValue(Vector3 WorldPositin, int value, int range)
    //{
    //    for (int x = 0; x < range; x++)
    //    {
    //        for (int y = 0; y < range - x; y++)
    //        {
    //            AddValue(GetXY(WorldPositin).x + x, GetXY(WorldPositin).y + y, value);
    //            if (x != 0)
    //            {
    //                AddValue(GetXY(WorldPositin).x - x, GetXY(WorldPositin).y + y, value);
    //            }
    //            if (y != 0)
    //            {
    //                AddValue(GetXY(WorldPositin).x + x, GetXY(WorldPositin).y - y, value);
    //                if (x != 0)
    //                {
    //                    AddValue(GetXY(WorldPositin).x - x, GetXY(WorldPositin).y - y, value);
    //                }
    //            }
    //        }
    //    }

    //}
    //public TextMesh GetText(string text, Transform parent, Vector3 localPosition, int FrontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int SortingOrder)
    //{
    //    GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
    //    Transform transform = gameObject.transform;
    //    transform.SetParent(parent, false);
    //    transform.localPosition = localPosition;
    //    TextMesh textMesh = gameObject.GetComponent<TextMesh>();
    //    textMesh.anchor = textAnchor;
    //    textMesh.alignment = textAlignment;
    //    textMesh.text = text;
    //    textMesh.fontSize = FrontSize;
    //    textMesh.color = color;
    //    textMesh.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
    //    return textMesh;
    //}
}
