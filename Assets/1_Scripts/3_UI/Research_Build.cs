using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Research_Build : MonoBehaviour
{




    HexGrid grid;
    public GameObject prefab;
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




    public void ResearchBuild()
    {
        hexMapEditor.GetCellUnderCursor();
        if (Input.GetMouseButton(0))
        {
            hexFeatureManager.AddResearchBuilding(cell, position);
            hexMapEditor.SetApplySpecialIndex(false);
        }
    }



    void Start()
    {
        hexMapEditor.SetEditMode(true);
        hexMapEditor.SetApplySpecialIndex(true);
        hexMapEditor.SetSpecialIndex(1);

        //prefab = GameObject.Instantiate(prefab);
        //container = prefab.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    hexFeatureManager.AddResearchBuilding(cell, position);
        //    hexMapEditor.SetApplySpecialIndex(false);
        //}
    }
}
