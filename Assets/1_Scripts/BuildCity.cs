using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BuildCity : MonoBehaviour
{
    const int mapFileVersion = 5;
    public Material terrainMaterial;
    public HexGrid hexGrid;
    public HexMapCamera cam;
    [SerializeField] string[] mapName;
    HexCell cell;

    public void OnclickLoadMap()
    {
        terrainMaterial.DisableKeyword("GRID_ON");
		Shader.EnableKeyword("HEX_MAP_EDIT_MODE");

        string path = Path.Combine(
            // Application.persistentDataPath, mapName[Random.Range(0, 3)]+".map"
            Application.dataPath, mapName[Random.Range(0, 3)]+".map"
        );
        Load(path);
    }

    public void OnclickBuildCity()
    {
        GenerateCities();
    }

    public void GenerateCities()
    {
        bool invalid;
        do {
            int randomX = Random.Range(0, hexGrid.cellCountX);
            int randomZ = Random.Range(0, hexGrid.cellCountZ);
            cell = hexGrid.GetCell(randomX, randomZ);

            invalid = false;
            invalid |= cell.IsUnderwater;
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                if (invalid) break;
                HexCell neighbor = cell.GetNeighbor(d);
                if (neighbor == null) 
                {
                    invalid = true;
                    break;
                }
                invalid |= neighbor.IsUnderwater;
            }
        } while (invalid);
        
        cell.Walled = true;
        for (HexDirection d=HexDirection.NE; d<=HexDirection.NW; d++)
        {
            HexCell neighbor = cell.GetNeighbor(d);
            neighbor.Walled = true;
        }
        cam.transform.localPosition = 
            hexGrid.wrapping ? 
            cam.WrapPosition(cell.transform.localPosition) : 
            cam.ClampPosition(cell.transform.localPosition);
    }

    public void OnclickSetCities()
    {
        int citiesNum = 3;
        
        for (int i=0; i<citiesNum; i++)
        {
            GenerateCities();
        }
    }

    void Load (string path) {
		if (!File.Exists(path)) {
			Debug.LogError("File does not exist " + path);
			return;
		}
		using (BinaryReader reader = new BinaryReader(File.OpenRead(path))) {
			int header = reader.ReadInt32();
			if (header <= mapFileVersion) {
				hexGrid.Load(reader, header);
				HexMapCamera.ValidatePosition();
			}
			else {
				Debug.LogWarning("Unknown map format " + header);
			}
		}
	}
}
