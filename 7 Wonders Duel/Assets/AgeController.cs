using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeController : MonoBehaviour
{
    [SerializeField] private GameObject[] cards;
    [SerializeField] private GameObject nextAge;

    private void Update()
    {
        if (!AreAnyCardsLeft())
        {
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
    }
}
