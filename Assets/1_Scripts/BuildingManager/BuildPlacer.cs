using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacer : MonoBehaviour
{
    [SerializeField] Raycaster rc = null;


    [SerializeField] int buildableLayer = 0; // 6���� �Ǽ����� ���̾�� �����Ϸ����ߴµ�
                                             // HexGrid �ڽĵ��� �⺻���� 0���� �����ż� ��¿ �� ���� �ϴ� 0���� ����

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
