using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardModel : MonoBehaviour
{
    [SerializeField] protected int coins;
    [SerializeField] protected int victoryPoints;
    [SerializeField] protected int militaryStrength;
    [SerializeField] protected int wood;
    [SerializeField] protected int ore;
    [SerializeField] protected int clay;
    [SerializeField] protected int stone;
    [SerializeField] protected int glass;
    [SerializeField] protected int papyrus;
    [SerializeField] protected int textiles;

    [SerializeField] protected int scienceToken;
    [SerializeField] protected int chainingToken;

    [SerializeField] protected int cardCoinsCost;
    [SerializeField] protected int cardWoodCost;
    [SerializeField] protected int cardOreCost;
    [SerializeField] protected int cardClayCost;
    [SerializeField] protected int cardStoneCost;
    [SerializeField] protected int cardGlassCost;
    [SerializeField] protected int cardPapyrusCost;
    [SerializeField] protected int cardTextilesCost;
    [SerializeField] protected int cardChainingCost;

    [SerializeField] protected GameObject coinPrefab;
    [SerializeField] protected GameObject woodPrefab;
    [SerializeField] protected GameObject orePrefab;
    [SerializeField] protected GameObject clayPrefab;
    [SerializeField] protected GameObject stonePrefab;
    [SerializeField] protected GameObject glassPrefab;
    [SerializeField] protected GameObject papyrusPrefab;
    [SerializeField] protected GameObject textilesPrefab;
    [SerializeField] protected GameObject chainingTokenPrefab;
    [SerializeField] protected GameObject scienceTokenPrefab;
    [SerializeField] protected GameObject victoryPointsPrefab;
    [SerializeField] protected GameObject militaryStrengthPrefab;

    protected Player playerOne;
    protected Player playerTwo;

    protected Dictionary<string, int> resources = new Dictionary<string, int>();

    [SerializeField] protected GameObject[] resourcePositions;
    [SerializeField] protected GameObject chainingTokenPosition;

    [SerializeField] protected GameObject[] costPositons;

    [SerializeField] protected bool isWonder;

    [SerializeField] protected Button button;

    [SerializeField] private bool woodDiscount;
    [SerializeField] private bool oreDiscount;
    [SerializeField] private bool clayDiscount;
    [SerializeField] private bool stoneDiscount;
    [SerializeField] private bool glassDiscount;
    [SerializeField] private bool papyrusDiscount;
    [SerializeField] private bool textilesDiscount;

    protected string cardDiscount;

    protected virtual void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();

        CardSetup();
    }

    protected void AddResourcesToDictionary()
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
        if (scienceToken > 0 && scienceToken < 7)
        {
            resources.Add(nameof(scienceToken), scienceToken);
        }
        if (chainingToken > 0 && chainingToken < 7)
        {
            resources.Add(nameof(chainingToken), chainingToken);
        }
    }

    public int CalculateCost()
    {
        Player player;
        Player opponent;
        if (GameController.IsPlayerOneTurn)
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
            if (player.Discounts.Contains(nameof(wood)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardWoodCost, player.Wood, opponent.Wood);
            }
        }
        if (cardOreCost > player.Ore)
        {
            if (player.Discounts.Contains(nameof(ore)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardOreCost, player.Ore, opponent.Ore);
            }
        }
        if (cardClayCost > player.Clay)
        {
            if (player.Discounts.Contains(nameof(clay)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardClayCost, player.Clay, opponent.Clay);
            }
        }
        if (cardStoneCost > player.Stone)
        {
            if (player.Discounts.Contains(nameof(stone)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardStoneCost, player.Stone, opponent.Stone);
            }
        }
        if (cardGlassCost > player.Glass)
        {
            if (player.Discounts.Contains(nameof(glass)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardClayCost, player.Clay, opponent.Clay);
            }
        }
        if (cardPapyrusCost > player.Papyrus)
        {
            if (player.Discounts.Contains(nameof(papyrus)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardPapyrusCost, player.Papyrus, opponent.Papyrus);
            }
        }
        if (cardTextilesCost > player.Textiles)
        {
            if (player.Discounts.Contains(nameof(textiles)))
            {
                cost++;
            }
            else
            {
                cost += ResourceCost(cardTextilesCost, player.Textiles, opponent.Textiles);
            }
        }

        return cost;
    }

    protected int ResourceCost(int resource, int playerResources, int opponentResources)
    {
        var cost = 0;
        cost += (2 * (resource - playerResources)) + opponentResources;

        return cost;
    }

    protected string firstResource;
    protected string secondResource;
    protected string thirdResource;
    protected string chainingResource;

    protected string firstCost;
    protected string secondCost;
    protected string thirdCost;
    protected string chainingCost;

    protected List<Canvas> costCanvases = new List<Canvas>();
    protected List<SpriteRenderer> costSprites = new List<SpriteRenderer>();

    protected List<Canvas> resourceCanvases = new List<Canvas>();
    protected List<SpriteRenderer> resourceSprites = new List<SpriteRenderer>();

    protected virtual void CardSetup()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        foreach (var cost in costPositons)
        {
            if (cardCoinsCost > 0 && !IsSetup(nameof(cardCoinsCost)))
            {
                var prefab = Instantiate(coinPrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardCoinsCost));
                prefab.GetComponentInChildren<Text>().text = cardCoinsCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardWoodCost > 0 && !IsSetup(nameof(cardWoodCost)))
            {
                var prefab = Instantiate(woodPrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardWoodCost));
                prefab.GetComponentInChildren<Text>().text = cardWoodCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardOreCost > 0 && !IsSetup(nameof(cardOreCost)))
            {
                var prefab = Instantiate(orePrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardOreCost));
                prefab.GetComponentInChildren<Text>().text = cardOreCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardClayCost > 0 && !IsSetup(nameof(cardClayCost)))
            {
                var prefab = Instantiate(clayPrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardClayCost));
                prefab.GetComponentInChildren<Text>().text = cardClayCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardStoneCost > 0 && !IsSetup(nameof(cardStoneCost)))
            {
                var prefab = Instantiate(stonePrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardStoneCost));
                prefab.GetComponentInChildren<Text>().text = cardStoneCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardGlassCost > 0 && !IsSetup(nameof(cardGlassCost)))
            {
                var prefab = Instantiate(glassPrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardGlassCost));
                prefab.GetComponentInChildren<Text>().text = cardGlassCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardPapyrusCost > 0 && !IsSetup(nameof(cardPapyrusCost)))
            {
                var prefab = Instantiate(papyrusPrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardPapyrusCost));
                prefab.GetComponentInChildren<Text>().text = cardPapyrusCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
            else if (cardTextilesCost > 0 && !IsSetup(nameof(cardTextilesCost)))
            {
                var prefab = Instantiate(textilesPrefab, cost.transform.position, cost.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignCostName(nameof(cardTextilesCost));
                prefab.GetComponentInChildren<Text>().text = cardTextilesCost.ToString();

                ChangeLayer(renderer, prefab, false);
            }
        }

        foreach (var resource in resourcePositions)
        {
            if (SetupDisounts(renderer, resource))
            {
                continue;
            }
            else if ((scienceToken > 0 && scienceToken < 7) && !IsSetup(nameof(scienceToken)))
            {
                var prefab = Instantiate(scienceTokenPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(scienceToken));
                prefab.GetComponentInChildren<Text>().text = scienceToken.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (militaryStrength > 0 && !IsSetup(nameof(militaryStrength)))
            {
                var prefab = Instantiate(militaryStrengthPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(militaryStrength));
                prefab.GetComponentInChildren<Text>().text = militaryStrength.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (victoryPoints > 0 && !IsSetup(nameof(victoryPoints)))
            {
                var prefab = Instantiate(victoryPointsPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(victoryPoints));
                prefab.GetComponentInChildren<Text>().text = victoryPoints.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (coins > 0 && !IsSetup(nameof(coins)))
            {
                var prefab = Instantiate(coinPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(coins));
                prefab.GetComponentInChildren<Text>().text = coins.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (wood > 0 && !IsSetup(nameof(wood)))
            {
                var prefab = Instantiate(woodPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(wood));
                prefab.GetComponentInChildren<Text>().text = wood.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (ore > 0 && !IsSetup(nameof(ore)))
            {
                var prefab = Instantiate(orePrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(ore));
                prefab.GetComponentInChildren<Text>().text = ore.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (clay > 0 && !IsSetup(nameof(clay)))
            {
                var prefab = Instantiate(clayPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(clay));
                prefab.GetComponentInChildren<Text>().text = clay.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (stone > 0 && !IsSetup(nameof(stone)))
            {
                var prefab = Instantiate(stonePrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(stone));
                prefab.GetComponentInChildren<Text>().text = stone.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (glass > 0 && !IsSetup(nameof(glass)))
            {
                var prefab = Instantiate(glassPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(glass));
                prefab.GetComponentInChildren<Text>().text = glass.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (papyrus > 0 && !IsSetup(nameof(papyrus)))
            {
                var prefab = Instantiate(papyrusPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(papyrus));
                prefab.GetComponentInChildren<Text>().text = papyrus.ToString();

                ChangeLayer(renderer, prefab, true);
            }
            else if (textiles > 0 && !IsSetup(nameof(textiles)))
            {
                var prefab = Instantiate(textilesPrefab, resource.transform.position, resource.transform.rotation);
                prefab.transform.parent = gameObject.transform;
                AssignResourceName(nameof(textiles));
                prefab.GetComponentInChildren<Text>().text = textiles.ToString();

                ChangeLayer(renderer, prefab, true);
            }
        }
    }

    protected bool IsSetup(string name)
    {
        if (name == firstResource || name == secondResource || name == thirdResource || name == chainingResource)
        {
            return true;
        }

        if (name == firstCost || name == secondCost || name == thirdCost || name == chainingCost)
        {
            return true;
        }

        return false;
    }

    protected void AssignCostName(string name)
    {
        if (firstCost == null)
        {
            firstCost = name;
        }
        else if (secondCost == null)
        {
            secondCost = name;
        }
        else if (thirdCost == null)
        {
            thirdCost = name;
        }
    }

    protected void AssignResourceName(string name)
    {
        if (firstResource == null)
        {
            firstResource = name;
        }
        else if (secondResource == null)
        {
            secondResource = name;
        }
        else if (thirdResource == null)
        {
            thirdResource = name;
        }
    }
    
    protected void AssignChainingResourceName(string name)
    {
        if (chainingResource == null)
        {
            chainingResource = name;
        }
    }
    
    protected void AssignChainingCostName(string name)
    {
        if (chainingCost == null)
        {
            chainingCost = name;
        }
    }

    

    protected void ChangeLayer(SpriteRenderer renderer, GameObject prefab, bool isResource)
    {
        var canvas = prefab.GetComponentInChildren<Canvas>();
        canvas.sortingOrder = renderer.sortingOrder + 1;

        var sprite = prefab.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = renderer.sortingOrder + 1;

        if (isResource)
        {
            resourceCanvases.Add(canvas);
            resourceSprites.Add(sprite);
        }
        else
        {
            costCanvases.Add(canvas);
            costSprites.Add(sprite);
        }
    }

    public virtual void ChangeLayerWithCard()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();

        foreach (var canvas in costCanvases)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = renderer.sortingOrder + 1;
        }
        foreach (var sprite in costSprites)
        {
            sprite.sortingOrder = renderer.sortingOrder + 1;
        }
        foreach (var canvas in resourceCanvases)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = renderer.sortingOrder + 1;
        }
        foreach (var sprite in resourceSprites)
        {
            sprite.sortingOrder = renderer.sortingOrder + 1;
        }
    }

    private bool SetupDisounts(SpriteRenderer renderer, GameObject resource)
    {
        if (woodDiscount && !IsSetup(nameof(woodDiscount)))
        {
            var prefab = Instantiate(woodPrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(woodDiscount));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "wood";
            return true;
        }
        else if (oreDiscount && !IsSetup(nameof(oreDiscount)))
        {
            var prefab = Instantiate(orePrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(oreDiscount));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "ore";
            return true;
        } 
        else if (clayDiscount && !IsSetup(nameof(clayDiscount)))
        {
            var prefab = Instantiate(clayPrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(cardTextilesCost));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "clay";
            return true;
        }
        else if (stoneDiscount && !IsSetup(nameof(stoneDiscount)))
        {
            var prefab = Instantiate(stonePrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(stoneDiscount));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "stone";
            return true;
        }
        else if (glassDiscount && !IsSetup(nameof(glassDiscount)))
        {
            var prefab = Instantiate(glassPrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(glassDiscount));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "glass";
            return true;
        }
        else if (papyrusDiscount && !IsSetup(nameof(papyrusDiscount)))
        {
            var prefab = Instantiate(papyrusPrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(papyrusDiscount));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "papyrus";
            return true;
        }
        else if (textilesDiscount && !IsSetup(nameof(textilesDiscount)))
        {
            var prefab = Instantiate(textilesPrefab, resource.transform.position, resource.transform.rotation);
            prefab.transform.parent = gameObject.transform;
            AssignCostName(nameof(textilesDiscount));
            prefab.GetComponentInChildren<Text>().text = "";

            ChangeLayer(renderer, prefab, false);

            cardDiscount = "textiles";
            return true;
        }

        return false;
    }
}
