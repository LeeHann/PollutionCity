using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;

public class BuildCity : MonoBehaviour
{
    const int mapFileVersion = 5;
    public Material terrainMaterial;
    public HexGrid hexGrid;
    public HexMapCamera cam;
    [SerializeField] string[] maps;
    HexCell cell;

    public TextMeshProUGUI errorText;

    private void Start() 
    {
        terrainMaterial.DisableKeyword("GRID_ON");
		Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
    }

    public void OnclickLoadMap()
    {
        string fileName = "Maps/" + maps[UnityEngine.Random.Range(0, maps.Length)]  + ".map";

#if ( UNITY_EDITOR || UNITY_STANDALONE_WIN )
        string path = (Application.streamingAssetsPath + "/" + fileName);
        Load(path);
#else
        StartCoroutine(LoadFileOnAndroid(fileName));
#endif
    }

    void Load (string path) {
		if (!File.Exists(path)) {
			Debug.LogError("File does not exist " + path);
            errorText.text = "File does not exist " + path;
			return;
		}
		using (BinaryReader reader = new BinaryReader(File.OpenRead(path))) {
            errorText.text = "open file!";
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

    IEnumerator LoadFileOnAndroid(string fileName)
    {
        string path =  "jar:file://" + Application.dataPath + "!/assets/" + fileName;
        using (WWW file = new WWW(path))
        {
            yield return file;
            MemoryStream ms = new MemoryStream(file.bytes);
            BinaryReader reader = new BinaryReader(ms);
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

    public void OnclickBuildCity()
    {
        GenerateCities();
    }

    public void GenerateCities()
    {
        bool invalid;
        do {
            int randomX = UnityEngine.Random.Range(0, hexGrid.cellCountX);
            int randomZ = UnityEngine.Random.Range(0, hexGrid.cellCountZ);
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
}
