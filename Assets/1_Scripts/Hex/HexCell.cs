using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;
	public RectTransform uiRect;
	[HideInInspector] public Image highlight;
	[HideInInspector] public Button highlightBtn;

#region City Boundary
	
	public HexGrid hexGrid {
		get {
			return _hexGrid;
		}
		set {
			_hexGrid = value;
			if (!IsUnderwater && !hexGrid.emptyCells.Contains(this))
				hexGrid.emptyCells.Add(this);
		}
	}
	private HexGrid _hexGrid;
	public HexGridChunk chunk;
	public GameObject[] walls;
	public PlayerSit sit = (PlayerSit)(-1);
	public Material[] materials;

#endregion

	[SerializeField] GameObject[] resources;

	public int Index { get; set; }
	public int ColumnIndex { get; set; }

	public int Elevation {
		get {
			return elevation;
		}
		set {
			if (elevation == value) {
				return;
			}
			int originalViewElevation = ViewElevation;
			elevation = value;
			if (ViewElevation != originalViewElevation) {
				ShaderData.ViewElevationChanged();
			}
			RefreshPosition();
			ValidateRivers();

			for (int i = 0; i < roads.Length; i++) {
				if (roads[i] && GetElevationDifference((HexDirection)i) > 1) {
					SetRoad(i, false);
				}
			}

			Refresh();
		}
	}

	public int WaterLevel {
		get {
			return waterLevel;
		}
		set {
			if (waterLevel == value) {
				return;
			}
			int originalViewElevation = ViewElevation;
			waterLevel = value;
			if (ViewElevation != originalViewElevation) {
				ShaderData.ViewElevationChanged();
			}
			ValidateRivers();
			Refresh();
		}
	}

	public int ViewElevation {
		get {
			return 6;
		}
	}

	public bool IsUnderwater {
		get {
			return waterLevel > elevation;
		}
	}

	public bool HasIncomingRiver {
		get {
			return hasIncomingRiver;
		}
	}

	public bool HasOutgoingRiver {
		get {
			return hasOutgoingRiver;
		}
	}

	public bool HasRiver {
		get {
			return hasIncomingRiver || hasOutgoingRiver;
		}
	}

	public bool HasRiverBeginOrEnd {
		get {
			return hasIncomingRiver != hasOutgoingRiver;
		}
	}

	public HexDirection RiverBeginOrEndDirection {
		get {
			return hasIncomingRiver ? incomingRiver : outgoingRiver;
		}
	}

	public bool HasRoads {
		get {
			for (int i = 0; i < roads.Length; i++) {
				if (roads[i]) {
					return true;
				}
			}
			return false;
		}
	}

	public HexDirection IncomingRiver {
		get {
			return incomingRiver;
		}
	}

	public HexDirection OutgoingRiver {
		get {
			return outgoingRiver;
		}
	}

	public Vector3 Position {
		get {
			return transform.localPosition;
		}
	}

	public float StreamBedY {
		get {
			return
				(elevation + HexMetrics.streamBedElevationOffset) *
				HexMetrics.elevationStep;
		}
	}

	public float RiverSurfaceY {
		get {
			return
				(elevation + HexMetrics.waterElevationOffset) *
				HexMetrics.elevationStep;
		}
	}

	public float WaterSurfaceY {
		get {
			return
				(waterLevel + HexMetrics.waterElevationOffset) *
				HexMetrics.elevationStep;
		}
	}

	public int SpecialIndex
	{
		get
		{
			return specialIndex;
		}
		set
		{
			if (specialIndex != value && !HasRiver)
			{
				specialIndex = value;
				RefreshSelfOnly();
			}
		}
	}
	public bool IsSpecial
	{
		get
		{
			return specialIndex > 0;
		}
	}

	public ResourceType Resource {
		get {
			return resource;
		}
		set {
			if (resource != ResourceType.None)
				resources[(int)resource].SetActive(false);
			if (value != ResourceType.None)
			{
				resources[(int)value].SetActive(true);
				if (hexGrid.emptyCells.Contains(this))
					hexGrid.emptyCells.Remove(this);
			} else {
				if (!hexGrid.emptyCells.Contains(this))
					hexGrid.emptyCells.Add(this);
			}
			resource = value;
		}	
	}

	public bool Walled {
		get {
			return walled;
		}
		set {
			if (walled != value) {
				walled = value;
				if (walled)
				{
					for (int child = 0; child < 6; child++)
					{
						walls[child].TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer);
						meshRenderer.material = materials[(int)sit < 0 ? 0 : (int)sit];
					}
					if (hexGrid.emptyCells.Contains(this))
						hexGrid.emptyCells.Remove(this);
				}
				for (HexDirection d = 0; d <= HexDirection.NW; d++)
				{
					HexCell neighbor = GetNeighbor(d);
					if (neighbor == null)
						continue;
					if(neighbor.Walled != walled)
					{
						walls[(int)d].SetActive(walled);
						neighbor.walls[(int)(d+3)%6].SetActive(neighbor.Walled);
					} 
					else if (neighbor.Walled && walled) 
					{
						bool sitStatus = neighbor.sit != sit;
						walls[(int)d].SetActive(sitStatus);
						neighbor.walls[(int)(d+3)%6].SetActive(sitStatus);
					}
				}
			}
		}
	}

	public int TerrainTypeIndex {
		get {
			return terrainTypeIndex;
		}
		set {
			if (terrainTypeIndex != value) {
				terrainTypeIndex = value;
				ShaderData.RefreshTerrain(this);
			}
		}
	}

	public bool IsVisible {
		get {
			return visibility > 0 && Explorable;
		}
	}

	public bool IsExplored {
		get {
			return explored && Explorable;
		}
		private set {
			explored = value;
		}
	}

	public bool Explorable { get; set; }

	public int Distance {
		get {
			return distance;
		}
		set {
			distance = value;
		}
	}

	public Unit Unit { get; set; }

	public HexCell PathFrom { get; set; }

	public int SearchHeuristic { get; set; }

	public int SearchPriority {
		get {
			return distance + SearchHeuristic;
		}
	}

	public int SearchPhase { get; set; }

	public HexCell NextWithSamePriority { get; set; }

	public HexCellShaderData ShaderData { get; set; }

	int terrainTypeIndex;

	int elevation = int.MinValue;
	int waterLevel;

	int specialIndex;

	int distance;

	int visibility;

	bool explored;
	
	ResourceType resource;

	public bool walled;
	bool buytile;
	bool hasIncomingRiver, hasOutgoingRiver;
	HexDirection incomingRiver, outgoingRiver;

	[SerializeField]
	HexCell[] neighbors;

	[SerializeField]
	bool[] roads;

	public void IncreaseVisibility () {
		visibility += 1;
		if (visibility == 1) {
			IsExplored = true;
			ShaderData.RefreshVisibility(this);
		}
	}

	public void DecreaseVisibility () {
		visibility -= 1;
		if (visibility == 0) {
			ShaderData.RefreshVisibility(this);
		}
	}

	public void ResetVisibility () {
		if (visibility > 0) {
			visibility = 0;
			ShaderData.RefreshVisibility(this);
		}
	}

	public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;
	}

	public HexEdgeType GetEdgeType (HexDirection direction) {
		return HexMetrics.GetEdgeType(
			elevation, neighbors[(int)direction].elevation
		);
	}

	public HexEdgeType GetEdgeType (HexCell otherCell) {
		return HexMetrics.GetEdgeType(
			elevation, otherCell.elevation
		);
	}

	public bool HasRiverThroughEdge (HexDirection direction) {
		return
			hasIncomingRiver && incomingRiver == direction ||
			hasOutgoingRiver && outgoingRiver == direction;
	}

	public void RemoveIncomingRiver () {
		if (!hasIncomingRiver) {
			return;
		}
		hasIncomingRiver = false;
		RefreshSelfOnly();

		HexCell neighbor = GetNeighbor(incomingRiver);
		neighbor.hasOutgoingRiver = false;
		neighbor.RefreshSelfOnly();
	}

	public void RemoveOutgoingRiver () {
		if (!hasOutgoingRiver) {
			return;
		}
		hasOutgoingRiver = false;
		RefreshSelfOnly();

		HexCell neighbor = GetNeighbor(outgoingRiver);
		neighbor.hasIncomingRiver = false;
		neighbor.RefreshSelfOnly();
	}

	public void RemoveRiver () {
		RemoveOutgoingRiver();
		RemoveIncomingRiver();
	}

	public void SetOutgoingRiver (HexDirection direction) {
		if (hasOutgoingRiver && outgoingRiver == direction) {
			return;
		}

		HexCell neighbor = GetNeighbor(direction);
		if (!IsValidRiverDestination(neighbor)) {
			return;
		}

		RemoveOutgoingRiver();
		if (hasIncomingRiver && incomingRiver == direction) {
			RemoveIncomingRiver();
		}
		hasOutgoingRiver = true;
		outgoingRiver = direction;

		neighbor.RemoveIncomingRiver();
		neighbor.hasIncomingRiver = true;
		neighbor.incomingRiver = direction.Opposite();

		SetRoad((int)direction, false);
	}

	public bool HasRoadThroughEdge (HexDirection direction) {
		return roads[(int)direction];
	}

	public void AddRoad (HexDirection direction) {
		if (
			!roads[(int)direction] && !HasRiverThroughEdge(direction) &&
			GetElevationDifference(direction) <= 1
		) {
			SetRoad((int)direction, true);
		}
	}

	public void RemoveRoads () {
		for (int i = 0; i < neighbors.Length; i++) {
			if (roads[i]) {
				SetRoad(i, false);
			}
		}
	}

	public int GetElevationDifference (HexDirection direction) {
		int difference = elevation - GetNeighbor(direction).elevation;
		return difference >= 0 ? difference : -difference;
	}

	bool IsValidRiverDestination (HexCell neighbor) {
		return neighbor && (
			elevation >= neighbor.elevation || waterLevel == neighbor.elevation
		);
	}

	void ValidateRivers () {
		if (
			hasOutgoingRiver &&
			!IsValidRiverDestination(GetNeighbor(outgoingRiver))
		) {
			RemoveOutgoingRiver();
		}
		if (
			hasIncomingRiver &&
			!GetNeighbor(incomingRiver).IsValidRiverDestination(this)
		) {
			RemoveIncomingRiver();
		}
	}

	void SetRoad (int index, bool state) {
		roads[index] = state;
		neighbors[index].roads[(int)((HexDirection)index).Opposite()] = state;
		neighbors[index].RefreshSelfOnly();
		RefreshSelfOnly();
	}

	void RefreshPosition () {
		Vector3 position = transform.localPosition;
		position.y = elevation * HexMetrics.elevationStep;
		position.y +=
			(HexMetrics.SampleNoise(position).y * 2f - 1f) *
			HexMetrics.elevationPerturbStrength;
		transform.localPosition = position;

		Vector3 uiPosition = uiRect.localPosition;
		uiPosition.z = -position.y;
		uiRect.localPosition = uiPosition;
	}

	void Refresh () {
		if (chunk) {
			chunk.Refresh();
			for (int i = 0; i < neighbors.Length; i++) {
				HexCell neighbor = neighbors[i];
				if (neighbor != null && neighbor.chunk != chunk) {
					neighbor.chunk.Refresh();
				}
			}
			if (Unit) {
				Unit.transform.localPosition = Unit.Location.Position;
			}
		}
	}

	void RefreshSelfOnly () {
		chunk.Refresh();
		if (Unit) {
			Unit.transform.localPosition = Unit.Location.Position;
		}
	}

	public void Save (BinaryWriter writer) {
		writer.Write((byte)terrainTypeIndex);
		writer.Write((byte)(elevation + 127));
		writer.Write((byte)waterLevel);
		writer.Write(walled);

		if (hasIncomingRiver) {
			writer.Write((byte)(incomingRiver + 128));
		}
		else {
			writer.Write((byte)0);
		}

		if (hasOutgoingRiver) {
			writer.Write((byte)(outgoingRiver + 128));
		}
		else {
			writer.Write((byte)0);
		}

		int roadFlags = 0;
		for (int i = 0; i < roads.Length; i++) {
			if (roads[i]) {
				roadFlags |= 1 << i;
			}
		}
		writer.Write((byte)roadFlags);
		writer.Write(IsExplored);
	}

	public void Load (BinaryReader reader, int header) {
		terrainTypeIndex = reader.ReadByte();
		ShaderData.RefreshTerrain(this);
		elevation = reader.ReadByte();
		if (header >= 4) {
			elevation -= 127;
		}
		RefreshPosition();
		waterLevel = reader.ReadByte();
		walled = reader.ReadBoolean();

		byte riverData = reader.ReadByte();
		if (riverData >= 128) {
			hasIncomingRiver = true;
			incomingRiver = (HexDirection)(riverData - 128);
		}
		else {
			hasIncomingRiver = false;
		}

		riverData = reader.ReadByte();
		if (riverData >= 128) {
			hasOutgoingRiver = true;
			outgoingRiver = (HexDirection)(riverData - 128);
		}
		else {
			hasOutgoingRiver = false;
		}

		int roadFlags = reader.ReadByte();
		for (int i = 0; i < roads.Length; i++) {
			roads[i] = (roadFlags & (1 << i)) != 0;
		}

		IsExplored = header >= 3 ? reader.ReadBoolean() : false;
		ShaderData.RefreshVisibility(this);
	}

	public void SetLabel (string text) {
		uiRect.TryGetComponent<Text>(out Text label);
		label.text = text;
	}

	public void DisableHighlight () {
		if (highlight == null)
		{
			uiRect.GetChild(0).TryGetComponent<Image>(out Image highlight);
			this.highlight = highlight;
		}
		highlight.enabled = false;
	}

	public void EnableHighlight (Color color) {
		if (highlight == null)
		{
			uiRect.GetChild(0).TryGetComponent<Image>(out Image highlight);
			this.highlight = highlight;
		}
		if (highlightBtn == null)
		{
			highlight.TryGetComponent<Button>(out Button btn);
			highlightBtn = btn;
		}
		highlight.color = color;
		highlight.enabled = true;
	}

	public bool IsEnabledHighlight ()
	{
		if (highlight == null)
		{
			uiRect.GetChild(0).TryGetComponent<Image>(out Image highlight);
			this.highlight = highlight;
		}
		if (highlightBtn == null)
		{
			highlight.TryGetComponent<Button>(out Button btn);
			highlightBtn = btn;
		}
		return highlight.enabled;
	}

	public void SetMapData (float data) {
		ShaderData.SetMapData(this, data);
	}

}