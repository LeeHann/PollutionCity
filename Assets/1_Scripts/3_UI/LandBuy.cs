using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LandBuy : MonoBehaviour
{
    public Resources highlight;
    public MapSetter mapSetter;
    HexGrid hexGrid;
    public bool land;



    public HexCell GetCellUnderCursor()
    {
        return
            hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
    public void BuyLand()
    {
        Debug.Log("BuyTile Button");
        mapSetter.OnLandBuyButton();
    }

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(BuyLand);
    }

}
