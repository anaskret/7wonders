using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScreenController : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    /*Player playerOne;
    Player playerTwo;

    private void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Player>();
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyCard(Player opponent)
    {
        for(int i = 0; i < opponent.Cards.Count; i++)
        {
            opponent.Cards[i].transform.position = points[i].transform.position;
            opponent.Cards[i].GetComponent<CardController>().ChangeLayerWithCard();
        }
    }
}
