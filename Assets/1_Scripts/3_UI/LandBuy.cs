using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LandBuy : MonoBehaviour
{
    public Resources highlight;
   
    HexCell hexcell;
    HexGridChunk hexGridChunk;
    HexGrid hexGrid;
    HexDirection hexDirection;
    Canvas gridCanvas;
    bool walled;
    public bool land;
   

  



    public void BuyLand()
    {
        
        Debug.Log("BuyTile Button");


        //hexcell.BuyTile(hexDirection, hexcell);

        //if (hexcell.SetNeighbor(hexDirection, hexcell) && !hexcell.Walled)
        //{
        //    hexcell.EnableHighlight(Color.red);
        //}



    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



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

�ѳ����� ���Ű� HexUnit����.

public void OnPointerClick(PointerEventData e)
{
    if (!TurnUnit) return;                         => ���� �ƴϸ� �׳� ����
    Queue queue = new Queue();                     
    queue.Enqueue(Location);

    while (queue.Count > 0)                         => �����̼� ť�� Count�� ���� 0�̾ƴϸ�
    {
        HexCell cell = (HexCell)queue.Dequeue();    => ť , ��ť
        cell.EnableHighlight(Color.yellow);         => ���� ��������
        highlights.Add(cell);                       => ���� ���̶���Ʈ Add

        cell.highlightBtn.onClick.RemoveAllListeners();                             => ��Ŭ�������� �ʱ�ȭ
        cell.highlightBtn.onClick.AddListener(() => OnClickHighlight(cell));        => ��Ŭ�������ʿ� ��Ŭ��(���̶���Ʈ)÷��

        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            HexCell neighbor = cell.GetNeighbor(d);                                             => �̿�.�̿�������

            if (neighbor.IsEnabledHighlight() || !Grid.Search(Location, neighbor, this))        => ���̶���ƮȰ��ȭ�Ǹ� ��ġ ����
                continue;

            queue.Enqueue(neighbor);                                                            => �̿� ť��

        }
    }
    Location.DisableHighlight();                    => �ٲ�ź���� ���̶���Ʈ ��Ȱ��ȭ
}
public void OnClickHighlight(HexCell cell)
{
    for (int i = highlights.Count - 1; i > 0; i--)
    {
        highlights[i].DisableHighlight();
        highlights.RemoveAt(i);
    }
    Grid.FindPath(Location, cell, this);            => ��ã��
    Travel(Grid.GetPath());                         => �̵�
}



[HideInInspector] public Button highlightBtn;
RectTransform uiRect;


public void EnableHighlight(Color color)                                => ���̶���Ʈ �÷� ����
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