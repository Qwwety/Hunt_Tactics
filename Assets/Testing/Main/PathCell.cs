using UnityEngine;

public class PathCell
{
    public Grid Grid;
    public int x;
    public int y;

    public int gCost;// Cost for single stap
    public int hCost;//Heuristic cost shortest path to aim (ignore non walkable objects )
    public int fCost; //g+h


    public bool IsWalkable;
    public PathCell CameFromCell;

    public PathCell( int x, int y)
    {
        this.x = x;
        this.y = y;
        IsWalkable = true;
    }

    /// <summary>
    /// Возвращает положение Клетки по X и Y, Z =0 всегда
    /// </summary>
    /// <returns></returns>
    public Vector3Int GetCellPosition()
    {
        return new Vector3Int(x, y, 0);
    }

    /// <summary>
    /// Возвращате FCost данной клетки 
    /// </summary>
    /// <returns></returns>
    public int CalculatefCost()
    {
        fCost = gCost + hCost;
        return fCost;
    }
}
