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

    [SerializeField] private GameObject[] scienceTokens;
    [SerializeField] private GameObject[] chainingTokens;

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

        foreach(var token in currentPlayer.ChainingTokens)
        {
            switch (token)
            {
                case 1: chainingTokens[0].SetActive(true);
                    break;
                case 2: chainingTokens[1].SetActive(true);
                    break;
                case 3: chainingTokens[2].SetActive(true);
                    break;
                case 4: chainingTokens[3].SetActive(true);
                    break;
                case 5: chainingTokens[4].SetActive(true);
                    break;
                case 6: chainingTokens[5].SetActive(true);
                    break;
            }
        }

        foreach(var token in currentPlayer.ScienceTokens)
        {
            switch (token)
            {
                case 1: scienceTokens[0].SetActive(true);
                    break;
                case 2: scienceTokens[1].SetActive(true);
                    break;
                case 3: scienceTokens[2].SetActive(true);
                    break;
                case 4: scienceTokens[3].SetActive(true);
                    break;
                case 5: scienceTokens[4].SetActive(true);
                    break;
                case 6: scienceTokens[5].SetActive(true);
                    break;
            }
        }
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
