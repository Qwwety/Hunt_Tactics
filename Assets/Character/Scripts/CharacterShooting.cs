using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public void DoDamage(CharacterClass character,float Damage)
    {
        ITakeDamage attack = character.GetComponent<CharacterClass>();

        attack.TakeDamage(Damage);
    }
}
