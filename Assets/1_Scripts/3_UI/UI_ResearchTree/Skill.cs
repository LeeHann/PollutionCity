using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static UI_ResearchTree;
public class Skill : Unit
{
    ResourceType resourceType;
    HexCell cell;

    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;
    public Button UpgradePaperBtn;
    public Button UpgradeCanBtn;
    public Button UpgradeGlassBtn;
    public Button UpgradePlasticBtn;

    [SerializeField]
    SkillTree_Control skillTree_Control;


    public City city
    {
        get
        {
            return TurnSystem.turnCity;
        }
    }

    City _City;


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


    public void UnLock()        // 일쓰 & 재활용 버튼 클릭용도
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        skilltree.UpdateAllSkillUI();
    }

    public void UnLock_Environment_Paper()       
    {
        //ActionResearcher(action) = 1

        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])//&& ActionResearcher(action) = 0
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        if(skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradePaperBtn.interactable = false;
        }

        city.UpdateResearch(2);




        skilltree.UpdateAllSkillUI();
        TurnUnit = false;
        skillTree_Control.CloseSkillTree();
    }
    public void UnLock_Environment_Can()
    {
        resourceType = ResourceType.Can;
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        if (skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradeCanBtn.interactable = false;
        }

        city.UpdateResearch(3);

        skilltree.UpdateAllSkillUI();
        TurnUnit = false;
        skillTree_Control.CloseSkillTree();
    }
    public void UnLock_Environment_Glass() 
    {
        resourceType = ResourceType.Glass;
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        if (skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradeGlassBtn.interactable = false;
        }

        city.UpdateResearch(4);

        skilltree.UpdateAllSkillUI();
        TurnUnit = false;
        skillTree_Control.CloseSkillTree();
    }
    public void UnLock_Environment_Plastic() 
    {
        resourceType = ResourceType.Plastic;
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        if (skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradePlasticBtn.interactable = false;
        }

        city.UpdateResearch(5);

        skilltree.UpdateAllSkillUI();
        TurnUnit = false;
        skillTree_Control.CloseSkillTree();
    }

    public void PollutionLevelUp()  
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;


        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;

    
            if (skilltree.SkillLevels[id] == 1)
            {
                city.PA -= (int)(city.PA * 0.835f);
            }
            else if (skilltree.SkillLevels[id] == 2)
            {
                city.PA -= (int)(city.PA * 0.720f);
            }
             else if (skilltree.SkillLevels[id] == 3)
            {
            city.PA   -= (int)(city.PA * 0.445f);
            }
            else if (skilltree.SkillLevels[id] == 4)
            {
                city.PA -= (int)(city.PA * 0.275f);
            }
            else if (skilltree.SkillLevels[id] == 5)
            {
                city.PA -= (int)(city.PA * 0.170f);
            }
            else if (skilltree.SkillLevels[id] == 6)
            {
                city.PA -= (int)(city.PA * 0.105f);
            }
            else if (skilltree.SkillLevels[id] == 7)
            {
                city.PA -= (int)(city.PA * 0.065f);
            }
            else if (skilltree.SkillLevels[id] == 8)
            {
                city.PA -= (int)(city.PA * 0.040f);
            }
            else if (skilltree.SkillLevels[id] == 9)
            {
                city.PA -= (int)(city.PA * 0.025f);
            }
            else if (skilltree.SkillLevels[id] == 10)
            {
                city.PA -= (int)(city.PA * 0.015f);
            }
            else if (skilltree.SkillLevels[id] == 11)
            {
                city.PA -= (int)(city.PA * 0.010f);
            }
            else if (skilltree.SkillLevels[id] == 12)
            {
                city.PA -= (int)(city.PA * 0.005f);
            }
            else if (skilltree.SkillLevels[id] == 13)
            {
                city.PA -= (int)(city.PA * 0.005f);
            }
            else
            {
                city.PA -= (int)(city.PA * 0.003f);
            }

        Debug.Log(city.PA);
        

        skilltree.UpdateAllSkillUI();
        TurnUnit = false;
        

        skillTree_Control.CloseSkillTree();
    }

    public void OpenResBuild()
    {
        skillTree_Control.OpenResearchBuilding();

        skilltree.UpdateAllSkillUI();
    }

}
