using UnityEngine;
using UnityEngine.EventSystems;

public class HexGameUI : MonoBehaviour {

	public HexGrid grid;

	HexCell currentCell;

	HexUnit selectedUnit;

	// void Update () {
	// 	if (!EventSystem.current.IsPointerOverGameObject()) {
	// 		if (Input.GetMouseButtonDown(0)) {
	// 			DoSelection();
	// 		}
	// 		else if (selectedUnit) {
	// 			if (Input.GetMouseButtonDown(1)) {
	// 				DoMove();
	// 			}
	// 			else {
	// 				DoPathfinding();
	// 			}
	// 		}
	// 	}
	// }

	void DoSelection () {
		grid.ClearPath();
		UpdateCurrentCell();
		if (currentCell) {
			selectedUnit = (HexUnit)currentCell.Unit;
		}
	}

	void DoPathfinding () {
		if (UpdateCurrentCell()) {
			if (currentCell && selectedUnit.IsValidDestination(currentCell)) {
				grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
			}
			else {
				grid.ClearPath();
			}
		}
	}

	void DoMove () {
		if (grid.HasPath) {
			selectedUnit.Travel(grid.GetPath());
			grid.ClearPath();
		}
	}

	bool UpdateCurrentCell () {
		HexCell cell =
			grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
		if (cell != currentCell) {
			currentCell = cell;
			return true;
		}
		return false;
	}

	public void OnclickToTitle()
	{
		Loading.LoadSceneHandle("Title");
	}
}