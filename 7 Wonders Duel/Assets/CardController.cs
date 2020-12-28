using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : CardModel
{
    [SerializeField] private GameObject cardSelection;
    [SerializeField] private GameObject divider;

    public string cardColor;

    [SerializeField] private GameObject[] coveringCards;

    [SerializeField] private GameObject chainingCostPositon;

    public bool isTaken = false;
    [SerializeField] private bool isBackToFront = false;
    [SerializeField] private GameObject cardBack;

    [SerializeField] private Button button; 

    private float startX;
    private float startY;

    private Color grey = new Color(128, 128, 128, 255)/255;
    private Color brown = new Color(97, 64, 18, 255)/255;
    private Color red = new Color(152, 46, 12, 255)/255;
    private Color blue = new Color(22, 108, 159, 255)/255;
    private Color yellow = new Color(255, 225, 88, 255)/255;
    private Color green = new Color(44, 90, 20, 255)/255;

    protected override void Start()
    {
        base.Start();

        AddResourcesToDictionary();

        startX = gameObject.transform.position.x;
        startY = gameObject.transform.position.y;

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

        button.interactable = false;

        player.AddResources(dictionary);

        GameController.IsPlayerOneTurn = !GameController.IsPlayerOneTurn;
        isTaken = true;

        gameObject.transform.position = new Vector3(100, 100);
    }

    public void Build()
    {
        GameController.IsPlayerOneTurn = !GameController.IsPlayerOneTurn;
        isTaken = true;

        button.interactable = false;

        gameObject.transform.position = new Vector3(100, 100);
    }

    public void MoveCardBack()
    {
        gameObject.transform.position = new Vector3(startX, startY);
        gameObject.transform.localScale = new Vector3(0.5661473f, 0.9380213f);
    }

    protected override void CardSetup()
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

        if (cardChainingCost > -1 && cardChainingCost < 7)
        {
            var prefab = Instantiate(textilesPrefab, chainingCostPositon.transform.position, chainingCostPositon.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(cardTextilesCost));
            prefab.GetComponentInChildren<Text>().text = cardTextilesCost.ToString();

            ChangeLayer(renderer, prefab, false);
        }
        
        base.CardSetup();
    }

    

    
}
