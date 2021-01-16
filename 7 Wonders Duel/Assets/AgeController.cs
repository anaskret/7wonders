using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeController : MonoBehaviour
{
    [SerializeField] private GameObject[] cards;
    [SerializeField] private GameObject nextAge;
    [SerializeField] private bool isThirdAge;
    [SerializeField] private GameObject victoryScreen;

    private void Update()
    {
        if (!AreAnyCardsLeft())
        {
            if (isThirdAge)
            {
                victoryScreen.GetComponent<VictoryController>().PointVictory();
            }
            NextAge();
        }
    }

    private bool AreAnyCardsLeft()
    {
        foreach(var card in cards)
        {
            var controller = card.GetComponent<CardController>();
            if (!controller.isTaken) 
            {
                return true;
            }
        }

        return false;
    }

    private void NextAge()
    {
        nextAge.SetActive(true);
        gameObject.SetActive(false);
    }
}
