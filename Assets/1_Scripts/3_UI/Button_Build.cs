using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Build : MonoBehaviour
{
    //public Button ConstructButton;
    //public HexFeatureManager features;
    //public HexMesh hexMesh;
    //public HexGridChunk hexGridChunk;
    //public HexGrid hexgrid;

    public GameObject Panel;
    //public Button Living, Research, Industrial;
    public HexGrid hexGrid;
    HexFeatureManager features;
    HexGridChunk gridChunk;
    HexCell cell;
    Vector3 position;

    //Button button;
    //HexCell GetCellUnderCursor()
    //{
    //    return
    //        hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    //}


    void isSpecial()
    {

    }

    void Build()
    {
        
    }

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActivate = Panel.activeSelf;

            Panel.SetActive(!isActivate);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        

    }


    // Update is called once per frame
    void Update()
    {

    }
}
