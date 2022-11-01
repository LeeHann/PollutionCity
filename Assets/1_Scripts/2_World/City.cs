using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public bool myTurn;

    public PlayerSit sit;
    public Trash trash = new Trash();
    public List<Unit> units = new List<Unit>();
    public List<HexCell> cells = new List<HexCell>();
    public HexCell rootCell;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myTurn = false;
        }
    }

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
        cell.sit = sit;
        cell.Walled = true;
    }

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }
}
