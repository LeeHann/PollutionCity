using UnityEngine;
using System.Collections.Generic;

public enum UnitType {
	Explorer,
	Researcher,
	Manufacturer
}
public class Unit : MonoBehaviour
{
	public City city;
	public HexGrid Grid;

	public HexCell Location
	{
		get
		{
			return location;
		}
		set
		{
			if (location)
			{
				Grid.DecreaseVisibility(location, 4);
				location.Unit = null;
			}
			location = value;
			value.Unit = this;
			Grid.IncreaseVisibility(value, 4);
			transform.localPosition = value.Position;
			Grid.MakeChildOfColumn(transform, value.ColumnIndex);
		}
	}
	protected HexCell location;

	public float Orientation
	{
		get
		{
			return orientation;
		}
		set
		{
			orientation = value;
			transform.localRotation = Quaternion.Euler(0f, value, 0f);
		}
	}
	protected float orientation;

    public UnitType unitType;

	protected Coroutine hexunitCoroutine = null;

	public bool TurnUnit {
		get {
			return turnUnit;
		}
		set {
			if (value) 
			{ 
				count = 1;
				if (unitType == UnitType.Manufacturer)
				{
					city.Money += 100;
				}
			}
			else 
			{ 
				count = 0;
				if (unitType == UnitType.Explorer && hexunitCoroutine != null)
				{
					StopAllCoroutines();
					Grid.IncreaseVisibility(location, 3);
					transform.localPosition = location.Position;
					orientation = transform.localRotation.eulerAngles.y;

					TryGetComponent<HexUnit>(out HexUnit unit);
					Debug.Log(unit.pathToTravel);
					ListPool<HexCell>.Add(unit.pathToTravel);
					unit.pathToTravel = null;

					gameObject.transform.GetChild(0).TryGetComponent<Animator>(out Animator anim);
					anim.SetInteger("AnimationPar", 0);
					
					Grid.ClearPath();
					Grid.isMoving = false;
					city.PostExplorer(this);
					hexunitCoroutine = null;
				}
				if (city.actions.Contains(this))
				{
					city.actions.Remove(this);
					if (city.isPlayer)
					{
						city.unitText.text = city.actions.Count + " / " + city.units.Count;
					}
				}
			}
			turnUnit = value;
		}
	}
	private bool turnUnit = false;
	public int count; // count 체제로 count =0이 되면 큐에서 빼기
}
