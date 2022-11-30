using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [HideInInspector] public HexMapCamera cam;
    public HexGrid Grid { get; set; }
    public bool IsLose { get; private set; }

    public bool myTurn;
    public PlayerSit sit;
    private int[] trash = new int[6];

    public List<Unit> units = new List<Unit>();
    public List<Unit> actions = new List<Unit>();

    public List<HexCell> cells = new List<HexCell>();
    public HexCell rootCell;
    
    protected Coroutine _coroutine = null;
    protected WaitForSeconds dot5 = new WaitForSeconds(0.5f);
    Skill research;

    public int Money {
        get {
            return trash[1] * 100;
        }
        set{
            trash[1] = value / 100;
        }
    }

    public int PA {
        get {
            return pa;
        }
        set {
            pa = value;
        }
    }
    private int pa;

    public float PL {
        get {
            return PA / (float)GameInfo.maxPA;
        }
    }

    public int VisionRange {
		get {
			return 3;
		}
	}

    public int[] Research = new int[6];

    public void MyTurn()
    {
        myTurn = true;
        actions.AddRange(units);
        CameraPositioning(actions[actions.Count-1].gameObject);
        StartCoroutine(Scheduler());
    }

    protected virtual IEnumerator Scheduler()
    {
        _coroutine = StartCoroutine(BuyLand());
        yield return new WaitUntil(() => _coroutine == null);
        while (actions.Count > 0)
		{
            Unit action = actions[actions.Count-1];
            action.TurnUnit = true;

            switch (action.unitType)
            {
                case UnitType.Explorer: // 탐사 유닛 행동을 결정하기
                    _coroutine = StartCoroutine(ActionExplorer((HexUnit)action));
                    yield return new WaitUntil(() => _coroutine == null);
                    break;

	            case UnitType.Researcher: // 연구 유닛 행동을 결정하기
                    _coroutine = StartCoroutine(ActionResearcher(action));
                    yield return new WaitUntil(() => _coroutine == null);
                    break;

	            case UnitType.Manufacturer: // 제조 유닛 행동을 결정하기
                    _coroutine = StartCoroutine(ActionManufacturer(action));
                    yield return new WaitUntil(() => _coroutine == null);
                    break;
            }
            actions.Remove(action);
            yield return dot5;
		}
		myTurn = false;	
    }

    protected virtual IEnumerator BuyLand()
    { yield return null;
        _coroutine = null; }

    protected virtual IEnumerator ActionExplorer(HexUnit action)
    { yield return null; }

    protected virtual IEnumerator ActionResearcher(Unit action)
    { yield return null; }

    protected virtual IEnumerator ActionManufacturer(Unit action)
    { yield return null; }

    public void AddCell(HexCell cell)
    {
        cells.Add(cell);
        cell.sit = sit;
        cell.Walled = true;
        Grid.IncreaseVisibility(cell, VisionRange);
    }

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }

    public void AddLandMark(HexCell cell)
    {
        if(cell.walled != true)
            cell.walled = true;
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

    public void Lose()
    {
        // 유닛, 건물 제거
        
        IsLose = true;
    }
}
