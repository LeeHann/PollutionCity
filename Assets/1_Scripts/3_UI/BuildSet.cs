using UnityEngine;

public class BuildSet : MonoBehaviour
{
    [SerializeField] HexGrid hexgrid;
    [SerializeField] GameObject LivingPrefab;
	[SerializeField] GameObject ResearchPrefab;
	[SerializeField] GameObject IndustrialPrefab;

    [SerializeField] SkillTree_Control skillTree;

    public HexCell GetCellUnderCursor()
    {
        return
            hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    public void LivingBuild()
    {
        HexCell cell = GetCellUnderCursor();        
        if (cell && !cell.Unit && TurnSystem.turnCity.cells.Contains(cell))
        {
            var obj = Instantiate(LivingPrefab).GetComponent<Unit>();
            obj.Grid = hexgrid;
            obj.Location = cell;
            obj.Orientation = Random.Range(0, 360f);

            HexUnit hexUnit = hexgrid.AddUnit(
                Instantiate(hexgrid.unitPrefab[(int)TurnSystem.turnCity.sit]), cell, Random.Range(0, 360f)
            );
            TurnSystem.turnCity.AddUnit(hexUnit);
        }
    }

    public void IndustrialBuild()
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit && TurnSystem.turnCity.cells.Contains(cell))
        {
            var unit = Instantiate(IndustrialPrefab).GetComponent<MFCUnit>();
            unit.Grid = hexgrid;
            unit.Location = cell;
            unit.Orientation = Random.Range(0, 360f);
            TurnSystem.turnCity.AddUnit(unit);
        }
    }

    public void ResearchBuild()
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit && TurnSystem.turnCity.cells.Contains(cell))
        {
            var unit = Instantiate(ResearchPrefab).GetComponent<RSUnit>();
            unit.skillTree = skillTree;
            unit.Grid = hexgrid;
            unit.Location = cell;
            unit.Orientation = Random.Range(0, 360f);
            TurnSystem.turnCity.AddUnit(unit);
        }
    }
}
