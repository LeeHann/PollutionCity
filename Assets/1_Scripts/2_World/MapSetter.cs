using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapSetter : MonoBehaviour
{
    [SerializeField] Material terrainMaterial;
    [SerializeField] HexGrid hexGrid;
    [SerializeField] HexMapCamera cam;
    [SerializeField] UINoticer noticeUI;
    [SerializeField] Display[] displays;
    [SerializeField] Text unitText;

    private List<PlayerSit> sits = new List<PlayerSit>() {
        PlayerSit.Blue, PlayerSit.Red, PlayerSit.White, PlayerSit.Yellow
    };
    [SerializeField] List<City> cities = new List<City>();
    List<HexCell> highlights = new List<HexCell>();
    public static List<HexCell> occupiedCellList = new List<HexCell>();

    [SerializeField] string[] maps;
    const int mapFileVersion = 5;
    int i = 0;
    private void Start() 
    {
        terrainMaterial.DisableKeyword("GRID_ON");
        Shader.DisableKeyword("HEX_MAP_EDIT_MODE");

        string fileName = "Maps/" + maps[UnityEngine.Random.Range(0, maps.Length)]  + ".map";

#if ( UNITY_EDITOR || UNITY_STANDALONE_WIN )
        string path = (Application.streamingAssetsPath + "/" + fileName);
        Load(path);

#else
        StartCoroutine(LoadFileOnAndroid(fileName));

#endif

        cities[i] = GenerateCity(isPlayer:true);
        for (i = 1; i < GameInfo.cityCount; i++)
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
        CameraPositioning(cell, isPlayer);
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

        city.display = displays[i];
        city.Sit = sits[random];
        sits.RemoveAt(random);
        
        city.Grid = hexGrid;
        city.rootCell = cell;
        city.AddCell(cell);
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            city.AddCell(cell.GetNeighbor(d));
        }
        
        city.Money = GameInfo.startMoney;
        if (isPlayer)
            city.PA = GameInfo.startPA;
        else
            city.PA = GameInfo.startPA + (int)(GameInfo.startPA * Random.Range(-0.1f, 0.15f));
        
        if (isPlayer)
        {
            city.TryGetComponent<PlayerCity>(out PlayerCity p);
            p.notice += noticeUI.Notice;
            city.unitText = unitText;
        }
        // place units
        city.AddUnit(
            hexGrid.AddUnit(
                Instantiate(hexGrid.unitPrefab[(int)city.Sit]), cell, Random.Range(0f, 360f)
            )
        );
        
        city.cam = cam;
        return city;
    }

    public void OnLandBuyButton()
    {
        if (!TurnSystem.turnCity.isPlayer) return;
        if (occupiedCellList.Count > 0)
		{
			occupiedCellList.ForEach((cell)=> {
				cell.DisableHighlight();
				cell.highlightBtn.onClick.RemoveAllListeners();
			});
            occupiedCellList.Clear();
            return;
		}

        List<HexCell> cells = TurnSystem.turnCity.cells;

        for(int cell = 0; cell <= cells.Count-1; cell++)
        {
            for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
            {
                HexCell neighbor = cells[cell].GetNeighbor(d);
                if (neighbor == null)
                    continue;

                if (neighbor.walled == false && !neighbor.IsEnabledHighlight())                //???????????? cells
                {
                    neighbor.EnableHighlight(Color.green);
                    highlights.Add(neighbor);
                    occupiedCellList.Add(neighbor);
                    neighbor.highlightBtn.onClick.RemoveAllListeners();
                    neighbor.highlightBtn.onClick.AddListener(() => OnClickLandbuyHighlight(neighbor));
                }
            }
        }
    }

    public void OnClickLandbuyHighlight(HexCell cell)
    {
        int price = TurnSystem.turnCity.rootCell.coordinates.DistanceTo(cell.coordinates) * 100;
        if (TurnSystem.turnCity.Money > price)
        {
            TurnSystem.turnCity.Money -= price;
            for (int i = highlights.Count - 1; i >= 0; i--)
            {
                highlights[i].DisableHighlight();
                highlights[i].highlightBtn.onClick.RemoveAllListeners();
                highlights.RemoveAt(i);
                occupiedCellList.Remove(cell);
            }
            occupiedCellList.Clear();
            TurnSystem.turnCity.AddCell(cell);
        }  
        else {
            noticeUI.Notice("?????? ???????????????.");
            for (int i = highlights.Count - 1; i >= 0; i--)
            {
                highlights[i].DisableHighlight();
                highlights[i].highlightBtn.onClick.RemoveAllListeners();
                highlights.RemoveAt(i);
                occupiedCellList.Remove(cell);
            }
            occupiedCellList.Clear();
        }
    }

    public void ScatterResources(int rscVal)
    {
        // ?????? ?????????
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
                cam.WrapPosition(cell.transform.localPosition);
    }
}
