using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Research_Build : MonoBehaviour
{




    public HexGrid hexgrid;
    public GameObject prefab;
    public GameObject ResearchTree;     //연구트리 UI  연동
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


    public HexCell GetCellUnderCursor()
    {
        return
            hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
    public void ResearchBuild()
    {

        Debug.Log("Research Button");
    
        HexCell cell = GetCellUnderCursor();
        
        if (cell && !cell.Unit)
        {
            hexgrid.AddResearchBuilding(
                Instantiate(HexUnit.ResearchPrefab),
                cell,
                Random.Range(0f, 360f)
                );
        }
        if (HexUnit.ResearchPrefab && ResearchTree != null)
            {
                bool isActivate = ResearchTree.activeSelf;

                ResearchTree.SetActive(!isActivate);
            }
        

    }



    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
