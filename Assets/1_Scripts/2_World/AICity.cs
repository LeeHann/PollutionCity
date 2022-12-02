using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICity : City
{
	protected override void BuyLandAndBuilding()
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
					switch (Random.Range(0, 3))
					{
						case 0:
							Grid.buildSet.InstanceLiving(cells[cell]);
							break;
						case 1:
							Grid.buildSet.InstanceIndustrial(cells[cell]);
							break;
						case 2:
							Grid.buildSet.InstanceResearch(cells[cell]);
							break;
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
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			if (action.Location.Resource <= ResourceType.Money || GetResearch((int)action.Location.Resource) > 0)
			{
				UpdateTrash(action.Location.Resource, Random.Range(5, 20));
				action.Location.Resource = ResourceType.None;
			}
		}
		_coroutine = null;
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
