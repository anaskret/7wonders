using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionController : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject[] points;
    [SerializeField] private Text buyText;
    [SerializeField] private Text sellText;
    [SerializeField] private Text[] buildWonders;

    private Player playerOne;
    private Player playerTwo;

    private Player currentPlayer;
    private Player opponent;

    private bool isPlayerOneTurn;

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
        card.transform.position = new Vector3(-0.55f, 0.5929288f);
        card.transform.localScale = new Vector3(1.5f, 2f);
        gameObject.transform.localScale = new Vector3(0.65f, 0.55f);
        gameObject.transform.position = new Vector3(0.7544492f, 0.8872917f);
        card.GetComponent<Renderer>().sortingOrder = 4;

        buyText.text = card.GetComponent<CardController>().CalculateCost().ToString();
        sellText.text = (currentPlayer.NumberOfYellowCards() + 2).ToString();

        var index = 0;
        foreach(var text in buildWonders)
        {
            text.text = currentPlayer.CalculateWonderCost(index, opponent).ToString();
        }


        foreach (var point in points)
        {
            currentPlayer.ChangeCardPosition(point.transform.position.x, point.transform.position.y, currentPlayer.WonderCards[index]);
            currentPlayer.WonderCards[index].transform.GetComponentInChildren<Renderer>().sortingOrder = 4;
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

    public void WonderBuild()
    {
        //finish building

        gameObject.SetActive(false);
        currentPlayer.MoveWondersToBoard();
    }

    public void HideWindow()
    {
        var index = 0;
        foreach (var point in points)
        {
            currentPlayer.ChangeCardPosition(currentPlayer.wonderCardsPositions[index].transform.position.x, currentPlayer.wonderCardsPositions[index].transform.position.y, currentPlayer.WonderCards[index]);
            currentPlayer.WonderCards[index].transform.GetComponentInChildren<Renderer>().sortingOrder = 3;
            index++;
        }

        card.GetComponent<CardController>().MoveCardBack();
        card.GetComponent<Renderer>().sortingOrder = 0;
        gameObject.SetActive(false);
    }
}
