using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WonderSelectionTurn
{
    public static int PlayerTurn { get; private set; } = 1;
    public static int Round { get; private set; } = 0;

    public static int Index { get; private set; } = 0;

    public static void TurnChange()
    {
        Index++;
        if (Round == 0)
        {
            if (Index == 1 || Index == 2)
            {
                PlayerTurn = 2;
            }
            else
            {
                PlayerTurn = 1;
            }
        }
        else
        {
            if (Index == 1 || Index == 2)
            {
                PlayerTurn = 1;
            }
            else
            {
                PlayerTurn = 2;
            }
        }
    }

    public static void NewRound()
    {
        Index = 0;
        PlayerTurn = 2;
        Round = 1;
    }

    public static void Reset()
    {
        PlayerTurn = 1;
        Round = 0;
        Index = 0;
    }
}
