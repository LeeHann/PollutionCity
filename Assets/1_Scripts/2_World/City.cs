using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public PlayerSit sit;
    public Trash trash = new Trash();
    public List<HexUnit> units = new List<HexUnit>();
    public List<HexCell> cells = new List<HexCell>();
    public HexCell rootCell;

    public int Money {
        get {
            return money;
        }
        set{
            money = value;
        }
    }
    private int money;

    public int PA {
        get {
            return pa;
        }
        set {
            pa = value;
        }
    }
    private int pa;

    public void AddCell(HexCell cell)
    {
        cells.Add(cell);

        cell.chunk.features.walls.meshRenderer.material
                = cell.materials[(int)sit];
        cell.Walled = true;
    }
}
