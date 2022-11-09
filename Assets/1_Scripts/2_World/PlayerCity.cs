using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCity : City
{	
	WaitForSeconds dot1 = new WaitForSeconds(0.1f);
	protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
		yield return new WaitUntil(() => action.TurnUnit == false);
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			// TODO: if : Was it researched?

			//////////////////////////////
			trash[(int)action.Location.Resource]++;
			// ui
			Image notice = Instantiate(noticeUI).GetComponent<Image>();
			Color color = Color.white;
			while (notice.color.a > 0)
			{
				color.a -= Time.deltaTime;
				notice.color = color;
				yield return dot1;
			}
			action.Location.Resource = ResourceType.None;
		}
		_coroutine = null;
	}

    protected override IEnumerator ActionResearcher(Unit action) // 연구 행동 결정 함수
    {
		yield return null;
		_coroutine = null;
	}

    protected override IEnumerator ActionManufacturer(Unit action) // 제조 행동 결정 함수
    {
		yield return null;
		_coroutine = null;
	}
}
