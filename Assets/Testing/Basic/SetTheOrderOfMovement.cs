
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetTheOrderOfMovement : MonoBehaviour
{
    [SerializeField] private CharacterClass[] Characters;

    int Index = 0;


    public void ActivateQueueSort()
    {
        Queue(Characters, 0, Characters.Length - 1);
    }

    private int GetMarker(CharacterClass[] Characters, int StartPoint, int EndPoint)
    {
        CharacterClass TemporyValue;
        int marker = StartPoint;

        for (int i = StartPoint; i <= EndPoint; i++)
        {
            if (Characters[i].GetInitiativeAmount() > Characters[EndPoint].GetInitiativeAmount())
            {
                TemporyValue = Characters[marker];
                Characters[marker] = Characters[i];
                Characters[i] = TemporyValue;
                marker++;
            }
        }

        TemporyValue = Characters[marker];
        Characters[marker] = Characters[EndPoint];
        Characters[EndPoint] = TemporyValue;

        return marker;
    }

    public void Queue(CharacterClass[] Characters, int StartPoint, int EndPoint)
    {
        if (StartPoint >= EndPoint)
        {
            return;
        }

        int ponter = GetMarker(Characters, StartPoint, EndPoint);
        Queue(Characters, StartPoint, ponter - 1);
        Queue(Characters, ponter + 1, EndPoint);
    }

    public CharacterClass GetNewPlayableCharacter()
    {
        CharacterClass CurrentCharacter;
        if (Index < Characters.Length)
        {
            CurrentCharacter = Characters[Index];
            Index++;
        }
        else
        {
            Index = 0;
            CurrentCharacter = Characters[Index];
        }
        return CurrentCharacter;
    }

}
