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
	Skill research;
	protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
		yield return new WaitUntil(() => action.TurnUnit == false);
		if(research == null)
        {
			research = FindObjectOfType<Skill>();
        }
		if (action.Location.Resource != ResourceType.None)	// Obtain Resources
		{
			// TODO: if : Was it researched?
			if (action.Location.Resource == ResourceType.Can && research.UpgradeCanBtn.interactable != false)
			{
				notice("캔류 해금이 안된상태입니다.");
				_coroutine = null;
			}
			else if (action.Location.Resource == ResourceType.Glass && research.UpgradeGlassBtn.interactable != false)
			{
				notice("유리류 해금이 안된상태입니다.");
				_coroutine = null;
			}
			else if (action.Location.Resource == ResourceType.Plastic && research.UpgradePlasticBtn.interactable != false)
			{
				notice("플라스틱류 해금이 안된상태입니다.");
				_coroutine = null;
			}
			else if (action.Location.Resource == ResourceType.Paper && research.UpgradePaperBtn.interactable != false)
			{
				notice("종이류 해금이 안된상태입니다.");
				_coroutine = null;
			}
			else
			{
				UpdateTrash(action.Location.Resource, 1);
				notice(action.Location.Resource.Rsc2Str() + " 획득");
				action.Location.Resource = ResourceType.None;
			}
		}
		Debug.Log(PA);
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
