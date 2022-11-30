using UnityEngine;
using UnityEngine.UI;

public class SaveLoadItem : MonoBehaviour {

	public SaveLoadMenu menu;

	public string MapName {
		get {
			return mapName;
		}
		set {
			mapName = value;
			transform.GetChild(0).TryGetComponent<Text>(out Text text);
			text.text = value;
		}
	}

	string mapName;

	public void Select () {
		menu.SelectItem(mapName);
	}
}