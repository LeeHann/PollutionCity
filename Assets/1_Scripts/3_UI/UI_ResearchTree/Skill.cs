using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UI_ResearchTree;

public class Skill : MonoBehaviour
{
    ResourceType resourceType;
    HexCell cell;

    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skilltree.SkillLevels[id]}/{skilltree.SkillCaps[id]}" +        //스킬트리 레벨 & 최대치
            $"\n{skilltree.SkillNames[id]}";                                                //스킬이름

        DescriptionText.text = $"{""}";//$"{skilltree.SkillDescription[id]}\n{"턴당한번만 가능합니다"}";

        GetComponent<Image>().color = skilltree.SkillLevels[id] >= skilltree.SkillCaps[id] ? Color.yellow   //레벨max다다르면 노랑색
            : skilltree.Money >= 1 ? Color.green : Color.white;                                             //재화있으면 초록 다썼으면 흰색으로 표시..

        foreach(var connectedSkill in ConnectedSkills)
        {
            skilltree.SkillList[connectedSkill].gameObject.SetActive(skilltree.SkillLevels[id] > 0);
            skilltree.ConnectorList[connectedSkill].SetActive(skilltree.SkillLevels[id] > 0);
        }

    }




    /*
        UnLock을 제외한 모든 기능들은 마지막줄에 기능 비활성화 & 다시 턴이 돌아오면 활성화 넣어줘야함   
    */


    public void UnLock()        // 일쓰 & 재활용 버튼 클릭용도
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        skilltree.UpdateAllSkillUI();
    }

    public void UnLock_Environment_Paper()        //자원 해금 클릭용도
    {
        //ActionResearcher(action) = 1

        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])//&& ActionResearcher(action) = 0
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        /*
            눌렀을 때 이제 잠겨있던 자원들을 해금 
            lock == false >>>>>> 해당자원 이제 먹을 수 있음
            
            
        */

        /*
            해금 시스템
            1. 잠겨있으면 캐릭터가 여행중일때 올라가도 못먹게. ( bool ~~~로 만들어서 true면 못먹고 false때 먹게  )
            2. 해금버튼 누르면 bool == true 를 false로 변경



            
            
        */
        resourceType = ResourceType.Paper;
        if(Input.GetMouseButtonDown(0))
        {
            //ActionResearcher(action)--
        }
        
        

        skilltree.UpdateAllSkillUI();
    }
    public void UnLock_Environment_Can()        //자원 해금 클릭용도
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        /*
            눌렀을 때 이제 잠겨있던 자원들을 해금 
        */

        /*
            해금 시스템
            1. 잠겨있으면 캐릭터가 여행중일때 올라가도 못먹게. ( bool ~~~로 만들어서 true면 못먹고 false때 먹게  )
            2. 해금버튼 누르면 bool == true 를 false로 변경
        */
        resourceType = ResourceType.Can;


        skilltree.UpdateAllSkillUI();
    }
    public void UnLock_Environment_Glass()        //자원 해금 클릭용도
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        /*
            눌렀을 때 이제 잠겨있던 자원들을 해금 
        */

        /*
            해금 시스템
            1. 잠겨있으면 캐릭터가 여행중일때 올라가도 못먹게. ( bool ~~~로 만들어서 true면 못먹고 false때 먹게  )
            2. 해금버튼 누르면 bool == true 를 false로 변경
        */
        resourceType = ResourceType.Glass;


        skilltree.UpdateAllSkillUI();
    }
    public void UnLock_Environment_Plastic()        //자원 해금 클릭용도
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        /*
            눌렀을 때 이제 잠겨있던 자원들을 해금 
        */

        /*
            해금 시스템
            1. 잠겨있으면 캐릭터가 여행중일때 올라가도 못먹게. ( bool ~~~로 만들어서 true면 못먹고 false때 먹게  )
            2. 해금버튼 누르면 bool == true 를 false로 변경
        */
        resourceType = ResourceType.Plastic;


        skilltree.UpdateAllSkillUI();
    }

    public void PollutionLevelUp()      //소각&매립 => 레벨시스템용도.
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        /*  
         *  SkillList[1] 
        
            레벨시스템 & 레벨별 감소율 넣어줘야함
            PA에 -= 
                    skilltree.SkillLevels[id]에
                    id++될수록 밑에 감소율 증가...

            lv1 = 1.165
            +=
            lv2 = 0.720
            +=
            lv3 = 0.445
            ....

        */



        

        skilltree.UpdateAllSkillUI();
    }

    
}
