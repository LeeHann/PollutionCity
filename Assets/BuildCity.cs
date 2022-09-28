using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BuildCity : MonoBehaviour
{
    const int mapFileVersion = 5;
    public HexGrid hexGrid;
    [SerializeField] string[] mapName;

    public void OnclickLoadMap()
    {
        string path = Path.Combine(
            Application.persistentDataPath, mapName[Random.Range(0, 3)]+".map"
        );
        Load(path);
    }

    public void OnclickBuildCity()
    {
        // 인접한 7개 타일이 있는 곳을 선택해서 도시 벽 세우기
        int randomX = Random.Range(0, hexGrid.cellCountX);
        int randomZ = Random.Range(0, hexGrid.cellCountZ);
        HexCell cell = hexGrid.GetCell(randomX, randomZ);
        cell.Walled = true;
        
        Debug.Log(string.Format("({0}, {1})", randomX, randomZ));
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
