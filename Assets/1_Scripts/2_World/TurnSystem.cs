using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    static public List<City> cities; 
    private PlayerNum whoseTurn;
    public bool boolOver;

    [SerializeField] HexGrid hexGrid;
    [SerializeField] HexMapCamera cam;
    [SerializeField] MapSetter mapSetter;
    int scatterTurn = 5;

    private void Start() 
    {
        whoseTurn = 0;
        StartCoroutine(SpinATurn());
    }

    IEnumerator SpinATurn()
    {   
        City turnPlayer = cities[(int)whoseTurn];
        if (scatterTurn <= 0)
        {
            mapSetter.ScatterResources(Random.Range(0, 5));
            scatterTurn = 5;
        }
        Debug.Log(string.Format(
                "turnPlayer is {0} whose sit is {1}", 
                whoseTurn, turnPlayer.sit
            )
        );
        turnPlayer.PA += (int)(turnPlayer.PA * 0.033f);

        turnPlayer.MyTurn();
        yield return new WaitWhile(()=> turnPlayer.myTurn != false);

        bool isOver = boolOver;
        if (isOver)
        {
            Debug.Log("game is over. the winner is ~");
        } else
        {
            do{
                whoseTurn = (PlayerNum)((int)(whoseTurn + 1) % 4);
            } while (turnPlayer == null);
            scatterTurn--;
            StartCoroutine(SpinATurn());
        }
    }
}
