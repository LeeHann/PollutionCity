using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [HideInInspector] public HexMapCamera cam;

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
            return money;
        }
        set{
            money = value;
        }
    }
    private int money;

    public float PA {
        get {
            return pa;
        }
        set {
            pa = value;
        }
    }
    private float pa;

    public void MyTurn()
    {
        myTurn = true;
        actions.AddRange(units);
        CameraPositioning(actions[actions.Count-1].gameObject);
        StartCoroutine(Scheduler());
    }

    protected virtual IEnumerator Scheduler()
    {
        while (actions.Count > 0)
		{
            Unit action = actions[actions.Count-1];
            action.TurnUnit = true;
            CameraPositioning(action.gameObject);

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
            action.TurnUnit = false;
            actions.RemoveAt(actions.Count-1);
            yield return dot5;
		}
		myTurn = false;	
    }

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
        if (trash[(int)type] == 2 && research.UpgradePaperBtn.interactable != false)
        {
            Debug.Log("종이못줍");
        }
        else if (trash[(int)type] == 3 && research.UpgradeCanBtn.interactable != false)
        {
            Debug.Log("캔못줍");
        }
        else if (trash[(int)type] == 4 && research.UpgradeGlassBtn.interactable != false)
        {
            Debug.Log("유리못줍");
        }
        else if (trash[(int)type] == 5 && research.UpgradePlasticBtn.interactable != false)
        {
            Debug.Log("플라스틱못줍");
        }
        return trash[(int)type]; 
    }
    
    public void UpdateTrash(ResourceType type, int num=0)
    {
        trash[(int)type] += num;
        



        if (trash[(int)type] < 0) trash[(int)type] = 0;
        if (trash[(int)type] > int.MaxValue) trash[(int)type] = int.MaxValue;
    }
}
