using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTextController : MonoBehaviour
{
    [SerializeField] private Text turn;

    private void Start()
    {
        turn = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (GameController.IsPlayerOneTurn)
        {
            turn.text = "Player 1 turn";
        }
        else
        {
            turn.text = "Player 2 turn";
        }
    }
}
