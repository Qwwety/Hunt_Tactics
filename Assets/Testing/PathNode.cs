using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    public Grid Grid;
    public int x;
    public int y;

    public int gCost;// Cost for single stap
    public int hCost;//Heuristic cost shortest path to aim (ignore non walkable objects )
    public int fCost; //g+h

    public bool IsWalkable;
    public PathNode CameFromNode;

    public PathNode( int x, int y)
    {
        this.x = x;
        this.y = y;
        IsWalkable = true;
    }
    //public T GetNode<T>(T dfsdf )
    //{
    //    return dfsdf;
    //}
    public Vector3Int GetNode()
    {
        return new Vector3Int(x, y, 0);
    }
    public int CalculatefCost()
    {
        fCost = gCost + hCost;
        return fCost;
    }
}
