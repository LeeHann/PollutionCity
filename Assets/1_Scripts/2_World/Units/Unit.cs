using UnityEngine;

public enum UnitType {
	Explorer,
	Researcher,
	Manufacturer
}
public class Unit : MonoBehaviour
{
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

	public bool TurnUnit {
		get {
			return turnUnit;
		}
		set {
			turnUnit = value;
		}
	}
	private bool turnUnit = false;
}
