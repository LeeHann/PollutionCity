using UnityEngine;

public class BuildSet : MonoBehaviour
{
    [SerializeField] UINoticer noticer;
    [SerializeField] HexGrid hexgrid;
    [SerializeField] GameObject LivingPrefab;
	[SerializeField] GameObject ResearchPrefab;
	[SerializeField] GameObject IndustrialPrefab;

    [SerializeField] SkillTree_Control skillTree;
    [SerializeField] Button_Build button_Build;

    public HexCell GetCellUnderCursor()
    {
        return
            hexgrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    public void LivingBuild()
    {
        HexCell cell = GetCellUnderCursor();        
        if (cell && !cell.Unit && TurnSystem.turnCity.cells.Contains(cell)
        && TurnSystem.turnCity.Money >= TurnSystem.turnCity.buyBuildingPrice)
        {
            InstanceLiving(cell);
            TurnSystem.turnCity.Money -= TurnSystem.turnCity.buyBuildingPrice;
            TurnSystem.turnCity.buyBuildingPrice += 200;
            button_Build.ClosePanel();
        } else if (!TurnSystem.turnCity.cells.Contains(cell)) {
            noticer.Notice("도시 안에만 지을 수 있습니다.");
        }
    }

    public HexUnit InstanceLiving(HexCell cell)
    {
        Instantiate(LivingPrefab).TryGetComponent<Unit>(out Unit obj);
        obj.Grid = hexgrid;
        obj.Location = cell;
        obj.Orientation = Random.Range(0, 360f);

        HexUnit hexUnit = hexgrid.AddUnit(
            Instantiate(hexgrid.unitPrefab[(int)TurnSystem.turnCity.Sit]), cell, Random.Range(0, 360f)
        );
        TurnSystem.turnCity.AddUnit(hexUnit);
        return hexUnit;
    }

    public void IndustrialBuild()
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit && TurnSystem.turnCity.cells.Contains(cell)
        && TurnSystem.turnCity.Money >= TurnSystem.turnCity.buyBuildingPrice)
        {
            InstanceIndustrial(cell);
            TurnSystem.turnCity.Money -= TurnSystem.turnCity.buyBuildingPrice;
            TurnSystem.turnCity.buyBuildingPrice += 200;
            button_Build.ClosePanel();
        } else if (!TurnSystem.turnCity.cells.Contains(cell)) {
            noticer.Notice("도시 안에만 지을 수 있습니다.");
        }
    }

    public MFCUnit InstanceIndustrial(HexCell cell)
    {
        Instantiate(IndustrialPrefab).TryGetComponent<MFCUnit>(out MFCUnit unit);
        unit.Grid = hexgrid;
        unit.Location = cell;
        unit.Orientation = Random.Range(0, 360f);
        TurnSystem.turnCity.AddUnit(unit);
        return unit;
    }

    public void ResearchBuild()
    {
        HexCell cell = GetCellUnderCursor();
        if (cell && !cell.Unit && TurnSystem.turnCity.cells.Contains(cell)
        && TurnSystem.turnCity.Money >= TurnSystem.turnCity.buyBuildingPrice)
        {
            InstanceResearch(cell);
            TurnSystem.turnCity.Money -= TurnSystem.turnCity.buyBuildingPrice;
            TurnSystem.turnCity.buyBuildingPrice += 200;
            button_Build.ClosePanel();
        } else if (!TurnSystem.turnCity.cells.Contains(cell)) {
            noticer.Notice("도시 안에만 지을 수 있습니다.");
        }
    }

    public RSUnit InstanceResearch(HexCell cell)
    {
        Instantiate(ResearchPrefab).TryGetComponent<RSUnit>(out RSUnit unit);
        unit.skillTree = skillTree;
        unit.Grid = hexgrid;
        unit.Location = cell;
        unit.Orientation = Random.Range(0, 360f);
        TurnSystem.turnCity.AddUnit(unit);
        return unit;
    }
}
