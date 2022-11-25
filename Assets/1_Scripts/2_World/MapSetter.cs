using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class MapSetter : MonoBehaviour
{
    [SerializeField] Material terrainMaterial;
    [SerializeField] HexGrid hexGrid;
    [SerializeField] HexMapCamera cam;
    [SerializeField] UINoticer noticeUI;

    private List<PlayerSit> sits = new List<PlayerSit>() {
        PlayerSit.Blue, PlayerSit.Red, PlayerSit.White, PlayerSit.Yellow
    };
    [SerializeField] List<City> cities = new List<City>();
    [SerializeField] GameObject[] units;
    List<HexCell> highlights = new List<HexCell>();
    [SerializeField] string[] maps;
    const int mapFileVersion = 5;

    private void Start() 
    {
        terrainMaterial.DisableKeyword("GRID_ON");
		// Shader.EnableKeyword("HEX_MAP_EDIT_MODE");

        string fileName = "Maps/" + maps[UnityEngine.Random.Range(0, maps.Length)]  + ".map";

#if ( UNITY_EDITOR || UNITY_STANDALONE_WIN )
        string path = (Application.streamingAssetsPath + "/" + fileName);
        Load(path);

#else
        StartCoroutine(LoadFileOnAndroid(fileName));

#endif

        cities[0] = GenerateCity(isPlayer:true);
        for (int i = 1; i < GameInfo.cityCount; i++)
        {
            cities[i] = GenerateCity();
        }
        TurnSystem.cities = this.cities;
        
        ScatterResources(30);
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

    City GenerateCity(bool isPlayer = false)
    {
        HexCell cell = SearchValidCityPoint();
        City city = SetCityProperty(cell, isPlayer);
        cell.SetLabel(isPlayer.ToString());
        CameraPositioning(cell, isPlayer);
        //LandBuy(cell, isPlayer);
        return city;
    }

    HexCell SearchValidCityPoint()
    {
        HexCell cell;

        bool invalid;
        do {
            int random = UnityEngine.Random.Range(0, hexGrid.emptyCells.Count-1);
            cell = hexGrid.emptyCells[random];

            invalid = cell.IsUnderwater | cell.Walled;
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                if (invalid) break;
                HexCell neighbor = cell.GetNeighbor(d);
                if (neighbor == null) 
                    invalid = true;
                else
                    invalid |= neighbor.IsUnderwater | neighbor.Walled;
            }
        } while (invalid);
        
        return cell;
    }

    
    City SetCityProperty(HexCell cell, bool isPlayer)
    {
        City city = isPlayer ? new GameObject(name:"PlayerCity").AddComponent<PlayerCity>() 
                                : new GameObject(name:"AICity").AddComponent<AICity>();
        int random = Random.Range(0, sits.Count);
        
        city.sit = sits[random];
        sits.RemoveAt(random);

        city.rootCell = cell;
        city.AddCell(cell);
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            city.AddCell(cell.GetNeighbor(d));
        }
        //LandBuy(cell, isPlayer);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (cell.walled != true)
        //        cell.walled = true;
        //}

        city.Money = GameInfo.startMoney;
        city.PA = GameInfo.startPA;
        if (isPlayer)
            city.GetComponent<PlayerCity>().notice += noticeUI.Notice;
        
        // place units (explorer, lab)
        city.AddUnit(
            hexGrid.AddUnit(
                Instantiate(hexGrid.unitPrefab[(int)city.sit]), cell, Random.Range(0f, 360f)
            )
        );
        
        city.cam = cam;
        return city;
    }

    public void LandBuy(HexCell cell, bool isPlayer)
    {
        City city = isPlayer ? GameObject.Find("PlayerCity").GetComponent<PlayerCity>()
                                : GameObject.Find("AiCity").GetComponent<AICity>();

        city.AddLandMark(cell);
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            cell.GetNeighbor(d);
            if (Input.GetMouseButtonDown(0))
            {
                if (cell.walled != true)
                {
                    cell.walled = true;
                }
            }
        }
      
        //if(playerCity)
        //{
        //    cell.EnableHighlight(Color.blue);
        //}
    
        //if (isPlayer )
        //{
        //   cell.EnableHighlight(Color.green);

        //}
        //for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        //{
        //    cell.EnableHighlight(Color.green);
        //}

        //if(SetCityProperty())
        {

        }
    }


    /*
        cell.EnableHighlight(Color.green);
        highlights.Add(cell);

        cell.highlightBtn.onClick.RemoveAllListeners();
        cell.highlightBtn.onClick.AddListener(() => OnClickLandbuyHighlight(cell));
    */



    public void OnLandBuyButton()
    {
        List<HexCell> cells = TurnSystem.turnCity.cells;

        for(int cell = 0; cell <= cells.Count-1; cell++)
        {
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {

                HexCell neighbor = cells[cell].GetNeighbor(d);
                if (neighbor == null)
                    continue;


                if (neighbor.walled == false && !neighbor.IsEnabledHighlight())                //가장자리 cells
                {
                   neighbor.EnableHighlight(Color.green);
                    highlights.Add(neighbor);
                   neighbor.highlightBtn.onClick.RemoveAllListeners();
                   neighbor.highlightBtn.onClick.AddListener(() => OnClickLandbuyHighlight(neighbor));
                }
            
            }
        }

       

    }

    public void OnClickLandbuyHighlight(HexCell cell)
    {
        for (int i = highlights.Count - 1; i >= 0; i--)
        {
            highlights[i].DisableHighlight();
            highlights[i].highlightBtn.onClick.RemoveAllListeners();
            highlights.RemoveAt(i);
        }
        TurnSystem.turnCity.AddCell(cell);
    }

    public void OnClickLandbuy(HexCell cell)
    {
        //도시 셀들 가져와야함
        if (cell.walled)
        {
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                cell.GetNeighbor(d);
                cell.EnableHighlight(Color.green);
            }
        }
    }

    public void ScatterResources(int rscVal)
    {
        // 자원 뿌리기
        int count = 0;
        int resourceCnt = rscVal;
        while (count < resourceCnt && count < hexGrid.emptyCells.Count)
        {
            HexCell cell;
            bool invalid;
            do {
                int random = UnityEngine.Random.Range(0, hexGrid.emptyCells.Count-1);
                cell = hexGrid.emptyCells[random];
                invalid = cell.IsUnderwater | cell.Walled | cell.Resource != ResourceType.None;
            } while (invalid);
            ResourceType resourceType = (ResourceType)UnityEngine.Random.Range(
                (int)ResourceType.Money, (int)ResourceType.Plastic
            );
            cell.Resource = resourceType;

            count++;
        }
    }

    void CameraPositioning(HexCell cell, bool isPlayer)
    {
        if (isPlayer)
            cam.transform.localPosition = 
                hexGrid.wrapping ? 
                cam.WrapPosition(cell.transform.localPosition) : 
                cam.ClampPosition(cell.transform.localPosition);
    }
}
