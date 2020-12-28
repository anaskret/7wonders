using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedBoardController : MonoBehaviour
{
    [SerializeField] private GameObject[] wonderPositions;
    [SerializeField] private GameObject[] cardPositions;

    Player player;

    public void ShowBoard(Player player)
    {
        this.player = player;

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

    public void HideBoard()
    {
        var index = 0;
        foreach (var position in wonderPositions)
        {
            player.ChangeCardPosition(player.wonderCardsPositions[index].transform.position.x, player.wonderCardsPositions[index].transform.position.y, player.WonderCards[index]);
            index++;
        }
        index = 0;



        if (player.Cards.Count <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        foreach (var position in cardPositions)
        {
            player.ChangeCardPosition(-100, -100, player.Cards[index]);
            index++;
        }

        gameObject.SetActive(false);
    }
}
