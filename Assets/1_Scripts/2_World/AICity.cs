using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICity : City
{
	protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
		// choose position to move
		action.GoToTravel(SelectCell(action));
		yield return new WaitUntil(() => action.TurnUnit == false);
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			if (GetResearch((int)action.Location.Resource) > 0)
			{
				UpdateTrash(action.Location.Resource, 1);
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
			cell = (HexCell)queue.Dequeue();
			for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
			{
				HexCell neighbor = cell.GetNeighbor(d);
				if (neighbor == null)
					continue;
				if (explorer.Location.coordinates.DistanceTo(neighbor.coordinates) > 4)
					continue;
				if (!neighbor.IsUnderwater && neighbor.Resource != ResourceType.None)
					return neighbor;
				
				queue.Enqueue(neighbor);
			}
		}
		if (cell == explorer.Location) cell = explorer.Location.GetNeighbor(0);
		Debug.Log(cell.coordinates.X + " : " + cell.coordinates.Z);
		return cell;
	}

    protected override IEnumerator ActionResearcher(Unit action) // 연구 행동 결정 함수
    {
		yield return null;
		_coroutine = null;
	}

    protected override IEnumerator ActionManufacturer(Unit action) // 제조 행동 결정 함수
    {
		yield return null;
		_coroutine = null;
	}
}
