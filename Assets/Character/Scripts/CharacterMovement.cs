using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Grid Grid;

    private Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
   
    /// <summary>
    /// Задает следующую клетку для движеия персонажа и запускает анимационный тригер
    /// </summary>
    /// <param name="TransformToPosition"></param>
    public void SetCellToMove(Vector3 TransformToPosition)
    {
        Animator.SetTrigger("GoToWalk");
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
        if (Vector3.Distance(transform.position, TransformToPosition) >0.1f)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(TransformToPosition.x, TransformToPosition.y, TransformToPosition.z),1);
        }
    }

    /// <summary>
    /// Достигрута ли последняя точка
    /// </summary>
    public void LastPointReached()
    {
        Animator.SetBool("IsLastPointReached",true);
    }
    public bool IsLastPointReached()
    {
        return Animator.GetBool("IsLastPointReached");
    }

    /// <summary>
    /// Определяте в пути персонаж или нет
    /// </summary>
    /// <returns></returns>
    public bool CanSetANewPoint()
    {
       return Animator.GetBool("IsMoving");
    }
}

