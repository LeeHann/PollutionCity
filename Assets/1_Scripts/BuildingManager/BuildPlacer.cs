using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacer : MonoBehaviour
{
    [SerializeField] Raycaster rc = null;


    [SerializeField] int buildableLayer = 0; // 6번을 건설전용 레이어로 설정하려고했는데
                                             // HexGrid 자식들이 기본으로 0으로 설정돼서 어쩔 수 없이 일단 0으로 셋팅

    [SerializeField] bool canBuild = false;
    public bool CanBuild => canBuild;

    private void Update()
    {
        if(!rc)
            return;

        if (rc.GetHitLayer != buildableLayer)
            return;

        transform.position = rc.GetHitPosition;
    }
}
