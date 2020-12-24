using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictTokenController : MonoBehaviour
{
    Player playerOne;
    Player playerTwo;

    [SerializeField] private GameObject[] conflictPoints;

    private int position = 7;

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
        position += (playerOne.MilitaryStrength - playerTwo.MilitaryStrength);
    }
}
