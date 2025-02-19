﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonderSelectionController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject[] newCards;
    [SerializeField] private GameObject selectionWindow;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject selectionText;
    [SerializeField] private GameObject age1;
    [SerializeField] private GameObject age2;
    [SerializeField] private GameObject age3;

    private bool isAgeSetUp = false;

    private void Start()
    {
        gameObject.SetActive(true);
        WonderSelectionTurn.Reset();
        GameController.Reset();
        age1.SetActive(false);
        age2.SetActive(false);
        age3.SetActive(false);
    }

    private void Update()
    {
        if (WonderSelectionTurn.PlayerTurn == 1)
        { 
            text.text = "It's Players One Turn";
        }
        else
        {
            text.text = "It's Players Two Turn";
        }

        if (WonderSelectionTurn.Index >= 4)
        {
            if (WonderSelectionTurn.Round == 0)
            {
                WonderSelectionTurn.NewRound();
                foreach(var card in newCards)
                {
                    card.SetActive(true);
                }
            }
            else if(WonderSelectionTurn.Round == 1 && !isAgeSetUp)
            {
                selectionWindow.SetActive(false);
                selectionText.SetActive(false);
                background.SetActive(false);
                age1.SetActive(true);
                isAgeSetUp = true;
            }
        }
    }
}
