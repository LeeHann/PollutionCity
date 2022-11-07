using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LandBuy : MonoBehaviour
{


    public HexCell hexcell;

    HexDirection hexDirection;

    // Start is called before the first frame update
    public void BuyLand()
    {
        
        Debug.Log("BuyTile Button");
        //hexcell.BuyTile(hexDirection , hexcell);
        //if (hexcell.SetNeighbor(hexDirection, hexcell) && !hexcell.Walled)
        //{
        //    hexcell.EnableHighlight(Color.red);
        //}

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    GameObject ourhitObject = hitInfo.collider.transform.gameObject;
        //    Debug.Log("Hit an object with name : " + ourhitObject.name);

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        MeshRenderer mr = ourhitObject.GetComponentInChildren<MeshRenderer>();

        //        if (mr.material.color == Color.red)
        //        {
        //            mr.material.color = Color.white;
        //        }
        //        else
        //        {
        //            mr.material.color = Color.red;
        //        }
        //    }
        //}
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }
}
