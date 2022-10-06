using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Raycaster raycaster = null;
    [SerializeField] private BuildPlacer placer = null;

    [SerializeField] private KeyCode buildKey = KeyCode.Mouse0;

    [SerializeField] private int buildLayer = 0;
    bool canBuild => raycaster != null
        && raycaster.GetHitLayer == buildLayer
        && placer.CanBuild;
        

    public GameObject Building1;
    public GameObject Building2;
    void Update()
    {
        if (Input.GetKeyDown(buildKey) && raycaster && canBuild && Input.GetKey(KeyCode.B))
        {
            GameObject o = GameObject.Instantiate(Building1);
            o.transform.position = raycaster.GetHitPosition;
        }
        else if (Input.GetKeyDown(buildKey) && raycaster && canBuild && Input.GetKey(KeyCode.V))
        {
            GameObject o = GameObject.Instantiate(Building2);
            o.transform.position = raycaster.GetHitPosition;
        }


    }
}
