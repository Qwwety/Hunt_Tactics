using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Testing : MonoBehaviour
{
    [SerializeField] private int Height;
    [SerializeField] private int Width;
    [SerializeField] private Tile[] Tiles;
    [SerializeField] private Tilemap FloorTilemap;
    [SerializeField] private Tilemap ObjectTilemap;
    private Grid Grid;
    private CustomGrid customGrid;
    private PathFinding PathFinding;
    private Vector3Int Pos;
    void Start()
    {

        Grid = gameObject.GetComponent<Grid>();
        customGrid = new CustomGrid(Width, Height, Grid, Tiles, FloorTilemap, ObjectTilemap);
        PathFinding = new PathFinding(customGrid);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pos = GetMousePos();
            customGrid.AddToObjectTile(Pos.x, Pos.y, Tiles[2]);
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
                        customGrid.AddToFloorTileMap(new Vector3Int(PathNode.x, PathNode.y, 0).x, new Vector3Int(PathNode.x, PathNode.y, 0).y, Tiles[0]);
                        Debug.Log("Done");
                        //for (int i = 0; i < Path.Count - 1; i++)
                        //{
                        //    customGrid.AddToFloorTileMap(new Vector3Int(Path[i].x, Path[i].y, 0).x, new Vector3Int(Path[i].x, Path[i].y, 0).y, Tiles[0]);
                        //    Debug.Log("Done");
                        //}

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
