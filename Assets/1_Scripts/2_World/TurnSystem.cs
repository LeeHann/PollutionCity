using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    static public List<City> cities; 
    public PlayerNum turnPlayer;
    public bool boolOver;

    [SerializeField] HexMapCamera cam;

    private void Start() 
    {
        turnPlayer = 0;
        StartCoroutine(SpinATurn());
    }

    IEnumerator SpinATurn()
    {
        Debug.Log(string.Format(
            "turnPlayer is {0} whose sit is {1}", 
            turnPlayer, 
            cities[(int)turnPlayer].sit
            )
        );

        cities[(int)turnPlayer].MyTurn();
        yield return new WaitWhile(()=> cities[(int)turnPlayer].myTurn != false);

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

    void CameraPositioning(GameObject obj)
    {
        cam.transform.localPosition = obj.transform.localPosition;
    }
}
