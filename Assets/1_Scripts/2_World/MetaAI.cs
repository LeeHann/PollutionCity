using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAI : MonoBehaviour
{
    public HexGrid hexGrid;
    public HexMapCamera cam;
    HexCell cell;

    public City[] Cities;

    private void Start() 
    {
        GenerateCities();
    }

    public void OnclickBuildCity()
    {
        GenerateCities();
    }

    public void GenerateCities()
    {
        bool invalid;
        do {
            int randomX = UnityEngine.Random.Range(0, hexGrid.cellCountX);
            int randomZ = UnityEngine.Random.Range(0, hexGrid.cellCountZ);
            cell = hexGrid.GetCell(randomX, randomZ);

            invalid = false;
            invalid |= cell.IsUnderwater;
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                if (invalid) break;
                HexCell neighbor = cell.GetNeighbor(d);
                if (neighbor == null) 
                {
                    invalid = true;
                    break;
                }
                invalid |= neighbor.IsUnderwater;
            }
        } while (invalid);
        
        cell.Walled = true;
        for (HexDirection d=HexDirection.NE; d<=HexDirection.NW; d++)
        {
            HexCell neighbor = cell.GetNeighbor(d);
            neighbor.Walled = true;
        }
        cam.transform.localPosition = 
            hexGrid.wrapping ? 
            cam.WrapPosition(cell.transform.localPosition) : 
            cam.ClampPosition(cell.transform.localPosition);
    }

    public void OnclickSetCities()
    {
        int citiesNum = 3;
        
        for (int i=0; i<citiesNum; i++)
        {
            GenerateCities();
        }
    }
}
