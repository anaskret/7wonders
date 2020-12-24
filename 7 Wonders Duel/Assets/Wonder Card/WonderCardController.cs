using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonderCardController : MonoBehaviour
{
    private Player playerOne;
    private Player playerTwo;

    public int cardCoinsCost;
    public int cardWoodCost;
    public int cardOreCost;
    public int cardClayCost;
    public int cardStoneCost;
    public int cardGlassCost;
    public int cardPapyrusCost;
    public int cardTextilesCost;

    public bool isBuilt;

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    public void WonderSelected()
    {
        if (WonderSelectionTurn.PlayerTurn == 1)
        {
            playerOne.AddWonderCard(gameObject);
            WonderSelectionTurn.TurnChange();
        }
        else
        {
            playerTwo.AddWonderCard(gameObject);
            WonderSelectionTurn.TurnChange();
        }
    }
}
