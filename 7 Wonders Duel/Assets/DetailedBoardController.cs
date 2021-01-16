using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedBoardController : MonoBehaviour
{
    [SerializeField] private GameObject[] wonderPositions;
    [SerializeField] private GameObject[] cardUsedToBuildPositions;
    [SerializeField] private GameObject[] cardPositions;

    Player player;

    public void ShowBoard(Player player)
    {
        this.player = player;

        var index = 0;
        foreach(var position in wonderPositions)
        {
            var wonder = player.WonderCards[index];
            var controller = wonder.GetComponent<WonderCardController>();
            player.ChangeCardPosition(position.transform.position.x, position.transform.position.y, wonder);
            if (controller.isBuilt)
            {
                controller.cardUsedToBuild.transform.position = cardUsedToBuildPositions[index].transform.position;
                controller.cardUsedToBuild.GetComponent<SpriteRenderer>().sortingOrder = 13;
                controller.cardUsedToBuild.GetComponent<CardController>().ChangeLayerWithCard();
            }
            wonder.GetComponentInChildren<Renderer>().sortingOrder = 14;
            wonder.GetComponent<WonderCardController>().ChangeLayerWithCard();
            index++;
        }
        index = 0;


        if (player.Cards.Count <= 0)
            return;

        for (int i = 0; i < player.Cards.Count; i++)
        {
            var card = player.Cards[index];
            player.ChangeCardPosition(cardPositions[i].transform.position.x, cardPositions[i].transform.position.y, card);
            card.transform.localScale = new Vector3(0.7f, 1.2f);
            card.GetComponent<SpriteRenderer>().sortingOrder = 13;
            card.GetComponent<CardController>().ChangeLayerWithCard();
            index++;
        }
    }

    public void HideBoard()
    {
        var index = 0;
        foreach (var position in wonderPositions)
        {
            var wonder = player.WonderCards[index];
            var controller = wonder.GetComponent<WonderCardController>();
            player.ChangeCardPosition(player.wonderCardsPositions[index].transform.position.x, player.wonderCardsPositions[index].transform.position.y, wonder);
            if (controller.isBuilt)
            {
                controller.cardUsedToBuild.transform.position = new Vector3(100, 100);
            }
            wonder.GetComponentInChildren<Renderer>().sortingOrder = 13;
            wonder.GetComponent<WonderCardController>().ChangeLayerWithCard();
            index++;
        }

        if (player.Cards.Count <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        for(int i = 0; i < player.Cards.Count; i++)
        {
            player.ChangeCardPosition(-100, -100, player.Cards[i]);
        }

        gameObject.SetActive(false);
    }
}
