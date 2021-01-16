using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    public int Coins { get; private set; } = 7;
    public int VictoryPoints { get; private set; }
    public int MilitaryStrength { get; private set; } 
    public int Wood { get; private set; }
    public int Ore { get; private set; }
    public int Clay { get; private set; }
    public int Stone { get; private set; }
    public int Glass { get; private set; }
    public int Papyrus { get; private set; }
    public int Textiles { get; private set; }
    public List<GameObject> WonderCards { get; private set; } = new List<GameObject>();
    public List<GameObject> Cards { get; private set; } = new List<GameObject>();

    public List<string> Discounts { get; set; } = new List<string>();

    public List<int> ScienceTokens { get; private set; } = new List<int>();
    public List<int> ChainingTokens { get; private set; } = new List<int>();

    public GameObject[] wonderCardsPositions;

    private int pointsIndex = 0;

    public void SpendCoins(int value)
    {
        Coins -= value;
    }

    public void AddWonderCard(GameObject wonderCard)
    {
        WonderCards.Add(wonderCard);
        
        var x = wonderCardsPositions[pointsIndex].transform.position.x;
        var y = wonderCardsPositions[pointsIndex].transform.position.y;
        ChangeCardPosition(x, y, wonderCard);
        wonderCard.transform.localScale = new Vector3(0.0063f, 0.015f);

        wonderCard.GetComponent<Button>().interactable = false;

        pointsIndex++;
        if (pointsIndex > 3)
            pointsIndex = 0;
    }

    public void AddResources(Dictionary<string, int> resources)
    {
        foreach (var resource in resources)
        {
            switch (resource.Key)
            {
                case "coins":
                    Coins += resource.Value;
                    break;
                case "victoryPoints":
                    VictoryPoints += resource.Value;
                    break;
                case "militaryStrength":
                    MilitaryStrength += resource.Value;
                    GameObject.FindGameObjectWithTag("ConflictToken").GetComponent<ConflictTokenController>().MilitaryStrengthChange();
                    break;
                case "wood":
                    Wood += resource.Value;
                    break;
                case "ore":
                    Ore += resource.Value;
                    break;
                case "clay":
                    Clay += resource.Value;
                    break;
                case "stone":
                    Stone += resource.Value;
                    break;
                case "glass":
                    Glass += resource.Value;
                    break;
                case "papyrus":
                    Papyrus += resource.Value;
                    break;
                case "textiles":
                    Textiles += resource.Value;
                    break;
                case "scienceToken":
                    if (!ScienceTokens.Contains(resource.Value))
                    {
                        ScienceTokens.Add(resource.Value);
                    }
                    break;
                case "chainingToken":
                    if (!ChainingTokens.Contains(resource.Value))
                    {
                        ChainingTokens.Add(resource.Value);
                    }
                    break;
            }
        }
    }

    public void AddDiscount(string discount)
    {
        if (!Discounts.Contains(discount))
        {
            Discounts.Add(discount);
        }
    }

    public void ChangeVictoryPoints(int number)
    {
        VictoryPoints += number;
    }

    public void ChangeCoins(int number)
    {
        Coins += number;
    }

    public void AddCard(GameObject card)
    {
        Cards.Add(card);
        card.transform.position = new Vector3(-30, -30);
    }

    public void ChangeCardPosition(float x, float y, GameObject card)
    {
        card.transform.position = new Vector3(x, y);
    }

    public void MoveWondersToBoard()
    {
        foreach(var wonder in WonderCards)
        {
            var x = wonderCardsPositions[pointsIndex].transform.position.x;
            var y = wonderCardsPositions[pointsIndex].transform.position.y;

            wonder.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            wonder.GetComponentInChildren<WonderCardController>().ChangeLayerWithCard();

            ChangeCardPosition(x, y, wonder);
            pointsIndex++;
        }
        pointsIndex = 0;
    }

    public int NumberOfYellowCards()
    {
        int number = 0;
        foreach(var card in Cards)
        {
            var controller = card.GetComponent<CardController>();

            if(controller.cardColor == "yellow" || controller.cardColor == "Yellow")
            {
                number++;
            }
        }

        return number;
    }

    /*public int CalculateWonderCost(int index, Player opponent)
    {
        var wonder = WonderCards[index].GetComponent<WonderCardController>();
        var cost = wonder.cardCoinsCost;

        if (wonder.cardWoodCost > Wood)
        {
            cost += ResourceCost(wonder.cardWoodCost, Wood, opponent.Wood);
        }
        if (wonder.cardOreCost > Ore)
        {
            cost += ResourceCost(wonder.cardOreCost, Ore, opponent.Ore);
        }
        if (wonder.cardClayCost > Clay)
        {
            cost += ResourceCost(wonder.cardClayCost, Clay, opponent.Clay);
        }
        if (wonder.cardStoneCost > Stone)
        {
            cost += ResourceCost(wonder.cardStoneCost, Stone, opponent.Stone);
        }
        if (wonder.cardGlassCost > Glass)
        {
            cost += ResourceCost(wonder.cardClayCost, Clay, opponent.Clay);
        }
        if (wonder.cardPapyrusCost > Papyrus)
        {
            cost += ResourceCost(wonder.cardPapyrusCost, Papyrus, opponent.Papyrus);
        }
        if (wonder.cardTextilesCost > Textiles)
        {
            cost += ResourceCost(wonder.cardTextilesCost, Textiles, opponent.Textiles);
        }

        return cost;
    }

    private int ResourceCost(int resource, int playerResources, int opponentResources)
    {
        var cost = 0;
        cost += (2 * (resource - playerResources)) + opponentResources;

        return cost;
    }*/
}