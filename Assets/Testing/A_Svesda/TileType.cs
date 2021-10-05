using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType
{
    public string Name;
    public GameObject Tile;


   
    private Vector2 IsometricToXY(float x, float y)// переводит из координат grid в мировые
    {
        return new Vector2(.5f * x - y, .5f * x + y);
    }
}