using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int Health;
   
    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health<=0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("I umer, Blin bulit");
    }

}
