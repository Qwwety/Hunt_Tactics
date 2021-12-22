
using System.Collections.Generic;
public class SetTheOrderOfMovement
{
    public SetTheOrderOfMovement(List<CharacterClass> characters)
    {
        Queue(characters, 0, characters.Count - 1);
    }

    private int GetMarker(List<CharacterClass> Characters, int StartPoint, int EndPoint)
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

    private void Queue(List<CharacterClass> Characters, int StartPoint, int EndPoint)
    {
        if (StartPoint >= EndPoint)
        {
            return;
        }

        int ponter = GetMarker(Characters, StartPoint, EndPoint);
        Queue(Characters, StartPoint, ponter - 1);
        Queue(Characters, ponter + 1, EndPoint);
    }
}
