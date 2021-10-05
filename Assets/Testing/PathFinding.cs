using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding 
{
    private const int MoveStraightCost = 10;
    private const int MoveDiagonalCost = 14;

    private List<PathNode> OpenList;
    private List<PathNode> ClosedList;
    private CustomGrid customGrid;

    private PathNode StartNode;
    private PathNode EndNode;

    public PathFinding(CustomGrid customGrid)
    {
        this.customGrid = customGrid;
    }
    public List<PathNode> FindPath(Vector3Int CurentPosition, Vector3Int TargetPosition)
    {
        StartNode = GetNode(CurentPosition.x, CurentPosition.y);
        EndNode = GetNode(TargetPosition.x, TargetPosition.y);

        OpenList = new List<PathNode> { StartNode };
        ClosedList = new List<PathNode>();
        for (int x = 0; x < customGrid.GetWidth(); x++)
        {
            for (int y = 0; y < customGrid.GetHeight(); y++)
            {
                PathNode pathNode = GetNode(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculatefCost();
                pathNode.CameFromNode = null;
            }
        }// проверить

        StartNode.gCost = 0;
        StartNode.hCost = CalculateHCost(StartNode, EndNode);
        StartNode.CalculatefCost();

        DFD();


        //while (OpenList.Count > 0)
        //{
        //    PathNode CurrentNode = GetLowestFCostNode(OpenList);
        //    if (CurrentNode == EndNode)
        //    {
        //        //Reached Final Node
        //        Debug.Log("Ne pusto");
        //        return CalculatePath(EndNode);
        //    }
        //    OpenList.Remove(CurrentNode);
        //    ClosedList.Add(CurrentNode);

        //    foreach (PathNode NeighbourNode in GetNeighbourList(CurrentNode))
        //    {
        //        if (ClosedList.Contains(NeighbourNode)) continue;

        //        int tentativeGCost = CurrentNode.gCost + CalculateHCost(CurrentNode, NeighbourNode);
        //        if (tentativeGCost < NeighbourNode.gCost)
        //        {
        //            NeighbourNode.CameFromNode = CurrentNode;
        //            NeighbourNode.gCost = tentativeGCost;
        //            NeighbourNode.hCost = CalculateHCost(CurrentNode, EndNode);
        //            NeighbourNode.CalculatefCost();
        //            if (!OpenList.Contains(NeighbourNode))
        //            {
        //                OpenList.Add(NeighbourNode);
        //            }

        //        }
        //    }// Ошибка скорее всего тут
        //}
        Debug.Log("Pust0");
        return DFD();
    }
    public List<PathNode> DFD()
    {
        List<PathNode> Take = new List<PathNode>();
        List<PathNode> TentativePath = new List<PathNode>();
        TentativePath.Add(StartNode);
        PathNode CurentNode = StartNode;
        while (CurentNode.GetNode() != EndNode.GetNode())
        {
            List<PathNode> Current = new List<PathNode>(); ;
            foreach (PathNode NeighbourNode in GetNeighbourList(CurentNode))
            {
                int tentativeGCost = CalculateHCost(CurentNode, NeighbourNode);// Находит GCost
                int FCost =  CalculateHCost(NeighbourNode, EndNode) + tentativeGCost;// Fcost //+ CalculateHCost(RE, EndNode); CurentNode.gCost +
                NeighbourNode.fCost = FCost;
                NeighbourNode.hCost = FCost - tentativeGCost;
                NeighbourNode.gCost = tentativeGCost;
                if (!Take.Contains(NeighbourNode)) 
                {
                    Current.Add(NeighbourNode);
                    Take.Add(NeighbourNode);
                }
            }
            CurentNode = GetLowestFCostNode(Current);
            TentativePath.Add(CurentNode);
        }
        TentativePath.Add(EndNode);
        return TentativePath;
    }
    private PathNode GetNode(int x, int y)
    {
        return new PathNode(x, y);
    }// Все кул
    private int CalculateHCost(PathNode Start, PathNode End)
    {
        int xDistance = Mathf.Abs(Start.x - End.x);
        int yDistance = Mathf.Abs(Start.y - End.y);
        int Remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * Remaining;
    } 
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        //List<PathNode> ListOfLowest= new List<PathNode>();
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost< lowestFCostNode.fCost)
            {
                lowestFCostNode=pathNodeList[i];
                //ListOfLowest.Add(lowestFCostNode);

            }
        }
        return lowestFCostNode;
    } // Все должно быть кул
    private List<PathNode> CalculatePath(PathNode EndNode)// Не доходит до этой функциии
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(EndNode);
        PathNode currentNode = EndNode;
        while (currentNode.CameFromNode != null)
        {
            path.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
            //Debug.Log("path.Count"+path.Count);
        }
        path.Reverse();
        return path;
    }
    private List<PathNode> GetNeighbourList(PathNode CurrentNode)  // Тут всего 3 у  neighbourList должно быть больше
    {
        List<PathNode> neighbourList = new List<PathNode>();
        if (CurrentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(CurrentNode.x - 1, CurrentNode.y));
            //Left Down
            if (CurrentNode.y - 1 >= 0) 
            { 
                neighbourList.Add(GetNode(CurrentNode.x - 1, CurrentNode.y - 1)); 
            }
            //Left Up
            if (CurrentNode.y + 1 < customGrid.GetHeight())
            {
                neighbourList.Add(GetNode(CurrentNode.x - 1, CurrentNode.y + 1));
            }
        }
        if (CurrentNode.x + 1 < customGrid.GetWidth())
        {   //Right
            neighbourList.Add(GetNode(CurrentNode.x + 1, CurrentNode.y));
            //Right Down
            if (CurrentNode.y - 1 >= 0)
            {
                neighbourList.Add(GetNode(CurrentNode.x + 1, CurrentNode.y - 1));
            }
            //Right Up`
            if (CurrentNode.y + 1 < customGrid.GetHeight())
            {
                neighbourList.Add(GetNode(CurrentNode.x + 1, CurrentNode.y + 1));
            }
        }
        //Down
        if (CurrentNode.y - 1 >= 0)
        {
            neighbourList.Add(GetNode(CurrentNode.x, CurrentNode.y - 1));
        }
        //Up
        if (CurrentNode.y + 1 < customGrid.GetHeight())
        {
            neighbourList.Add(GetNode(CurrentNode.x, CurrentNode.y + 1));
        } 
        return neighbourList;
    }
}
