using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICity : City
{
	private void Start() 
	{
		// Initiate Pollutant Amount
		PA = 5000 + (int)(5000 * Random.Range(-0.1f, 0.15f));
	}

	protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
		// choose position to move
		yield return new WaitUntil(() => action.TurnUnit == false);
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			// TODO: if : Was it researched?

			trash[(int)action.Location.Resource]++;
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
