using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildSet : MonoBehaviour
{

    HexMapEditor hexMapEditor;
    public HexGrid hexgrid;
    public HexCell hexCell;
    HexUnit hexUnit;
    Transform container;

    public GameObject ScrollView; // 제조 스크롤뷰 UI 연동
    public GameObject ResearchTree;     //연구트리 UI  연동
    // HexMapEditor hexMapEditor;
    HexFeatureManager hexFeatureManager;
    HexGridChunk hexGridChunk;
    Vector3 position;

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

        //hexMapEditor.GetCellUnderCursor();
        ////Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
        //hexMapEditor.SetEditMode(true);
        //hexMapEditor.SetApplySpecialIndex(true);
        //hexMapEditor.SetSpecialIndex(0);

        //if (Input.GetMouseButton(0))
        //{
        //    hexFeatureManager.AddLivingBuilding(cell, position);
        //}

        //hexMapEditor.SetApplySpecialIndex(false);
        //hexMapEditor.SetEditMode(false);
        //if (Input.GetMouseButton(0))
        //{
        //    hexMapEditor.CreateLivingBuilding();
        //}

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
        
        //hexMapEditor.CreateLivingBuilding();



    }
    public void IndustrialBuild()
    {
        //hexMapEditor.SetEditMode(true);
        //hexMapEditor.SetApplySpecialIndex(true);
        //hexMapEditor.SetSpecialIndex(2);
        //hexMapEditor.GetCellUnderCursor();
        //if (Input.GetMouseButton(0))
        //{
        //    hexFeatureManager.AddIndustrialBuilding(cell, position);
        //    hexMapEditor.SetApplySpecialIndex(false);
        //    hexMapEditor.SetEditMode(false);
        //}
        HexCell cell = GetCellUnderCursor();
        //if (Input.GetMouseButtonDown(0))

        if (cell && !cell.Unit)
        {
            hexgrid.AddIndustrialBuilding(
                Instantiate(HexUnit.IndustrialPrefab),
                cell,
                Random.Range(0f, 360f)
                );
        }

        Debug.Log("Industrial build Button");
        //hexMapEditor.CreateIndustrialBulding();
        if (HexUnit.IndustrialPrefab && ScrollView != null)
        {
            bool isActivate = ScrollView.activeSelf;

            ScrollView.SetActive(!isActivate);
        }

    }
    public void ResearchBuild()
    {

        //hexMapEditor.GetCellUnderCursor();
        //hexMapEditor.SetEditMode(true);
        //hexMapEditor.SetApplySpecialIndex(true);
        //hexMapEditor.SetSpecialIndex(1);
        //if (Input.GetMouseButton(0))
        //{
        //    hexFeatureManager.AddResearchBuilding(cell, position);
        //    hexMapEditor.SetApplySpecialIndex(false);
        //}
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

    public void NextButton()
    {
        
    }
    //HexMetrics hexMetrics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
