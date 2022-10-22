using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    static public List<City> cities; 
    public PlayerNum turnPlayer;
    public bool boolOver;

    private void Start() 
    {
        turnPlayer = 0;
        StartCoroutine(SpinATurn());
    }

    IEnumerator SpinATurn()
    {
        Debug.Log(string.Format("turnPlayer is {0} whose sit is {1}", turnPlayer, cities[(int)turnPlayer].sit));

        yield return new WaitForSeconds(1f);

        bool isOver = boolOver;
        if (isOver)
        {
            Debug.Log("game is over. the winner is ~");
        } else
        {
            do{
                turnPlayer = (PlayerNum)((int)(turnPlayer + 1) % 4);
            } while (cities[(int)turnPlayer] == null);
            StartCoroutine(SpinATurn());
        }
    }
}
