using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
    public static bool IsPlayerOneTurn = true;
    public static int NumberOfWonders = 0;
    public static List<GameObject> Wonders = new List<GameObject>();

    public static void Reset()
    {
        IsPlayerOneTurn = true;
        NumberOfWonders = 0;
        Wonders = new List<GameObject>();
    }
}
