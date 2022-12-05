using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [HideInInspector] public HexMapCamera cam;
    public HexGrid Grid { get; set; }
    public bool IsLose { get; private set; }

    public bool myTurn;
    public PlayerSit Sit
    {
        get {
            return sit;
        }
        set {
            sit = value;
            display.cityColor.color = display.colors[(int)sit];
        }
    }
    private PlayerSit sit;
    public bool isPlayer;

    public int[] trash = new int[6];

    public List<Unit> units = new List<Unit>();
    public List<Unit> actions = new List<Unit>();

    public List<HexCell> cells = new List<HexCell>();
    public HexCell rootCell;
    
    protected Coroutine _coroutine = null;
    protected WaitForSeconds dot5 = new WaitForSeconds(0.5f);

    public Display display;
    
    public int buyBuildingPrice = 100;

    public int Money {
        get {
            return trash[1];
        }
        set{
            trash[1] = value;
            display.money.text = (trash[1]).ToString();
        }
    }

    public int PA {
        get {
            return pa;
        }
        set {
            pa = value;
            display.pa.text = (PL*100).ToString("0.#")+"%";
        }
    }
    private int pa;

    public float PL {
        get {
            return PA / (float)GameInfo.maxPA;
        }
    }

    public float Plrate = GameInfo.PLPercent;

    public int[] Research = new int[6];

    public void MyTurn()
    {
        myTurn = true;
        actions.AddRange(units);
        CameraPositioning(actions[actions.Count-1].gameObject);
        StartCoroutine(Scheduler());
        PA += (int)(PA * Plrate);
    }

    protected virtual IEnumerator Scheduler()
    { yield return null; }

    protected virtual IEnumerator ActionExplorer(HexUnit action)
    { yield return null; }

    protected virtual IEnumerator ActionResearcher(Unit action)
    { yield return null; }

    protected virtual IEnumerator ActionManufacturer(Unit action)
    { yield return null; }

    public void AddCell(HexCell cell)
    {
        cells.Add(cell);
        cell.sit = Sit;
        cell.Walled = true;
        Grid.IncreaseVisibility(cell, 3);
    }

    public void AddUnit(Unit unit)
    {
        unit.city = this;
        units.Add(unit);
    }

    protected void CameraPositioning(GameObject obj)
    {
        cam.transform.localPosition =  
                cam.WrapPosition(obj.transform.localPosition);
    }

    public int GetTrash(ResourceType type)
    {
        return trash[(int)type]; 
    }
    
    public void UpdateTrash(ResourceType type, int num=0)
    {
        trash[(int)type] += num;
        if (type == ResourceType.Money)
        {
            Money = trash[(int)type];
        }
        if (trash[(int)type] < 0) trash[(int)type] = 0;
        if (trash[(int)type] > int.MaxValue) trash[(int)type] = int.MaxValue;
    }

    public int GetResearch(int type)
    {
        return Research[type];
    }

    public void UpdateResearch(int type)
    {
        Research[type]++;
    }

    public virtual void PostExplorer(Unit action)
    {}

    public void Lose()
    {
        // 유닛제거
        for (int i=0; i<units.Count; i++)
        {
            units[i].gameObject.SetActive(false);
        }
        IsLose = true;
    }
}
