using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.EventSystems;

public class HexUnit : Unit, IPointerClickHandler
{
	[SerializeField] Animator anim;

	const float rotationSpeed = 180f;
	const float travelSpeed = 1.5f;

	HexCell currentTravelLocation;

	public int Speed
	{
		get
		{
			return 24;
		}
	}

	public int VisionRange
	{
		get
		{
			return 3;
		}
	}

	public List<HexCell> pathToTravel;
	List<HexCell> highlights = new List<HexCell>();

	public void OnPointerClick(PointerEventData e)
	{
		if (Grid.isMoving) return;
		if (count <= 0 || !city.isPlayer) return;
		if (MapSetter.occupiedCellList.Count > 0)
		{
			MapSetter.occupiedCellList.ForEach((cell)=> {
				cell.DisableHighlight();
				cell.highlightBtn.onClick.RemoveAllListeners();
			});
			MapSetter.occupiedCellList.Clear();
			return;
		}
		Queue queue = new Queue();
		queue.Enqueue(Location);

		while (queue.Count > 0)
		{
			HexCell cell = (HexCell)queue.Dequeue();
			cell.EnableHighlight(Color.yellow);
			highlights.Add(cell);
			MapSetter.occupiedCellList.Add(cell);

			cell.highlightBtn.onClick.RemoveAllListeners();
			cell.highlightBtn.onClick.AddListener(() => OnClickHighlight(cell));

			for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
			{
				HexCell neighbor = cell.GetNeighbor(d);
				if (neighbor == null) 
					continue;
				if (neighbor.IsEnabledHighlight() || !Grid.Search(Location, neighbor, this))
					continue;

				queue.Enqueue(neighbor);
			}
		}
		Location.DisableHighlight();
		queue.Clear();
	}

	public void OnClickHighlight(HexCell cell)
	{
		for (int i = highlights.Count - 1; i >= 0; i--)
		{
			highlights[i].DisableHighlight();
			highlights[i].highlightBtn.onClick.RemoveAllListeners();
			highlights.RemoveAt(i);
			MapSetter.occupiedCellList.Remove(cell);
		}
		MapSetter.occupiedCellList.Clear();
		Grid.FindPath(Location, cell, this);
		Travel(Grid.GetPath());
	}

	public bool GoToTravel(HexCell cell)
	{
		Grid.FindPath(Location, cell, this);
		if (Grid.GetPath() != null && Grid.GetPath().Count > 1)
		{
			Travel(Grid.GetPath());
			MapSetter.occupiedCellList.ForEach((cell)=> {
				cell.DisableHighlight();
				cell.highlightBtn.onClick.RemoveAllListeners();
			});
			MapSetter.occupiedCellList.Clear();
			return true;
		}
		return false;
	}

	public bool IsValidDestination(HexCell cell)
	{
		return cell.IsExplored && !cell.IsUnderwater && !cell.Unit;
	}

	public void Travel(List<HexCell> path)
	{
		location.Unit = null;
		location = path[path.Count - 1];
		location.Unit = this;
		pathToTravel = path;
		StopAllCoroutines();
		hexunitCoroutine = StartCoroutine(TravelPath());

		anim.SetInteger("AnimationPar", 1);
	}

	IEnumerator TravelPath()
	{
		Grid.isMoving = true;
		Vector3 a, b, c = pathToTravel[0].Position;
		yield return LookAt(pathToTravel[1].Position);

		if (!currentTravelLocation)
		{
			currentTravelLocation = pathToTravel[0];
		}
		Grid.DecreaseVisibility(currentTravelLocation, VisionRange);
		int currentColumn = currentTravelLocation.ColumnIndex;

		float t = Time.deltaTime * travelSpeed;
		for (int i = 1; i < pathToTravel.Count; i++)
		{
			currentTravelLocation = pathToTravel[i];
			a = c;
			b = pathToTravel[i - 1].Position;

			int nextColumn = currentTravelLocation.ColumnIndex;
			if (currentColumn != nextColumn)
			{
				if (nextColumn < currentColumn - 1)
				{
					a.x -= HexMetrics.innerDiameter * HexMetrics.wrapSize;
					b.x -= HexMetrics.innerDiameter * HexMetrics.wrapSize;
				}
				else if (nextColumn > currentColumn + 1)
				{
					a.x += HexMetrics.innerDiameter * HexMetrics.wrapSize;
					b.x += HexMetrics.innerDiameter * HexMetrics.wrapSize;
				}
				Grid.MakeChildOfColumn(transform, nextColumn);
				currentColumn = nextColumn;
			}

			c = (b + currentTravelLocation.Position) * 0.5f;
			Grid.IncreaseVisibility(pathToTravel[i], VisionRange);

			for (; t < 1f; t += Time.deltaTime * travelSpeed)
			{
				transform.localPosition = Bezier.GetPoint(a, b, c, t);
				Vector3 d = Bezier.GetDerivative(a, b, c, t);
				d.y = 0f;
				transform.localRotation = Quaternion.LookRotation(d);
				yield return null;
			}
			Grid.DecreaseVisibility(pathToTravel[i], VisionRange);
			t -= 1f;
		}
		currentTravelLocation = null;

		a = c;
		b = location.Position;
		c = b;
		Grid.IncreaseVisibility(location, VisionRange);
		for (; t < 1f; t += Time.deltaTime * travelSpeed)
		{
			transform.localPosition = Bezier.GetPoint(a, b, c, t);
			Vector3 d = Bezier.GetDerivative(a, b, c, t);
			d.y = 0f;
			transform.localRotation = Quaternion.LookRotation(d);
			yield return null;
		}

		transform.localPosition = location.Position;
		orientation = transform.localRotation.eulerAngles.y;
		ListPool<HexCell>.Add(pathToTravel);
		pathToTravel = null;
		anim.SetInteger("AnimationPar", 0);

		Grid.ClearPath();
		Grid.isMoving = false;
		city.PostExplorer(this);
		hexunitCoroutine = null;
		TurnUnit = false;
	}

	IEnumerator LookAt(Vector3 point)
	{
		if (HexMetrics.Wrapping)
		{
			float xDistance = point.x - transform.localPosition.x;
			if (xDistance < -HexMetrics.innerRadius * HexMetrics.wrapSize)
			{
				point.x += HexMetrics.innerDiameter * HexMetrics.wrapSize;
			}
			else if (xDistance > HexMetrics.innerRadius * HexMetrics.wrapSize)
			{
				point.x -= HexMetrics.innerDiameter * HexMetrics.wrapSize;
			}
		}

		point.y = transform.localPosition.y;
		Quaternion fromRotation = transform.localRotation;
		Quaternion toRotation =
			Quaternion.LookRotation(point - transform.localPosition);
		float angle = Quaternion.Angle(fromRotation, toRotation);

		if (angle > 0f)
		{
			float speed = rotationSpeed / angle;
			for (
				float t = Time.deltaTime * speed;
				t < 1f;
				t += Time.deltaTime * speed
			)
			{
				transform.localRotation =
					Quaternion.Slerp(fromRotation, toRotation, t);
				yield return null;
			}
		}

		transform.LookAt(point);
		orientation = transform.localRotation.eulerAngles.y;
	}

	public int GetMoveCost(
		HexCell fromCell, HexCell toCell, HexDirection direction)
	{
		if (!IsValidDestination(toCell))
		{
			return -1;
		}
		HexEdgeType edgeType = fromCell.GetEdgeType(toCell);
		int moveCost;
		moveCost = edgeType == HexEdgeType.Flat ? 5 : 10;
		return moveCost;
	}

	public void Die()
	{
		if (location)
		{
			Grid.DecreaseVisibility(location, VisionRange);
		}
		location.Unit = null;
		Destroy(gameObject);
	}

	public void Save(BinaryWriter writer)
	{
		location.coordinates.Save(writer);
		writer.Write(orientation);
	}

	// public static void Load(BinaryReader reader, HexGrid grid)
	// {
	// 	HexCoordinates coordinates = HexCoordinates.Load(reader);
	// 	float orientation = reader.ReadSingle();
	// 	// grid.AddUnit(
	// 	// 	Instantiate(unitPrefab), grid.GetCell(coordinates), orientation
	// 	// );
	// 	grid.AddLivingBuilding(
	// 		Instantiate(LivingPrefab), grid.GetCell(coordinates), orientation
	// 	);
	// 	grid.AddResearchBuilding(
	// 		Instantiate(ResearchPrefab), grid.GetCell(coordinates), orientation
	// 	);
	// 	grid.AddIndustrialBuilding(
	// 		Instantiate(IndustrialPrefab), grid.GetCell(coordinates), orientation
	// 	);
	// }

	void OnEnable()
	{
		if (location)
		{
			transform.localPosition = location.Position;
			if (currentTravelLocation)
			{
				Grid.IncreaseVisibility(location, VisionRange);
				Grid.DecreaseVisibility(currentTravelLocation, VisionRange);
				currentTravelLocation = null;
			}
		}
	}
}