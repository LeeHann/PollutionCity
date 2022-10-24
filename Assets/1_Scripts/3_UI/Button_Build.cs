using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Build : MonoBehaviour
{
    //public Button ConstructButton;
    //public HexFeatureManager features;
    //public HexMesh hexMesh;
    //public HexGridChunk hexGridChunk;
    //public HexGrid hexgrid;

    public GameObject Panel;
    public Button Living, Research, Industrial;
  //  public HexGrid hexGrid;
    HexFeatureManager hexFeatureManager;

    //Button button;
    //HexCell GetCellUnderCursor()
    //{
    //    return
    //        hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    //}
    void isSpecial()
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
        //button = GetComponent<Button>();
        //button.onClick.AddListener(LiveBuilding);
    }


    //void LiveBuilding()
   // {
        //HexCell cell = GetCellUnderCursor();
        //    if (cell && !cell.Unit)
        //    {
        //        hexGrid.AddUnit(
        //            Instantiate(HexUnit.unitPrefab), cell, Random.Range(0f, 360f)
        //        );

        //    }
  //  }
    // Update is called once per frame
    void Update()
    {

    }
}
