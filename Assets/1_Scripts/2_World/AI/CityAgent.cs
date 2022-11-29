using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CityAgent : Agent
{
	[SerializeField] HexGrid hexGrid;
	[SerializeField] MapSetter mapSetter;
	[SerializeField] HexUnit unit;
	float time;
	HexCell target;

    private void Start() 
	{
		hexGrid = HexGrid.hexGrid;
		unit.Grid = hexGrid;
		do {
			unit.Location = hexGrid.emptyCells[Random.Range(0, hexGrid.emptyCells.Count)];	
		} while (unit.Location.IsUnderwater);
		unit.Orientation = Random.Range(0f, 360f);
	}

	public override void OnEpisodeBegin()
	{
		time = 0f;
		Debug.Log("begin");
		if (hexGrid.emptyCells.Count > 1000) mapSetter.ScatterResources(1);
	}

	private void Update() {
		time += Time.deltaTime;
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(unit.Location.coordinates.X);
		sensor.AddObservation(unit.Location.coordinates.Z);

		for (HexDirection dir = HexDirection.NE; dir <= HexDirection.NW; dir++)
		{
			sensor.AddObservation(unit.Location.GetNeighbor(dir).Resource != ResourceType.None);
		}
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		HexCell cell = hexGrid.GetCell(new HexCoordinates(
			actions.DiscreteActions[0], actions.DiscreteActions[1])
		);

		if (cell != null && cell != unit.Location &&
			unit.Location.coordinates.DistanceTo(cell.coordinates) < 5)
		{
			unit.GoToTravel(cell);
			if (hexGrid.GetPath() != null)
			{
				if (cell.Resource != ResourceType.None)
				{
					cell.Resource = ResourceType.None;
					SetReward(1.0f);
					EndEpisode();
				}
			}
		}

		if (time > 3f)
		{
			Debug.Log("timeout!");
			EndEpisode();
		}
	}

	// public override void Heuristic(in ActionBuffers actionsOut)
	// {
	// 	var discreteActionsOut = actionsOut.DiscreteActions;
	// 	if (Input.GetMouseButton(0)) {
	// 		HexCell cell = hexGrid.GetCell(
	// 			Camera.main.ScreenPointToRay(Input.mousePosition)
	// 		);
	// 		discreteActionsOut[0] = cell.coordinates.X;
	// 		discreteActionsOut[1] = cell.coordinates.Z;
	// 	} else {
	// 		discreteActionsOut[0] = unit.Location.coordinates.X;
	// 		discreteActionsOut[1] = unit.Location.coordinates.Z;
	// 	}
	// }
}
