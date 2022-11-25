using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildSet : MonoBehaviour
{

    public HexGrid hexgrid;
    public HexCell hexCell;
    HexUnit hexUnit;
    Transform container;

    public GameObject ScrollView; // 제조 스크롤뷰 UI 연동
    public GameObject ResearchTree;     //연구트리 UI  연동


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
    public void IndustrialBuild()
    {
        HexCell cell = GetCellUnderCursor();

        if (cell && !cell.Unit)
        {
            hexgrid.AddIndustrialBuilding(
                Instantiate(HexUnit.IndustrialPrefab),
                cell,
                Random.Range(0f, 360f)
                );
        }

        Debug.Log("Industrial build Button");

        if (HexUnit.IndustrialPrefab && ScrollView != null)
        {
            bool isActivate = ScrollView.activeSelf;

            ScrollView.SetActive(!isActivate);
        }

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

}
