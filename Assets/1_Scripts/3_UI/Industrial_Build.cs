using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Industrial_Build : MonoBehaviour
{




    public HexGrid hexgrid;
    public GameObject prefab;
    public GameObject ScrollView; // 제조 스크롤뷰 UI 연동
    Transform container;

    HexMapEditor hexMapEditor;
    HexFeatureManager hexFeatureManager;
    HexGridChunk hexGridChunk;
    HexCell cell;
    Vector3 position;
    //HexMetrics hexMetrics;

    


    public void Clear()
    {
        if (container)
        {
            Destroy(container.gameObject);
        }
        container = new GameObject("Features Container").transform;
        container.SetParent(transform, false);
    }

    //public void AddSpecialFeature(HexCell cell, Vector3 position)
    //{
    //    HexHash hash = HexMetrics.SampleHashGrid(position);
    //    Transform instance = Instantiate(prefab);
    //    instance.localPosition = HexMetrics.Perturb(position);
    //    instance.localRotation = Quaternion.Euler(0f, 360f * hash.e, 0f);
    //    instance.SetParent(container, false);
    //}

    //public void Build_Living()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        HexHash hash = HexMetrics.SampleHashGrid(Vector3 position);
    //        // GameObject livebuilding = Instantiate(prefab);
    //        Transform instance = HexMetrics.Perturb(Vector3 position);
    //        instance.localRotation = Quaternion.Euler(0f, 360f * hash.e, 0f);
    //        instance.SetParent(container, false);
    //        //livebuilding.transform.position = position;

    //        //livebuilding.SetActive(true);
    //    }
    //}
    // Start is called before the first frame update


    public HexCell GetCellUnderCursor()
    {
        return
            hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
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
        if( HexUnit.IndustrialPrefab && ScrollView != null)
        {
            bool isActivate = ScrollView.activeSelf;

            ScrollView.SetActive(!isActivate);
        }
        
    }
    void Awake()
    {

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    IndustrialBuild();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    IndustrialBuild();
        //}
    }
}
