using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MFCUnit : Unit, IPointerClickHandler
{
	City city;

	private void Start() {
		city = TurnSystem.turnCity;
	}
	
	public void OnPointerClick(PointerEventData e)
	{
		// 클릭 시 제조 스크롤 뷰 활성화
		MFCUI.Instance.UIOpen();
		MFCUI.Instance.SetUnit(this);
		MFCSystem.Instance.SetUnit(this);
	}
}
