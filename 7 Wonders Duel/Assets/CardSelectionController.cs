﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionController : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject[] points;
    [SerializeField] private GameObject[] buildButtons;
    [SerializeField] private Text buyText;
    [SerializeField] private Text sellText;
    [SerializeField] private Text[] buildWonders;

    private Player playerOne;
    private Player playerTwo;

    private Player currentPlayer;
    private Player opponent;

    private bool isPlayerOneTurn;

    private int orderInLayer;

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    private void Update()
    {
        if (isPlayerOneTurn)
        {
            currentPlayer = playerOne;
            opponent = playerTwo;
        }
        else
        {
            currentPlayer = playerTwo;
            opponent = playerOne;
        }
    }

    public void ShowSelection(bool turn)
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();

        isPlayerOneTurn = turn;

        if (isPlayerOneTurn)
        {
            currentPlayer = playerOne;
            opponent = playerTwo;
        }
        else
        {
            currentPlayer = playerTwo;
            opponent = playerOne;
        }

        gameObject.SetActive(true);
        card.transform.position = new Vector3(-0.75f, 0.5929288f);
        card.transform.localScale = new Vector3(1.5f, 2f);
        gameObject.transform.localScale = new Vector3(0.65f, 0.55f);
        gameObject.transform.position = new Vector3(0.7544492f, 0.8872917f);

        var cardRenderer = card.GetComponent<Renderer>();
        var cardController = card.GetComponent<CardController>();
        orderInLayer = cardRenderer.sortingOrder;
        cardRenderer.sortingOrder = 11;

        cardController.ChangeLayerWithCard();

        buyText.text = cardController.CalculateCost().ToString();
        sellText.text = (currentPlayer.NumberOfYellowCards() + 2).ToString();

        var index = 0;
        foreach(var text in buildWonders)
        {
            var wonder = currentPlayer.WonderCards[index];
            var controller = wonder.GetComponent<WonderCardController>();

            wonder.GetComponentInChildren<Renderer>().sortingOrder = 11;
            controller.ChangeLayerWithCard();

            if (controller.canBeBuilt && !controller.isBuilt)
            {
                text.text = controller.CalculateCost().ToString();
            }
            else if(!controller.canBeBuilt || controller.isBuilt)
            {
                text.text = "";
                buildButtons[index].SetActive(false);
            }
            index++;
        }
        index = 0;

        foreach (var point in points)
        {
            var wonder = currentPlayer.WonderCards[index];
            currentPlayer.ChangeCardPosition(point.transform.position.x, point.transform.position.y, wonder);
            
            index++;
        }
    }

    public void CardBought()
    {
        var canBeBought = card.GetComponent<CardController>().Buy();

        if (canBeBought)
        {
            gameObject.SetActive(false);
            currentPlayer.MoveWondersToBoard();
        }
    }

    public void CardSold()
    {
        card.GetComponent<CardController>().Sell();

        gameObject.SetActive(false);
        currentPlayer.MoveWondersToBoard();
    }

    public void WonderBuild(int index)
    {
        var wonder = currentPlayer.WonderCards[index];

        if (CanBeBuilt(wonder, currentPlayer))
        {
            var controller = wonder.GetComponent<WonderCardController>();

            card.GetComponent<CardController>().Build(controller.repeatTurn);

            GameController.Wonders.Remove(wonder);
            controller.Build(currentPlayer, card);

            card.transform.position = new Vector3(100, 100);
            card.transform.localScale = new Vector3(0.95f, 1.2f);
            card.transform.rotation *= Quaternion.Euler(0, 0, 90f);

            gameObject.SetActive(false);
            currentPlayer.MoveWondersToBoard();
        }
    }

    private bool CanBeBuilt(GameObject wonder, Player player)
    {
        var controller = wonder.GetComponent<WonderCardController>();
        var cost = controller.CalculateCost();

        if(cost > player.Coins)
        {
            return false;
        }

        return true;
    }

    public void HideWindow()
    {
        currentPlayer.MoveWondersToBoard();

        var cardController = card.GetComponent<CardController>();

        cardController.MoveCardBack();
        card.GetComponent<Renderer>().sortingOrder = orderInLayer;
        cardController.ChangeLayerWithCard();
        gameObject.SetActive(false);
    }
}
