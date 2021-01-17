using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictTokenController : MonoBehaviour
{
    Player playerOne;
    Player playerTwo;

    [SerializeField] private GameObject[] conflictPoints;

    private int position = 8;
    private int lastPosition = 8;

    private bool firstPhasePlayerOne = false;
    private bool secondPhasePlayerOne = false;
    private bool firstPhasePlayerTwo = false;
    private bool secondPhasePlayerTwo = false;

    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = conflictPoints[position].transform.position;
    }

    public void MilitaryStrengthChange()
    {
        if ((playerOne.MilitaryStrength - playerTwo.MilitaryStrength) == 0)
        {
            position = 8;
        }
        else
        {
            position = 8 +  (playerOne.MilitaryStrength - playerTwo.MilitaryStrength);
        }

        if(position == 8 && lastPosition == 7)
        {
            playerTwo.ChangeVictoryPoints(-2);
            lastPosition = position;
        }
        if(position == 7)
        {
            playerTwo.ChangeVictoryPoints(2);
            lastPosition = position;
        }
        if(position == 6 && lastPosition == 5)
        {
            playerTwo.ChangeVictoryPoints(-3);
            lastPosition = position;
        }
        if(position == 5)
        {
            playerTwo.ChangeVictoryPoints(3);
            if (firstPhasePlayerTwo)
            {
                playerTwo.ChangeCoins(2);
                firstPhasePlayerTwo = true;
            }
            lastPosition = position;
        }
        if(position == 4 && lastPosition == 3)
        {
            playerTwo.ChangeVictoryPoints(-5);
            lastPosition = position;
        }
        if(position == 3)
        {
            playerTwo.ChangeVictoryPoints(5);
            if (secondPhasePlayerTwo)
            {
                playerTwo.ChangeCoins(2);
                secondPhasePlayerTwo = true;
            }
            lastPosition = position;
        }

        if (position == 8 && lastPosition == 9)
        {
            playerOne.ChangeVictoryPoints(-2);
            lastPosition = position;
        }
        if (position == 9)
        {
            playerOne.ChangeVictoryPoints(2);
            lastPosition = position;
        }
        if(position == 10 && lastPosition == 11)
        {
            playerOne.ChangeVictoryPoints(-3);
            lastPosition = position;
        }
        if(position == 11)
        {
            playerOne.ChangeVictoryPoints(3);
            if (firstPhasePlayerOne)
            {
                playerOne.ChangeCoins(2);
                firstPhasePlayerOne = true;
            }
            lastPosition = position;
        }
        if(position == 12 && lastPosition == 13)
        {
            playerOne.ChangeVictoryPoints(-5);
            lastPosition = position;
        }
        if(position == 13)
        {
            playerOne.ChangeVictoryPoints(5);
            if (secondPhasePlayerOne)
            {
                playerOne.ChangeCoins(2);
                secondPhasePlayerOne = true;
            }
            lastPosition = position;
        }
    }
}
