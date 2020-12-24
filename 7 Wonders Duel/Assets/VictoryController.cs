using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    Player playerOne;
    Player playerTwo;

    [SerializeField] private Text victoriousPlayer;
    [SerializeField] private Text victoryType;

    [SerializeField] private GameObject game;

    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        MilitaryVictory();
    }

    private void MilitaryVictory()
    {
        if (playerOne.MilitaryStrength - playerTwo.MilitaryStrength >= 7)
        {
            victoriousPlayer.text = "Player 1 won";
            victoryType.text = "Military Victory";
            ShowVictory();
        }
        else if(playerTwo.MilitaryStrength - playerOne.MilitaryStrength >= 7)
        {
            victoriousPlayer.text = "Player 2 won";
            victoryType.text = "Military Victory";
            ShowVictory();
        }
    }

    private void ShowVictory()
    {
        gameObject.transform.position = new Vector3(0.62f, 0.82f);

        var buttons = game.GetComponentsInChildren<Button>();
        foreach(var button in buttons)
        {
            button.interactable = false;
        }
    }
}
