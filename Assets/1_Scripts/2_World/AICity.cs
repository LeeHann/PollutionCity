using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICity : City
{
	List<HexUnit> hexUnits = new List<HexUnit>();
	List<RSUnit> rSUnits = new List<RSUnit>();
	List<MFCUnit> mFCUnits = new List<MFCUnit>();

	protected override IEnumerator Scheduler()
    {
        BuyLandAndBuilding();
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
	protected void BuyLandAndBuilding()
    {
		// 빈 땅이 없다면 구매
		int empty = 0;
		for (int i=0; i<cells.Count; i++)
		{
			if (cells[i].Unit == null)
				empty++;
		}
		if (empty == 0)
		{
			// 구매
        	for(int cell = 0; cell <= cells.Count-1; cell++)
        	{
        	    for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        	    {
        	        HexCell neighbor = cells[cell].GetNeighbor(d);
        	        if (neighbor == null) continue;

					int price = rootCell.coordinates.DistanceTo(neighbor.coordinates) * 100;
        	        if (neighbor.walled == false && Money > price)                //가장자리 cells
        	        {
						Money -= price;
						AddCell(neighbor);
						break;
        	        }
        	    }
        	}
		}
		else
		{
			// 건설
			for (int cell = 0; cell <= cells.Count-1; cell++)
			{
				if (cells[cell].Unit == null && Money >= buyBuildingPrice)
				{
					if (rSUnits.Count > hexUnits.Count * 2) {
						hexUnits.Add(Grid.buildSet.InstanceLiving(cells[cell]));
					} else if (hexUnits.Count > mFCUnits.Count + 1) {
						mFCUnits.Add(Grid.buildSet.InstanceIndustrial(cells[cell]));
					} else {
						rSUnits.Add(Grid.buildSet.InstanceResearch(cells[cell]));
					}
					Money -= buyBuildingPrice;
					buyBuildingPrice += 200;
					break;
				}
			}
		}
	}

	protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
		// choose position to move
		if (!action.GoToTravel(SelectCell(action)))
		{
			for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
			{
				var neighbor = action.Location.GetNeighbor(d);
				if (neighbor != null && neighbor.Unit == null && !neighbor.IsUnderwater)
				{
					action.GoToTravel(neighbor);
					break;
				}
			}
		}
		yield return new WaitUntil(() => action.TurnUnit == false);
		_coroutine = null;
	}

	public override void PostExplorer(Unit action)
	{
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			if (action.Location.Resource <= ResourceType.Money || GetResearch((int)action.Location.Resource) > 0)
			{
				int ran = Random.Range(5, 20);
				UpdateTrash(action.Location.Resource, ran);
				PA -= ran * 10;
				action.Location.Resource = ResourceType.None;
			}
		}
	}

	HexCell SelectCell(HexUnit explorer)
	{
		Queue queue = new Queue();
		HexCell cell = explorer.Location.GetNeighbor(0);
		queue.Enqueue(explorer.Location);
		while (queue.Count > 0)
		{
			if (cell)
			cell = (HexCell)queue.Dequeue();
			for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
			{
				HexCell neighbor = cell.GetNeighbor(d);
				if (neighbor == null)
					continue;
				if (queue.Contains(neighbor))
					continue;
				if (neighbor.coordinates.DistanceTo(cell.coordinates) > 4)
					continue;
				if (!neighbor.IsUnderwater && neighbor.Resource != ResourceType.None)
					return neighbor;
				
				queue.Enqueue(neighbor);
			}
		}
		return cell;
	}

    protected override IEnumerator ActionResearcher(Unit action) // 연구 행동 결정 함수
    {
		UpdateResearch(Random.Range(0, Research.Length));
		action.count--;
		yield return null;
		_coroutine = null;
	}

    protected override IEnumerator ActionManufacturer(Unit action) // 제조 행동 결정 함수
    {
		for (int i=MFCSystem.Instance.mFCItems.Length-1; i>=0; i--)
		{
			var item = MFCSystem.Instance.mFCItems[i];
			if (GetTrash(item.input) >= item.inputCnt)
			{
				UpdateTrash(item.input, -item.inputCnt);
        		Money += item.getMoney;
				break;
			}
		}
		action.count--;
		yield return null;
		_coroutine = null;
	}
}
