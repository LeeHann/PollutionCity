using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [HideInInspector] public HexMapCamera cam;

    public bool myTurn;

    public PlayerSit sit;
    public Trash trash = new Trash();
    public List<Unit> units = new List<Unit>();
    public List<HexCell> cells = new List<HexCell>();
    public HexCell rootCell;

    public List<Unit> actions = new List<Unit>();
    protected Coroutine _coroutine = null;
    WaitForSeconds dot5 = new WaitForSeconds(0.5f);

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
            CameraPositioning(action.gameObject);

            switch (action.unitType)
            {
                case UnitType.Explorer: // 탐사 유닛 행동을 결정하기
                    _coroutine = StartCoroutine(ActionExplorer(action));
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
            actions.RemoveAt(actions.Count-1);
            yield return dot5;
		}
		myTurn = false;	
    }

    protected virtual IEnumerator ActionExplorer(Unit action)
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

    protected void CameraPositioning(GameObject obj)
    {
        cam.transform.localPosition = obj.transform.localPosition;
    }
}
