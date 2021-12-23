using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Testing : MonoBehaviour //Сделать меню и запихнуть- назначение цены, как и саму цену, тип специалиста/класс, позитивные/негативные парки в интерфейс/ы, как и их назначение
                                     // Чтобы враги могли тоже наследоваться от этого класа
                                     // сделать возможнозть атаковать 
                                     //
                                     // крестик для возврата в меню и что бы при наведение, в хабе, здания выделялись
                                     // 
                                     //и мне не нравиться  OrderOfMovement- она нужна только для срабатывания ее конструктора и все, надо придумать как выкинуть ее
                                     //
                                     //давай,Кирилл, удачи
{
    public static Testing Instance;

    [Header("Grid")]
    [SerializeField] private int Height;
    [SerializeField] private int Width;

    [Header("Tiles")]
    [SerializeField] private Tile[] Tiles;
    [SerializeField] private Tile WakableTile;
    [SerializeField] private Tile AttackableTile;

    [Header("Tilemap")]
    [SerializeField] private Tilemap FloorTilemap;
    [SerializeField] private Tilemap ObjectTilemap;
    [SerializeField] private ShowActiveCells InterfaceTilemap;

    [Header("OrderOfMovement")]
    [SerializeField] private List<CharacterClass> CharactersClases;
    private Vector3Int[] CharactersPositions;

    private SetTheOrderOfMovement OrderOfMovement;


    [Header("Movement")]
    [SerializeField] private CharacterClass Character;
    [SerializeField] private CharacterMovement CharacterMovement;
    [SerializeField] private int MaxMovemetDistance;

    [Header("Attack")]
    [SerializeField] private CharacterShooting CharacterShooting;
    [SerializeField] private int MaxAttackRange;


    private Grid Grid;
    private CustomGrid customGrid;
    private PathFinding PathFinding;

    private Vector3Int Pos;
    private List<Vector3Int> PathCells = new List<Vector3Int>(30);

    private int CharacterNumber = 0;




    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        Grid = gameObject.GetComponent<Grid>();

        customGrid = new CustomGrid(Width, Height, Grid, Tiles, FloorTilemap, ObjectTilemap);

        GetCharacterPosition();

        PathFinding = new PathFinding(customGrid, CharactersPositions);

        OrderOfMovement = new SetTheOrderOfMovement(CharactersClases);

        SetNewCharcter();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& CharacterMovement.CanSetANewPoint() == false*/)
        {
            Pos = GetMouseosition();
            Vector3Int CharacterPosition = CharacterMovement.GetCharacterPosition();
            try
            {
                if (Pos.x < Width && Pos.y < Height && Pos.x >= 0 && Pos.y >= 0 && PathFinding.FindPath(CharacterPosition, new Vector3Int(Pos.x, Pos.y, 0)).Count <= MaxMovemetDistance)
                {
                    TrySetCells(PathFinding.FindPath(CharacterPosition, Pos));

                    GoToNextPosition();
                }
            }
            catch
            {
                Debug.Log("нельзая добраться до даннйо клетки");
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            Pos = GetMouseosition();
            Vector3Int CharacterPosition = CharacterMovement.GetCharacterPosition();
            Vector3 ClickPosition = Pos - CharacterPosition;
            //float Dist = Vector3Int.Distance(CharacterPosition, Pos);

            try
            {
                if (Pos.x < Width && Pos.y < Height && Pos.x >= 0 && Pos.y >= 0 && Pos!= CharacterPosition && Math.Abs(ClickPosition.x) <= MaxAttackRange && Math.Abs(ClickPosition.y) <= MaxAttackRange)
                {
                    TryToAttack(Pos);
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// Попытка проверить не пустой ли путь и если нет сохрнаить его в масив
    /// </summary>
    /// <param name="Cells"></param>
    private void TrySetCells(List<Vector3Int> Cells)
    {

        if (Cells.Count > 0)
        {
            PathCells.Clear();

            foreach (Vector3Int Cell in Cells)
            {
                PathCells.Add(Cell);
            }

        }
        else
        {
            return;
        }
    }

    private Vector3Int GetMouseosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return Grid.WorldToCell(mouseWorldPos);
    }

    /// <summary>
    /// Переход на следующию клетку пути
    /// </summary>
    public void GoToNextPosition()
    {
        TrySetNewPosition();
    }

    /// <summary>
    /// Попытка  передать персонажу клетку куда он должен пойти, в случае провала завершить путь 
    /// </summary>
    /// <param name="IndexPosition"></param>
    private void TrySetNewPosition()
    {
        int Index = 0;
        bool IsLastPointReached = false;

        while (IsLastPointReached != true)
        {
            Vector3 CharacterPosition = CharacterMovement.GetCharacterPosition();
            Vector3 TargetPos = new Vector3Int(PathCells[Index].x, PathCells[Index].y, PathCells[Index].z);

            if (Vector3.Distance(CharacterPosition, new Vector3Int(PathCells[Index].x, PathCells[Index].y, PathCells[Index].z)) > .1f)
            {
                
                Vector3 Converted = Grid.CellToLocal(new Vector3Int(PathCells[Index].x, PathCells[Index].y, PathCells[Index].z));
                Vector3 CellPosition = new Vector3(Converted.x, Converted.y, Converted.z);

                CharacterMovement.SetCellToMove(CellPosition);
            }

            else
            {
                Index++;

                if (Index >= PathCells.Count())
                {
                    IsLastPointReached = true;

                    GetCharacterPosition();

                    PathFinding.UpdateCharctersPositions(CharactersPositions);

                    SetNewCharcter();

                    break;

                }
            }
        }
    }


    /// <summary>
    /// Перенсти в отдельный скрипт
    /// </summary>
    private void MarkWalk()
    {
        Vector3Int CharacterPosition = CharacterMovement.GetCharacterPosition();

        for (int x = CharacterPosition.x - MaxMovemetDistance; x <= CharacterPosition.x + MaxMovemetDistance; x++)
        {
            for (int y = CharacterPosition.y - MaxMovemetDistance; y <= CharacterPosition.y + MaxMovemetDistance; y++)
            {
                if (PathFinding.FindPath(CharacterPosition, new Vector3Int(x, y, 0)) != null && PathFinding.FindPath(CharacterPosition, new Vector3Int(x, y, 0)).Count <= MaxMovemetDistance)
                {
                    customGrid.AddToTileMap(x, y, WakableTile, InterfaceTilemap.GetComponent<Tilemap>());
                }
            }
        }
    }

    /// <summary>
    /// Перенсти в отдельный скрипт
    /// </summary>
    private void MarkAttackable()
    {
        Vector3Int CharacterPosition = CharacterMovement.GetCharacterPosition();

        for (int x = CharacterPosition.x - MaxAttackRange; x <= CharacterPosition.x + MaxAttackRange; x++)
        {
            for (int y = CharacterPosition.y - MaxAttackRange; y <= CharacterPosition.y + MaxAttackRange; y++)
            {
                if (!customGrid.GetObjectTileMap().GetTile(new Vector3Int(x, y, 0))

                   && customGrid.GetFloorTileMap().GetTile(new Vector3Int(x, y, 0))

                   && new Vector3Int(x,y,0)!= CharacterPosition
                )

                {
                    customGrid.AddToTileMap(x, y, AttackableTile, InterfaceTilemap.GetComponent<Tilemap>());
                }
            }
        }
    }

    private void SetNewCharcter()
    {
        Character = GetNewPlayableCharacter();

        CharacterMovement = Character.GetComponent<CharacterMovement>();
        CharacterShooting= Character.GetComponent<CharacterShooting>();

        MaxMovemetDistance = Character.GetMaxMovementDistance();
        MaxAttackRange = Character.GetMaxAttackRange();

        InterfaceTilemap.GetComponent<Tilemap>().ClearAllTiles();

        MarkAttackable();
        MarkWalk();

    }

    private void ChekAliveCharackters()
    {
        for (int i = 0; i < CharactersClases.Count; i++)
        {
            if (CharactersClases[i].gameObject.activeSelf == false)
            {
                CharactersClases.RemoveAt(i);



                GetCharacterPosition();

                PathFinding.UpdateCharctersPositions(CharactersPositions);


            }
        }
    }

    private CharacterClass GetNewPlayableCharacter()
    {
        CharacterClass CurrentCharacter;

        if (Character != CharactersClases.Last())
        {
            int ChracterIndex = CharactersClases.IndexOf(Character);
            ChracterIndex++;
            CurrentCharacter = CharactersClases[ChracterIndex];
            
        }
        else
        {
            CurrentCharacter = CharactersClases.First();
        }


        return CurrentCharacter;
    }

    private void GetCharacterPosition()
    {
        CharactersPositions = new Vector3Int[CharactersClases.Count];

        for (int i = 0; i < CharactersClases.Count; i++)
        {
            CharactersPositions[i] = CharactersClases[i].GetComponent<CharacterMovement>().GetCharacterPosition();
        }
    }

    /// <summary>
    /// Написать нормально
    /// </summary>
    /// <param name="TargetPosition"></param>
    private void TryToAttack(Vector3Int TargetPosition)
    {
        if (CharactersPositions.Contains(TargetPosition))
        {
            for (int i = 0; i < CharactersClases.Count; i++)
            {
               if(TargetPosition == CharactersClases[i].GetComponent<CharacterMovement>().GetCharacterPosition() && TargetPosition!= CharacterMovement.GetCharacterPosition())
               {
                    CharacterShooting.DoDamage(CharactersClases[i],Character.GetDamage());
                    break;
               }
            }

            ChekAliveCharackters();

            SetNewCharcter();
        }
    }
}