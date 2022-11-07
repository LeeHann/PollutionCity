using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{

    [SerializeField] private Camera mainCam = null;

    //memalloc to save GC collections
    private RaycastHit hit = new RaycastHit();
    HexCell hexcell;


    public int GetHitLayer => hit.collider != null ? hit.collider.gameObject.layer : -1;
    public Vector3 GetHitPosition => hit.collider != null ? hit.point : Vector3.zero;

    void Start()
    {
       // HexFeatureManager.Build();
    }

    void Update()
    {
        //if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition),
        //    out hit,
        //    1000f))
        //{
        //    print ("Hit an object with name : " + hit.collider.gameObject.name);
        //}

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;

        //if(Physics.Raycast(ray, out hitInfo))
        //{
        //    GameObject ourhitObject = hitInfo.collider.transform.gameObject;
        //    Debug.Log("Hit an object with name : " + ourhitObject.name);

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        MeshRenderer mr = ourhitObject.GetComponentInChildren<MeshRenderer>();

        //        if(mr.material.color == Color.red)
        //        {
        //            mr.material.color = Color.white;
        //        }
        //        else
        //        {
        //            mr.material.color = Color.red;
        //        }
        //    }
        //}

        void OnMouserButtonDown()
        {

        }
    }
}
