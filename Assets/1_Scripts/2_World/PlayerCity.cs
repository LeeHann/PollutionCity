using System.Collections;
using UnityEngine;
using System;

public class PlayerCity : City
{
	public event Action<string> notice;
    private void Start() {
        isPlayer = true;
    }
    
    protected override IEnumerator Scheduler()
    {
        unitText.text = actions.Count + " / " + units.Count;
        CameraPositioning(rootCell.gameObject);
        for (int i=0; i<actions.Count; i++)
        {
            actions[i].TurnUnit = true;
        }
        yield return new WaitUntil(() => actions.Count == 0);
		myTurn = false;	
    }

    protected override IEnumerator ActionExplorer(HexUnit action) // 탐사 행동 결정 함수
    {
        CameraPositioning(action.gameObject);
        yield return new WaitUntil(() => action.TurnUnit == false);
        _coroutine = null;
    }

    public override void PostExplorer(Unit action)
    {
        if (action.Location.Resource != ResourceType.None)    // Obtain Resources
        {
            if (IsResearched(action.Location.Resource))
            {
                int ran = UnityEngine.Random.Range(20, 50);
                UpdateTrash(action.Location.Resource, ran);
                PA -= ran * 10;
                notice(action.Location.Resource.Rsc2Str() + " 획득\n<size=30>오염 " + (ran*10).ToString()+" 감소</size>");
                action.Location.Resource = ResourceType.None;
            }
        }
    }
    
    bool IsResearched(ResourceType type)
    {
        if (type > ResourceType.Money && GetResearch((int)type) <= 0)
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
