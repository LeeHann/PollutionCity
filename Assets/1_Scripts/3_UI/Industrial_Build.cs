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

    void Update()
    {

    }
}
