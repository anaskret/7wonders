using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedBoardController : MonoBehaviour
{
    [SerializeField] private GameObject[] wonderPositions;
    [SerializeField] private GameObject[] cardPositions;

    public void ShowBoard(Player player)
    {
        var index = 0;
        foreach(var position in wonderPositions)
        {
            player.ChangeCardPosition(position.transform.position.x, position.transform.position.y, player.WonderCards[index]);
            index++;
        }
        index = 0;


        if (player.Cards.Count <= 0)
            return;

        foreach(var position in cardPositions)
        {
            player.ChangeCardPosition(position.transform.position.x, position.transform.position.y, player.Cards[index]);
            index++;
        }
    }

    public void HideBoard(Player player)
    {
        var index = 0;
        foreach (var position in wonderPositions)
        {
            player.ChangeCardPosition(player.wonderCardsPositions[index].transform.position.x, player.wonderCardsPositions[index].transform.position.y, player.WonderCards[index]);
            index++;
        }
        index = 0;


        if (player.Cards.Count <= 0)
            return;

        foreach (var position in cardPositions)
        {
            player.ChangeCardPosition(-100, -100, player.Cards[index]);
            index++;
        }
    }
}
