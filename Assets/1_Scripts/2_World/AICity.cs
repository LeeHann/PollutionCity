using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICity : City
{
	protected override IEnumerator ActionExplorer(Unit action) // 탐사 행동 결정 함수
    {
		yield return new WaitUntil(() => action.TurnUnit == false);
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
