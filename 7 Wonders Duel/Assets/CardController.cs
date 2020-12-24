using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private GameObject cardSelection;

    public string cardColor;

    [SerializeField] private int coins;
    [SerializeField] private int victoryPoints;
    [SerializeField] private int militaryStrength;
    [SerializeField] private int wood;
    [SerializeField] private int ore;
    [SerializeField] private int clay;
    [SerializeField] private int stone;
    [SerializeField] private int glass;
    [SerializeField] private int papyrus;
    [SerializeField] private int textiles;

    [SerializeField] private int cardCoinsCost;
    [SerializeField] private int cardWoodCost;
    [SerializeField] private int cardOreCost;
    [SerializeField] private int cardClayCost;
    [SerializeField] private int cardStoneCost;
    [SerializeField] private int cardGlassCost;
    [SerializeField] private int cardPapyrusCost;
    [SerializeField] private int cardTextilesCost;

    [SerializeField] private GameObject[] coveringCards;

    private Player playerOne;
    private Player playerTwo;

    public bool isTaken = false;

    private Dictionary<string, int> resources = new Dictionary<string, int>();

    [SerializeField] private Button button; 

    private float startX;
    private float startY;

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();

        AddResourcesToDictionary();

        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;
    }

    private void Update()
    {
        int isCovered = 0;
        foreach(var card in coveringCards)
        {
            var controller = card.GetComponent<CardController>();
            if (controller.isTaken)
            {
                isCovered++;
            }
        }
        if(isCovered == coveringCards.Length)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void CardSelected()
    {
        cardSelection.GetComponent<CardSelectionController>().ShowSelection(GameController.IsPlayerOneTurn);
    }

    private void AddResourcesToDictionary()
    {
        if (coins > 0)
        {
            resources.Add(nameof(coins), coins);
        }
        if (victoryPoints > 0)
        {
            resources.Add(nameof(victoryPoints), victoryPoints);
        }
        if (militaryStrength > 0)
        {
            resources.Add(nameof(militaryStrength), militaryStrength);
        }
        if (wood > 0)
        {
            resources.Add(nameof(wood), wood);
        }
        if (ore > 0)
        {
            resources.Add(nameof(ore), ore);
        }
        if (clay > 0)
        {
            resources.Add(nameof(clay), clay);
        }
        if (glass > 0)
        {
            resources.Add(nameof(glass), glass);
        }
        if (papyrus > 0)
        {
            resources.Add(nameof(papyrus), papyrus);
        }
        if (textiles > 0)
        {
            resources.Add(nameof(textiles), textiles);
        }
    }

    public bool Buy()
    {
        var cost = CalculateCost();
        Player player;

        if (GameController.IsPlayerOneTurn)
        {
            player = playerOne;
        }
        else
        {
            player = playerTwo;
        }

        if(cost > player.Coins)
        {
            return false;
        }

        player.AddResources(resources);
        player.SpendCoins(cost);
        player.AddCard(gameObject);

        button.interactable = false;

        GameController.IsPlayerOneTurn = !GameController.IsPlayerOneTurn;
        isTaken = true;

        return true;
    }

    public void Sell()
    {
        Player player;

        if (GameController.IsPlayerOneTurn)
        {
            player = playerOne;
        }
        else
        {
            player = playerTwo;
        }

        int coinsGained = player.NumberOfYellowCards() + 2;
        var dictionary = new Dictionary<string, int>();
        dictionary.Add("coins", coinsGained);

        player.AddResources(dictionary);

        GameController.IsPlayerOneTurn = !GameController.IsPlayerOneTurn;
        isTaken = true;

        gameObject.transform.position = new Vector3(100, 100);
    }

    public int CalculateCost()
    {
        Player player;
        Player opponent;
        if(GameController.IsPlayerOneTurn)
        {
            player = playerOne;
            opponent = playerTwo;
        }
        else
        {
            player = playerTwo;
            opponent = playerOne;
        }

        var cost = cardCoinsCost;

        if (cardWoodCost > player.Wood)
        {
            cost += ResourceCost(cardWoodCost, player.Wood, opponent.Wood);
        }
        if (cardWoodCost > player.Wood)
        {
            cost += ResourceCost(cardWoodCost, player.Wood, opponent.Wood);
        }
        if (cardOreCost > player.Ore)
        {
            cost += ResourceCost(cardOreCost, player.Ore, opponent.Ore);
        }
        if (cardClayCost > player.Clay)
        {
            cost += ResourceCost(cardClayCost, player.Clay, opponent.Clay);
        }
        if (cardStoneCost > player.Stone)
        {
            cost += ResourceCost(cardStoneCost, player.Stone, opponent.Stone);
        }
        if (cardGlassCost > player.Glass)
        {
            cost += ResourceCost(cardClayCost, player.Clay, opponent.Clay);
        }
        if (cardPapyrusCost > player.Papyrus)
        {
            cost += ResourceCost(cardPapyrusCost, player.Papyrus, opponent.Papyrus);
        }
        if (cardTextilesCost > player.Textiles)
        {
            cost += ResourceCost(cardTextilesCost, player.Textiles, opponent.Textiles);
        }

        return cost;
    }

    private int ResourceCost(int resource, int playerResources, int opponentResources)
    {
        var cost = 0;
        cost += (2 * (resource - playerResources)) + opponentResources;

        return cost;
    }

    public void MoveCardBack()
    {
        gameObject.transform.position = new Vector3(startX, startY);
        gameObject.transform.localScale = new Vector3(0.5661473f, 0.9380213f);
    }
}
