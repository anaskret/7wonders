using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    Player playerOne;
    Player playerTwo;

    [SerializeField] private Text victoriousPlayer;
    [SerializeField] private Text victoryType;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;

    [SerializeField] private GameObject game;

    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    void Update()
    {
        MilitaryVictory();
        ScienceVictory();
    }

    private void MilitaryVictory()
    {
        if (playerOne.MilitaryStrength - playerTwo.MilitaryStrength >= 8)
        {
            victoriousPlayer.text = "Player 1 won";
            victoryType.text = "Military Victory";
            ShowVictory();
        }
        else if(playerTwo.MilitaryStrength - playerOne.MilitaryStrength >= 8)
        {
            victoriousPlayer.text = "Player 2 won";
            victoryType.text = "Military Victory";
            ShowVictory();
        }
    }
    
    private void ScienceVictory()
    {
        if (playerOne.ScienceTokens.Count == 6)
        {
            victoriousPlayer.text = "Player 1 won";
            victoryType.text = "Science Victory";
            ShowVictory();
        }
        else if(playerTwo.ScienceTokens.Count == 6)
        {
            victoriousPlayer.text = "Player 2 won";
            victoryType.text = "Science Victory";
            ShowVictory();
        }
    }
    
    public void PointVictory()
    {
        if (playerOne.VictoryPoints > playerTwo.VictoryPoints)
        {
            victoriousPlayer.text = "Player 1 won";
            victoryType.text = "Point Victory";
            ShowVictory();
        }
        else if(playerTwo.VictoryPoints > playerOne.VictoryPoints)
        {
            victoriousPlayer.text = "Player 2 won";
            victoryType.text = "Point Victory";
            ShowVictory();
        }
        else if(playerTwo.VictoryPoints == playerOne.VictoryPoints)
        {
            victoriousPlayer.text = "Draw";
            victoryType.text = "";
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
