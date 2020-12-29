using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonderCardController : CardModel
{
    public bool isBuilt;

    [SerializeField] private GameObject checkmark;
    [SerializeField] private GameObject cross;

    public GameObject cardUsedToBuild;

    public void WonderSelected()
    {
        button.interactable = false;
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

    public void Build(Player player, GameObject card)
    {
        player.AddResources(resources);
        player.SpendCoins(CalculateCost());
        isBuilt = true;
        GameController.NumberOfWonders++;
        cardUsedToBuild = card;

        checkmark.SetActive(true);
    }
    protected override void CardSetup()
    {
        Canvas renderer = gameObject.GetComponentInParent<Canvas>();

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
            if ((scienceToken > -1 && scienceToken < 7) && !IsSetup(nameof(scienceToken)))
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

    protected void ChangeLayer(Canvas renderer, GameObject prefab, bool isResource)
    {
        var canvas = prefab.GetComponentInChildren<Canvas>();
        canvas.overrideSorting = true; 
        canvas.sortingOrder = renderer.sortingOrder + 2;

        var sprite = prefab.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = renderer.sortingOrder + 2;

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

    public override void ChangeLayerWithCard()
    {
        var renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        foreach (var canvas in costCanvases)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = renderer.sortingOrder + 3;
        }
        foreach (var sprite in costSprites)
        {
            sprite.sortingOrder = renderer.sortingOrder + 2;
        }
        foreach (var canvas in resourceCanvases)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = renderer.sortingOrder + 3;
        }
        foreach (var sprite in resourceSprites)
        {
            sprite.sortingOrder = renderer.sortingOrder + 2;
        }

        if (isBuilt)
        {
            var sprites = checkmark.GetComponentsInChildren<SpriteRenderer>();
            foreach(var sprite in sprites)
            {
                sprite.sortingOrder = renderer.sortingOrder + 2;
            }
        }
    }
}
