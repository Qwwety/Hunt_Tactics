using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Grid Grid;


    /// <summary>
    /// Задает следующую клетку для движеия персонажа и запускает анимационный тригер
    /// </summary>
    /// <param name="TransformToPosition"></param>
    public void SetCellToMove(Vector3 TransformToPosition)
    {
        Movement(TransformToPosition);
    }

    /// <summary>
    /// возвращвет позицую персонажа с попрапвкой на расположение grid
    /// </summary>
    /// <param name="Grid"></param>
    /// <returns></returns>
    public Vector3Int GetCharacterPosition()
    {
        return Grid.LocalToCell(new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    /// <summary>
    /// Передвежение
    /// </summary>
    private void Movement(Vector3 TransformToPosition)
    {
        transform.position = TransformToPosition;
    }

   
}

