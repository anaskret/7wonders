using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text vpText;
    [SerializeField] private Text woodText;
    [SerializeField] private Text oreText;
    [SerializeField] private Text clayText;
    [SerializeField] private Text stoneText;
    [SerializeField] private Text glassText;
    [SerializeField] private Text papyrusText;
    [SerializeField] private Text textilesText;

    [SerializeField] private bool isPlayerOne;
    [SerializeField] private GameObject detailedBoard;

    Player playerOne;
    Player playerTwo;

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    private void Update()
    {
        Player currentPlayer;
        if (isPlayerOne)
        {
            currentPlayer = playerOne;
        }
        else
        {
            currentPlayer = playerTwo;
        }

        coinText.text = currentPlayer.Coins.ToString();
        vpText.text = currentPlayer.VictoryPoints.ToString();
        woodText.text = currentPlayer.Wood.ToString();
        oreText.text = currentPlayer.Ore.ToString();
        clayText.text = currentPlayer.Clay.ToString();
        stoneText.text = currentPlayer.Stone.ToString();
        glassText.text = currentPlayer.Glass.ToString();
        papyrusText.text = currentPlayer.Papyrus.ToString();
        textilesText.text = currentPlayer.Textiles.ToString();
    }

    public void ShowDetails()
    {
        detailedBoard.SetActive(true);
        if (isPlayerOne)
        {
            detailedBoard.GetComponent<DetailedBoardController>().ShowBoard(playerOne);
        }
        else
        {
            detailedBoard.GetComponent<DetailedBoardController>().ShowBoard(playerTwo);
        }
    }
}
