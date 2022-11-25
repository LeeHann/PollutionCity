using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LandBuy : MonoBehaviour
{
    public Resources highlight;
    public MapSetter mapSetter;
    HexUnit hexunit;
    HexCell cell;
    HexGridChunk hexGridChunk;
    HexGrid hexGrid;
    City city;
    HexDirection hexDirection;
    Canvas gridCanvas;
    bool walled;
    public bool land;
    bool isPlayer;



    public HexCell GetCellUnderCursor()
    {
        return
            hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }//=>Landbuy city..
    public void BuyLand()
    {
        Debug.Log("BuyTile Button");
        //for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        //{
        //    hexcell.BuyTile(d, hexcell);
        //}



        /*
          
        정리.
        MapSetter에서 SetCityProperty 로 isPlayer도시 설정된거 +  City의 AddCell로 활성화된 자기 도시의 cell들과 반대편인 이웃 셀들 하이라이트 활성화.
        해당 cell 누르면 이웃된 벽 다 지우고 지운부분 제외 다 벽으로 설정 + 그 cell도 자기도시로 지정 ( 무한 반복 )
        ==> walled는 
        금화로 살수 있도록 해야함 금화는 그냥 Money로 지정함
        


        */
        //mapSetter.LandBuy(hexcell,isPlayer);
        //mapSetter.OnLandBuyButton();
        mapSetter.OnLandBuyButton();
    }

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(BuyLand);
    }
}


















//public bool SelectCity()
//{
//    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//    RaycastHit hit;

//    if(Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
//    {
//        if(hit.transform.GetComponent<HexGridChunk>)
//    }

//}


//public HexCell GetNeighbor(HexDirection direction)
//{

//    return neighbors[(int)direction];
//}
//public void BuyTile(HexDirection direction, HexCell cell)
//{
//    GetNeighbor(direction);
//    hexcell.SetNeighbor(direction, cell);
//    if (!walled)
//    {
//        EnableHighlight(Color.red);
//    }
//}




/*

한나님이 쓰신거 HexUnit에서.

public void OnPointerClick(PointerEventData e)
{
    if (!TurnUnit) return;                         => 유닛 아니면 그냥 리턴
    Queue queue = new Queue();                     
    queue.Enqueue(Location);

    while (queue.Count > 0)                         => 로케이션 큐에 Count가 아직 0이아니면
    {
        HexCell cell = (HexCell)queue.Dequeue();    => 큐 , 디큐
        cell.EnableHighlight(Color.yellow);         => 셀에 색입히기
        highlights.Add(cell);                       => 셀에 하이라이트 Add

        cell.highlightBtn.onClick.RemoveAllListeners();                             => 온클릭리스터 초기화
        cell.highlightBtn.onClick.AddListener(() => OnClickHighlight(cell));        => 온클릭리스너에 온클릭(하이라이트)첨부

        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            HexCell neighbor = cell.GetNeighbor(d);                                             => 이웃.이웃셀방향

            if (neighbor.IsEnabledHighlight() || !Grid.Search(Location, neighbor, this))        => 하이라이트활성화되면 서치 ㄴㄴ
                continue;

            queue.Enqueue(neighbor);                                                            => 이웃 큐인

        }
    }
    Location.DisableHighlight();                    => 다끄탄고나면 하이라이트 비활성화
}
public void OnClickHighlight(HexCell cell)
{
    for (int i = highlights.Count - 1; i > 0; i--)
    {
        highlights[i].DisableHighlight();
        highlights.RemoveAt(i);
    }
    Grid.FindPath(Location, cell, this);            => 길찾기
    Travel(Grid.GetPath());                         => 이동
}



[HideInInspector] public Button highlightBtn;
RectTransform uiRect;


public void EnableHighlight(Color color)                                => 하이라이트 컬러 설정
{
    uiRect = hexcell.GetComponent<RectTransform>();                     
    if (highlight == null)
        highlight = uiRect.GetChild(0).GetComponent<Image>();
    if (highlightBtn == null)
        highlightBtn = highlight.GetComponent<Button>();
    highlight.color = color;
    highlight.enabled = true;
}


*/






//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//RaycastHit hitInfo;

//if (Physics.Raycast(ray, out hitInfo))
//{
//    GameObject ourhitObject = hitInfo.collider.transform.gameObject;
//    Debug.Log("Hit an object with name : " + ourhitObject.name);

//    if (Input.GetMouseButtonDown(0))
//    {
//        MeshRenderer mr = ourhitObject.GetComponentInChildren<MeshRenderer>();

//        if (mr.material.color == Color.red)
//        {
//            mr.material.color = Color.white;
//        }
//        else
//        {
//            mr.material.color = Color.red;
//        }
//    }
//}