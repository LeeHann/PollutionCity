using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] GameObject ClearPanel;
    [SerializeField] TMP_Text ClearTMP;
    [SerializeField] Text ClearText;
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSource;

    static public List<City> cities; 
    private static PlayerNum whoseTurn;

    public static City turnCity {
        get {
            return cities[(int)whoseTurn];
        }
    }

    public int Turn {
        get;
        private set; 
    }

    enum ClearType {
        AllLose,
        AIWin,
        PlayerWin,
        PlayerLose,
        Pass
    }

    [SerializeField] HexGrid hexGrid;
    [SerializeField] MapSetter mapSetter;
    int scatterTurn = 5;
    bool overCheck;

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
        if (type != ClearType.Pass) {
            switch (type)
            {
                case ClearType.AllLose:
                    ClearTMP.text = "패배";
                    ClearText.text = "모든 세상이 오염되었습니다.";
                    audioSource.clip = clips[0];
                    break;
                case ClearType.AIWin:
                    ClearTMP.text = "패배";
                    ClearText.text = "다른 도시가 먼저 정화되었습니다.";
                    audioSource.clip = clips[0];
                    break;
                case ClearType.PlayerLose:
                    ClearTMP.text = "패배";
                    ClearText.text = "도시가 오염되었습니다.";
                    audioSource.clip = clips[0];
                    break;
                case ClearType.PlayerWin:
                    ClearTMP.text = "승리";
                    ClearText.text = "가장 먼저 도시를 정화했습니다.";
                    audioSource.clip = clips[1];
                    break;
            }
            ClearPanel.SetActive(true);
            yield return new WaitUntil(()=> overCheck == true);
            Loading.LoadSceneHandle("Title");
        } else {
            if (scatterTurn <= 0)
            {
                mapSetter.ScatterResources(Random.Range(0, 5));
                scatterTurn = 5;
            }

            turnPlayer.MyTurn();
            yield return new WaitWhile(()=> turnPlayer.myTurn != false);
            
            turnPlayer.PA += (int)(turnPlayer.PA * GameInfo.PLPercent);
            // turnPlayer.PA += (int)(turnPlayer.PA * 0.5);
            CheckSpin();
        }
    }

    void CheckSpin()
    {
        do{
            whoseTurn = (PlayerNum)((int)(whoseTurn + 1) % 4);
            if (whoseTurn == 0) Turn++;
        } while (cities[(int)whoseTurn] == null);
        scatterTurn--;
        StopAllCoroutines();
        StartCoroutine(SpinATurn());
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
                    / (float)(hexGrid.cellCountX * hexGrid.cellCountZ);
        if ((allPL / (float)(GameInfo.cityCount + 1)) >= 80.0f) 
        {
            return ClearType.AllLose;
        }

        // 지역 오염도 체크 - 패배하는 도시 체크
        for (int i=0; i<cities.Count; i++)
        {
            if (cities[i] != null)
            {
                if (cities[i].PL >= GameInfo.losePL)
                {
                    cities[i].Lose();
                    if (i == 0) return ClearType.PlayerLose;
                }
                    
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

    public void OnClickOver()
    {
        overCheck = true;
    }
}
