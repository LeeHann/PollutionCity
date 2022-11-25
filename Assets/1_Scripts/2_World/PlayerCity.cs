using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerCity : City
{
	private void Start() 
	{
		// Initiate Pollutant Amount
		PA = 5000;
	}

	public event Action<string> notice;

	protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
		yield return new WaitUntil(() => action.TurnUnit == false);
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			// TODO: if : Was it researched?

			UpdateTrash(action.Location.Resource, 1);
			notice(action.Location.Resource.Rsc2Str() + " 획득");
			action.Location.Resource = ResourceType.None;
		}
		_coroutine = null;
	}

    protected override IEnumerator ActionResearcher(Unit action) // 연구 행동 결정 함수
    {
		/*
		 플레이어턴일때 연구를 진행했으면 false로
		 */
		yield return new WaitUntil(() => action.TurnUnit == false);
		/*
			if(~~~~~~~~~~~~~~~~~~~~~)
			{
				
		*/
		_coroutine = null;
	}

    protected override IEnumerator ActionManufacturer(Unit action) // 제조 행동 결정 함수
    {
		yield return null;
		_coroutine = null;
	}
}
