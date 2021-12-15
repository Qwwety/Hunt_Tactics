using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Testing : MonoBehaviour //Короче надо сделать номальное перемешение, его пидорит шо пиздецй, на это като выиляет скорость ходьбы, но не особо, проверить ячейки в  PathCells, иб там проеб
                                     // и доделать смену персонажа
                                     // давай дурачок удачи
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
    private SetTheOrderOfMovement OrderOfMovement;

    [Header("Movement")]
    [SerializeField] private CharacterClass Character;
    [SerializeField] private CharacterMovement ControlledСharacter;
    [SerializeField] private int MaxMovemetDistance;

    [Header("Attack")]
    [SerializeField] private int MaxAttackRange;


    private Grid Grid;
    private CustomGrid customGrid;
    private PathFinding PathFinding;

    private Vector3Int Pos;
    private List<Vector3Int> PathCells = new List<Vector3Int>(30);
    private int Index = 0;

    private int Num = 0;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        Grid = gameObject.GetComponent<Grid>();

        customGrid = new CustomGrid(Width, Height, Grid, Tiles, FloorTilemap, ObjectTilemap);
        PathFinding = new PathFinding(customGrid);

        OrderOfMovement = GetComponent<SetTheOrderOfMovement>();
        OrderOfMovement.ActivateQueueSort();
        SetNewCharcter();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ControlledСharacter.CanSetANewPoint() == false)
        {
            Pos = GetMouseosition();
            Vector3Int CharacterPosition = ControlledСharacter.GetCharacterPosition();
            if (Pos.x < Width && Pos.y < Height && Pos.x >= 0 && Pos.y >= 0 && PathFinding.FindPath(CharacterPosition, new Vector3Int(Pos.x, Pos.y, 0)).Count <= MaxMovemetDistance)
            {
                TrySetCells(PathFinding.FindPath(ControlledСharacter.GetCharacterPosition(), Pos));
                GoToNextPosition();
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Pos.x < Width && Pos.y < Height && Pos.x >= 0 && Pos.y >= 0)
            {
                //MarkWalk();
            }
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
            //Index = 0;
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
        if (Index < PathCells.Count)
        {

            Vector3 Converted = Grid.CellToLocal(new Vector3Int(PathCells[Index].x, PathCells[Index].y, PathCells[Index].z));
            Vector3 CellPosition = new Vector3(Converted.x, Converted.y, Converted.z);
            ControlledСharacter.SetCellToMove(CellPosition);
            Index++;

        }
        else
        {
            Index = 0;
            ControlledСharacter.LastPointReached();

            MarkAttackable();
            MarkWalk();

            SetNewCharcter();
        }
    }

    private void MarkWalk()
    {
        Vector3Int CharacterPosition = ControlledСharacter.GetCharacterPosition();

        for (int x = CharacterPosition.x - MaxMovemetDistance + 1; x <= CharacterPosition.x + MaxMovemetDistance; x++)
        {
            for (int y = CharacterPosition.y - MaxMovemetDistance + 1; y <= CharacterPosition.y + MaxMovemetDistance; y++)
            {
                if (PathFinding.FindPath(CharacterPosition, new Vector3Int(x, y, 0)) != null && PathFinding.FindPath(CharacterPosition, new Vector3Int(x, y, 0)).Count <= MaxMovemetDistance)
                {
                    customGrid.AddToTileMap(x, y, WakableTile, InterfaceTilemap.GetComponent<Tilemap>());
                }
            }
        }
    }

    private void MarkAttackable()
    {
        Vector3Int CharacterPosition = ControlledСharacter.GetCharacterPosition();

        for (int x = CharacterPosition.x - MaxAttackRange + 1; x <= CharacterPosition.x + MaxAttackRange; x++)
        {
            for (int y = CharacterPosition.y - MaxAttackRange + 1; y <= CharacterPosition.y + MaxAttackRange; y++)
            {
                if (PathFinding.FindPath(CharacterPosition, new Vector3Int(x, y, 0)) != null && PathFinding.FindPath(CharacterPosition, new Vector3Int(x, y, 0)).Count <= MaxAttackRange)
                {
                    customGrid.AddToTileMap(x, y, AttackableTile, InterfaceTilemap.GetComponent<Tilemap>());
                }
            }
        }
    }

    private void SetNewCharcter()
    {

        Character = OrderOfMovement.GetNewPlayableCharacter();

        ControlledСharacter = Character.GetComponent<CharacterMovement>();
        MaxMovemetDistance = Character.GetMaxMovementDistance();
        MaxAttackRange = Character.GetMaxAttackRange();

    }

}
