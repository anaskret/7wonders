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
        if (isPlayerOne)
        {
            coinText.text = playerOne.Coins.ToString();
            vpText.text = playerOne.VictoryPoints.ToString();
            woodText.text = playerOne.Wood.ToString();
            oreText.text = playerOne.Ore.ToString();
            clayText.text = playerOne.Clay.ToString();
            stoneText.text = playerOne.Stone.ToString();
            glassText.text = playerOne.Glass.ToString();
            papyrusText.text = playerOne.Papyrus.ToString();
            textilesText.text = playerOne.Textiles.ToString();
        }
        else
        {
            coinText.text = playerTwo.Coins.ToString();
            vpText.text = playerTwo.VictoryPoints.ToString();
            woodText.text = playerTwo.Wood.ToString();
            oreText.text = playerTwo.Ore.ToString();
            clayText.text = playerTwo.Clay.ToString();
            stoneText.text = playerTwo.Stone.ToString();
            glassText.text = playerTwo.Glass.ToString();
            papyrusText.text = playerTwo.Papyrus.ToString();
            textilesText.text = playerTwo.Textiles.ToString();
        }
    }

    public void ShowDetails()
    {
        detailedBoard.SetActive(true);
    }
}
