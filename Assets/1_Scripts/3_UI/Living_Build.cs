using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Living_Build : MonoBehaviour
{


    public HexGrid hexgrid;
    public HexCell hexCell;
    public GameObject prefab;
    Transform container;




    public void Clear()
    {
        if (container)
        {
            Destroy(container.gameObject);
        }
        container = new GameObject("Features Container").transform;
        container.SetParent(transform, false);
    }

    public HexCell Location
    {
        get       
        {
            return location;
        }
        set
        {
            location = value;
            transform.localPosition = value.Position;
        }
    }

    HexCell location;



    public HexCell GetCellUnderCursor()
    {
        return
            hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    public void LivingBuild()
    {

        Debug.Log("Clicked Livingbuild Button");
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit)
        {
                hexgrid.AddLivingBuilding(
                Instantiate(HexUnit.LivingPrefab),
                cell,
                Random.Range(0f, 360f)
                );
        }
        
    }
    


    void Start()
    {

    }


    void Update()
    {

    }
}
