using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{

    [SerializeField] private Camera mainCam = null;

    //memalloc to save GC collections
    private RaycastHit hit = new RaycastHit();


    public int GetHitLayer => hit.collider != null ? hit.collider.gameObject.layer : -1;
    public Vector3 GetHitPosition => hit.collider != null ? hit.point : Vector3.zero;

    void Start()
    {
       // HexFeatureManager.Build();
    }

    void Update()
    {
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition),
            out hit,
            1000f))
        {
            print ("Hit an object with name : " + hit.collider.gameObject.name);
        }
    }
}
