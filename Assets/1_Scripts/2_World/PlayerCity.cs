using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerCity : City
{
	public event Action<string> notice;

    protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
        yield return new WaitUntil(() => action.TurnUnit == false);

        if (action.Location.Resource != ResourceType.None)    // Obtain Resources
        {
            if (IsResearched(action.Location.Resource))
            {
                UpdateTrash(action.Location.Resource, 1);
                notice(action.Location.Resource.Rsc2Str() + " 획득");
                action.Location.Resource = ResourceType.None;
            }
        }
        _coroutine = null;
    }


    protected override IEnumerator ActionResearcher(Unit action) // 연구 행동 결정 함수
    {
		
        action.TurnUnit = false;
		yield return new WaitUntil(() => action.TurnUnit == false);
		_coroutine = null;
	}

    protected override IEnumerator ActionManufacturer(Unit action) // 제조 행동 결정 함수
    {
		yield return null;
		_coroutine = null;
	}
    
    bool IsResearched(ResourceType type)
    {
        if (GetResearch((int)type) <= 0)
        {
            switch (type)
            {
                case ResourceType.Paper:
                    notice("종이 해금이 안된 상태입니다.");
                    break;
                case ResourceType.Can:
                    notice("캔 해금이 안된 상태입니다.");
                    break;
                case ResourceType.Plastic:
                    notice("플라스틱 해금이 안된 상태입니다.");
                    break;
                case ResourceType.Glass:
                    notice("유리 해금이 안된 상태입니다.");
                    break;
            }
            return false;
        }
        else return true;
    }
}
