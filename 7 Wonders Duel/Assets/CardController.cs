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

    [SerializeField] private int scienceToken;
    [SerializeField] private int chainingToken;

    [SerializeField] private int cardCoinsCost;
    [SerializeField] private int cardWoodCost;
    [SerializeField] private int cardOreCost;
    [SerializeField] private int cardClayCost;
    [SerializeField] private int cardStoneCost;
    [SerializeField] private int cardGlassCost;
    [SerializeField] private int cardPapyrusCost;
    [SerializeField] private int cardTextilesCost;
    [SerializeField] private int cardChainingCost;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject orePrefab;
    [SerializeField] private GameObject clayPrefab;
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private GameObject glassPrefab;
    [SerializeField] private GameObject papyrusPrefab;
    [SerializeField] private GameObject textilesPrefab;

    [SerializeField] private GameObject[] coveringCards;

    [SerializeField] private GameObject chainingTokenPrefab;

    [SerializeField] private GameObject[] resourcePositions;
    [SerializeField] private GameObject chainingTokenPosition;

    [SerializeField] private GameObject[] costPositons;
    [SerializeField] private GameObject chainingCostPositon;

    private Player playerOne;
    private Player playerTwo;

    public bool isTaken = false;
    [SerializeField] private bool isBackToFront = false;
    [SerializeField] private GameObject cardBack;

    private Dictionary<string, int> resources = new Dictionary<string, int>();

    [SerializeField] private Button button; 

    private float startX;
    private float startY;

    private Color grey = new Color(128, 128, 128, 255)/255;
    private Color brown = new Color(97, 64, 18, 255)/255;
    private Color red = new Color(152, 46, 12, 255)/255;
    private Color blue = new Color(22, 108, 159, 255)/255;
    private Color yellow = new Color(255, 225, 88, 255)/255;
    private Color green = new Color(44, 90, 20, 255)/255;

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();

        AddResourcesToDictionary();

        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;

        CardSetup();
    }

    private void Update()
    {
        int isNotCovered = 0;
        foreach(var card in coveringCards)
        {
            var controller = card.GetComponent<CardController>();
            if (controller.isTaken)
            {
                isNotCovered++;
            }
        }
        if(isNotCovered == coveringCards.Length)
        {
            button.interactable = true;
            cardBack.GetComponent<SpriteRenderer>().sortingOrder = -2;
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
        if (scienceToken > -1 && scienceToken < 7)
        {
            resources.Add(nameof(scienceToken), scienceToken);
        }
        if (chainingToken > -1 && chainingToken < 7)
        {
            resources.Add(nameof(chainingToken), chainingToken);
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

        if (player.ChainingTokens.Contains(cardChainingCost))
        {
            return 0;
        }

        var cost = cardCoinsCost;

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


    private string firstResource;
    private string secondResource;
    private string thirdResource;
    
    private string firstCost;
    private string secondCost;
    private string thirdCost;

    private GameObject[] costs;
    private GameObject[] resourcesPrefabs;

    private void CardSetup()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();

        if (isBackToFront)
        {
            cardBack.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
        }

        switch (cardColor)
        {
            case "grey":
                renderer.color = grey;
                break;
            case "brown":
                renderer.color = brown;
                break;
            case "red":
                renderer.color = red;
                break;
            case "blue":
                renderer.color = blue;
                break;
            case "yellow":
                renderer.color = yellow;
                break;
            case "green":
                renderer.color = green;
                break;
        }

        if(chainingToken > -1 && chainingToken < 7)
        {
            var token = Instantiate(chainingTokenPrefab, chainingTokenPosition.transform.position, chainingTokenPosition.transform.rotation);
            token.GetComponentInChildren<Text>().text = chainingToken.ToString();
        }

        foreach(var cost in costPositons)
        {
            if(cardCoinsCost > 0 && !IsSetup(nameof(cardCoinsCost)))
            {
                var prefab = Instantiate(coinPrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardCoinsCost));
                prefab.GetComponentInChildren<Text>().text = cardCoinsCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardWoodCost > 0 && !IsSetup(nameof(cardWoodCost)))
            {
                var prefab = Instantiate(woodPrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardWoodCost));
                prefab.GetComponentInChildren<Text>().text = cardWoodCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardOreCost > 0 && !IsSetup(nameof(cardOreCost)))
            {
                var prefab = Instantiate(orePrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardOreCost));
                prefab.GetComponentInChildren<Text>().text = cardOreCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardClayCost > 0 && !IsSetup(nameof(cardClayCost)))
            {
                var prefab = Instantiate(clayPrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardClayCost));
                prefab.GetComponentInChildren<Text>().text = cardClayCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardStoneCost > 0 && !IsSetup(nameof(cardStoneCost)))
            {
                var prefab = Instantiate(stonePrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardStoneCost));
                prefab.GetComponentInChildren<Text>().text = cardStoneCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardGlassCost > 0 && !IsSetup(nameof(cardGlassCost)))
            {
                var prefab = Instantiate(glassPrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardGlassCost));
                prefab.GetComponentInChildren<Text>().text = cardGlassCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardPapyrusCost > 0 && !IsSetup(nameof(cardPapyrusCost)))
            {
                var prefab = Instantiate(papyrusPrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardPapyrusCost));
                prefab.GetComponentInChildren<Text>().text = cardPapyrusCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
            else if (cardTextilesCost > 0 && !IsSetup(nameof(cardTextilesCost)))
            {
                var prefab = Instantiate(textilesPrefab, cost.transform.position, cost.transform.rotation);
                AssignCostName(nameof(cardTextilesCost));
                prefab.GetComponentInChildren<Text>().text = cardTextilesCost.ToString();
                prefab.GetComponentInChildren<Canvas>().sortingOrder = renderer.sortingOrder + 1;
                prefab.GetComponent<SpriteRenderer>().sortingOrder = renderer.sortingOrder + 1;
            }
        }
    }

    private bool IsSetup(string name)
    {
        if(name == firstResource || name == secondResource || name == thirdResource)
        {
            return true;
        }
        
        if(name == firstCost || name == secondCost || name == thirdCost)
        {
            return true;
        }

        return false;
    }

    private void AssignCostName(string name)
    {
        if(firstCost == null)
        {
            firstCost = name;
        }
        else if(secondCost == null)
        {
            secondCost = name;
        }
        else if(thirdCost == null)
        {
            thirdCost = name;
        }
    }
}
