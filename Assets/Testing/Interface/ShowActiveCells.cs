using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowActiveCells : MonoBehaviour
{
    [SerializeField] private Tile WalkTile;
    private Tilemap InterfaceTilemap;

    private void Start()
    {
        InterfaceTilemap = GetComponent<Tilemap>();
    }
    public void ShowWalkableTile(Vector3Int TilePosition)
    {
        InterfaceTilemap.SetTile(TilePosition, WalkTile);
    }

    [SerializeField] private float price;
    private float Price
    {
        get
        {
            return price;
        }
        set
        {
            price = SetPriceByFraction(2);
        }
    }


    private float SetPriceByFraction(int index)
    {
        float a = price;
        switch (index)
        {
            case 1:
                a /= 2;
                return a;
            case 2:
                a *= 2;
                return a;
            case 3:
                a *= 1;
                return a;
        }
        return a;

    }

    private int SetPrice(int MaxPrice)
    {
        int IntermediatePrice = MaxPrice; //(int)UnityEngine.Random.RandomRange(0, MaxPrice);
        return IntermediatePrice;
    }

}
