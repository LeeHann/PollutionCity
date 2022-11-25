using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;

    static public List<City> cities; 
    private static PlayerNum whoseTurn;

    public static City turnCity {
        get {
            return cities[(int)whoseTurn];
        }
    }

    public static int Turn {
        get;
        private set; 
    }

    enum ClearType {
        AllLose,
        AIWin,
        PlayerWin,
        Pass
    }

    [SerializeField] HexGrid hexGrid;
    [SerializeField] HexMapCamera cam;
    [SerializeField] MapSetter mapSetter;
    int scatterTurn = 5;
    

    private void Start() 
    {
        Turn = 1;
        whoseTurn = 0;
        StartCoroutine(SpinATurn());
    }

    IEnumerator SpinATurn()
    {   
        City turnPlayer = cities[(int)whoseTurn];
        Debug.Log(string.Format("turnPlayer is {0} whose sit is {1}", whoseTurn, turnPlayer.sit));
        ClearType type = CheckClear(turnPlayer);
        if (type != 0) {
            switch (type)
            {
                case ClearType.AllLose:

                    break;
                case ClearType.AIWin:

                    break;
                case ClearType.PlayerWin:

                    break;
            }
        } else {
            if (scatterTurn <= 0)
            {
                mapSetter.ScatterResources(Random.Range(0, 5));
                scatterTurn = 5;
            }

            turnPlayer.MyTurn();
            yield return new WaitWhile(()=> turnPlayer.myTurn != false);
            
            // turnPlayer.PA += (int)(turnPlayer.PA * GameInfo.PLPercent);
            turnPlayer.PA += (int)(turnPlayer.PA * 0.5);

            do{
                whoseTurn = (PlayerNum)((int)(whoseTurn + 1) % 4);
                if (whoseTurn == 0) Turn++;
            } while (cities[(int)whoseTurn] == null);
            scatterTurn--;
            StartCoroutine(SpinATurn());
        }
    }

    ClearType CheckClear(City turnPlayer)
    {
        // 전체 오염도 체크 - 오염도 넘어가면 전체 패배
        int cells = 0;
        float allPL = 0;
        for (int i=0; i<cities.Count; i++)
        {
            if (cities[i] != null)
            {
                cells += cities[i].cells.Count;
                allPL += cities[i].PL;
            }
        }
        allPL += (hexGrid.cellCountX * hexGrid.cellCountZ - (hexGrid.emptyCells.Count + cells)) 
                    / hexGrid.cellCountX * hexGrid.cellCountZ;
        if ((allPL / (GameInfo.cityCount + 1)) >= 80.0f) 
        {
            return ClearType.AllLose;
        }

        // 지역 오염도 체크 - 패배하는 도시 체크
        for (int i=0; i<cities.Count; i++)
        {
            if (cities[i] != null)
            {
                if (cities[i].PL >= GameInfo.losePL)
                    cities[i].Lose();
            }
        }

        // 현재 플레이어 오염도 체크 - 지정 오염도 이하면 승리 & 플레이어가 아니라면 플레이어 자동 패배
        if (turnPlayer.PL <= GameInfo.winPL)
        {
            if (whoseTurn == 0)
                return ClearType.PlayerWin;
            else
                return ClearType.AIWin;
        }

        // 턴이 다 끝났는지 체크 - 가장 오염도 낮은 곳 승리
        if (Turn == GameInfo.maxTurn + 1)
        {
            int min = GameInfo.maxPA + 1;
            ClearType type = ClearType.AllLose;
            for (int i=0; i<cities.Count; i++)
            {
                if (cities[i] != null)
                {
                    if (min > cities[i].PA)
                    {
                        min = cities[i].PA;
                        if (i == 0) type = ClearType.PlayerWin;
                        else type = ClearType.AIWin;
                    }
                }
            }
            return type;
        }

        return ClearType.Pass;
    }
}
