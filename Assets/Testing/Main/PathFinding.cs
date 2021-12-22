using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int MoveStraightCost = 10;
    private const int MoveDiagonalCost = 14;

    private CustomGrid customGrid;

    private PathCell StartCell;
    private PathCell EndCell;

    private Vector3Int[] CharacterPositions;

    public PathFinding(CustomGrid customGrid, Vector3Int[] CharacterPositions)
    {
        this.customGrid = customGrid;
        this.CharacterPositions = CharacterPositions;
    }
    public void UpdateCharctersPositions(Vector3Int[] CharacterPositions)
    {
        this.CharacterPositions = CharacterPositions;
    }

    /// <summary>
    /// Находит путь
    /// </summary>
    /// <param name="CurentPosition"></param>
    /// <param name="TargetPosition"></param>
    /// <returns></returns>
    public List<Vector3Int> FindPath(Vector3Int CurentPosition, Vector3Int TargetPosition)
    {
        #region SetStartValues
        StartCell = GetCell(CurentPosition.x, CurentPosition.y);
        EndCell = GetCell(TargetPosition.x, TargetPosition.y);

        if (!IsCellWakable(EndCell))
        {
            return null; 
        }

        List<Vector3Int> TentativePath = new List<Vector3Int>(20);//Сорежит все клетки котрые использовал скрипт для нахождения Пути
        List<Vector3Int> FinalPath = new List<Vector3Int>(20);

        for (int x = 0; x < customGrid.GetWidth(); x++)
        {
            for (int y = 0; y < customGrid.GetHeight(); y++)
            {
                PathCell PathCell = GetCell(x, y);
                PathCell.gCost = int.MaxValue;
                PathCell.CalculatefCost();
                PathCell.CameFromCell = null;
            }
        }

        StartCell.gCost = 0;
        StartCell.hCost = CalculateHCost(StartCell, EndCell);
        StartCell.CalculatefCost();
        #endregion

        #region CalculatePath
        FinalPath.Add(StartCell.GetCellPosition());
        TentativePath.Add(StartCell.GetCellPosition());

        PathCell CurrentCell = StartCell;

        int TentativeGCost;
        int FCost;

        while (!TentativePath.Contains(EndCell.GetCellPosition()))
        {
            List<PathCell> CurrentCells = new List<PathCell>(8);// клетки текущего масива
            foreach (PathCell NeighbourCell in GetNeighbourList(CurrentCell))
            {
                if (!TentativePath.Contains(NeighbourCell.GetCellPosition()))
                {
                    TentativeGCost = CalculateHCost(CurrentCell, NeighbourCell);// Находит GCost, написано,что находит H, но в данном случаее это G
                    FCost = CalculateHCost(NeighbourCell, EndCell) + TentativeGCost;

                    NeighbourCell.fCost = FCost;
                    NeighbourCell.hCost = FCost - TentativeGCost;
                    NeighbourCell.gCost = TentativeGCost;
                    NeighbourCell.IsWalkable = IsCellWakable(NeighbourCell);

                    if (NeighbourCell.IsWalkable == true)
                    {
                        CurrentCells.Add(NeighbourCell);
                        TentativePath.Add(NeighbourCell.GetCellPosition());
                    }

                }
            }

            CurrentCell = GetLowestFCostCell(CurrentCells);
            FinalPath.Add(CurrentCell.GetCellPosition());

        }
        return FinalPath;
        #endregion
    }

    /// <summary>
    /// Возвращвет Клетку по параметрам X и Y
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PathCell GetCell(int x, int y)
    {
        return new PathCell(x, y);
    }

    /// <summary>
    /// Расчет цены самого короткого пути
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    /// <returns></returns>
    private int CalculateHCost(PathCell Start, PathCell End)
    {
        int xDistance = Mathf.Abs(Start.x - End.x);
        int yDistance = Mathf.Abs(Start.y - End.y);
        int Remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * Remaining;
    }

    /// <summary>
    /// Вовращает самую дешовую коетку по F
    /// </summary>
    /// <param name="pathCellList"></param>
    /// <returns></returns>
    private PathCell GetLowestFCostCell(List<PathCell> pathCellList)
    {
        PathCell lowestFCostCell = pathCellList[0];
        //List<PathCell> ListOfLowest= new List<PathCell>();
        for (int i = 1; i < pathCellList.Count; i++)
        {
            if (pathCellList[i].fCost == lowestFCostCell.fCost)
            {
                lowestFCostCell = GetLowestHCost(pathCellList[i], lowestFCostCell);
                //ListOfLowest.Add(lowestFCostCell);

            }
            else if (pathCellList[i].fCost < lowestFCostCell.fCost)
            {
                lowestFCostCell = pathCellList[i];
            }
        }
        return lowestFCostCell;
    }

    /// <summary>
    /// Вовращает самую дешовую коетку по H
    /// </summary>
    /// <param name="Fist"></param>
    /// <param name="Second"></param>
    /// <returns></returns>
    private PathCell GetLowestHCost(PathCell Fist, PathCell Second)
    {
        if (Fist.hCost > Second.hCost)
        {
            return Second;
        }
        return Fist;
    }

    /// <summary>
    /// Находит все соседнии клетки от текущей 
    /// </summary>
    /// <param name="CurrentCell"></param>
    /// <returns></returns>
    private List<PathCell> GetNeighbourList(PathCell CurrentCell)
    {
        List<PathCell> neighbourList = new List<PathCell>();
        if (CurrentCell.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetCell(CurrentCell.x - 1, CurrentCell.y));
            //Left Down
            if (CurrentCell.y - 1 >= 0)
            {
                neighbourList.Add(GetCell(CurrentCell.x - 1, CurrentCell.y - 1));
            }
            //Left Up
            if (CurrentCell.y + 1 < customGrid.GetHeight())
            {
                neighbourList.Add(GetCell(CurrentCell.x - 1, CurrentCell.y + 1));
            }
        }
        if (CurrentCell.x + 1 < customGrid.GetWidth())
        {   //Right
            neighbourList.Add(GetCell(CurrentCell.x + 1, CurrentCell.y));
            //Right Down
            if (CurrentCell.y - 1 >= 0)
            {
                neighbourList.Add(GetCell(CurrentCell.x + 1, CurrentCell.y - 1));
            }
            //Right Up`
            if (CurrentCell.y + 1 < customGrid.GetHeight())
            {
                neighbourList.Add(GetCell(CurrentCell.x + 1, CurrentCell.y + 1));
            }
        }
        //Down
        if (CurrentCell.y - 1 >= 0)
        {
            neighbourList.Add(GetCell(CurrentCell.x, CurrentCell.y - 1));
        }
        //Up
        if (CurrentCell.y + 1 < customGrid.GetHeight())
        {
            neighbourList.Add(GetCell(CurrentCell.x, CurrentCell.y + 1));
        }
        return neighbourList;
    }

    /// <summary>
    /// Проверяте, можно ли ходить по даннйо клетке
    /// </summary>
    /// <param name="CurrentCell"></param>
    /// <returns></returns>
    private bool IsCellWakable(PathCell CurrentCell)
    {
        if (customGrid.GetFloorTileMap().GetTile(CurrentCell.GetCellPosition()
            ) 
            && !customGrid.GetObjectTileMap().GetTile(CurrentCell.GetCellPosition()) 

            && IsCellAllreadyOccupied(CurrentCell) == false

            )

        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsCellAllreadyOccupied(PathCell CurrentCell)
    {
        foreach (Vector3Int Char in CharacterPositions)
        {
            if (CurrentCell.GetCellPosition() == new Vector3Int(Char.x, Char.y,0))
            {
                return true;
                break;
            }
            
        }
        return false;
    }
}
