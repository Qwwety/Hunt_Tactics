using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Testing : MonoBehaviour
{
    [SerializeField] private int Height;
    [SerializeField] private int Width;
    private Grid Grid;
    [SerializeField] private Tile[] Tiles;
    [SerializeField] private Tilemap Tilemap;
    CustomGrid customGrid;
    PathFinding PathFinding;
    Vector3Int Pos;
    void Start()
    {
        Grid = gameObject.GetComponent<Grid>();
        customGrid = new CustomGrid(Width, Height, Grid, Tiles, Tilemap);
        PathFinding = new PathFinding(customGrid);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //customGrid.SetHeatMap(GetMousePos(),4, Tiles[0]);
            //Pos = GetMousePos();
            Debug.Log("Pos is:" + Pos);
            //PH.SetPath(Tiles[0]);
            //PathFinding.GetFCost(Pos);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Pos = GetMousePos();
            if (Pos.x >= 0 && Pos.y >= 0 && Pos.x < Width && Pos.y < Height)
            {
                Pos = GetMousePos();

                List<PathNode> Path = PathFinding.FindPath(new Vector3Int(0, 0, 0), Pos);
                Debug.Log("Path is" + Path);// тут Null
                if (Path != null)
                {
                    foreach (PathNode PathNode in Path)
                    {
                        for (int i = 0; i < Path.Count - 1; i++)
                        {
                            customGrid.AddToTileMap(new Vector3Int(Path[i].x, Path[i].y, 0).x, new Vector3Int(Path[i].x, Path[i].y, 0).y, Tiles[0]);
                            Debug.Log("Done");
                        }

                    }
                }

            }
        }
    }
    Vector3Int GetMousePos()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return Grid.WorldToCell(mouseWorldPos);
    }



}
