using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class MapAI : MonoBehaviour
{
    public Material terrainMaterial;
    public HexGrid hexGrid;
    [SerializeField] string[] maps;
    const int mapFileVersion = 5;

    public TextMeshProUGUI errorText;

    private void Start() 
    {
        terrainMaterial.DisableKeyword("GRID_ON");
		Shader.EnableKeyword("HEX_MAP_EDIT_MODE");

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
}
