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
    public Button UpgradePaperBtn;
    public Button UpgradeCanBtn;
    public Button UpgradeGlassBtn;
    public Button UpgradePlasticBtn;

    City city;
    PlayerCity playerCity;

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
        ActionResearcher(action) == 1 물어보기
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
        if(skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradePaperBtn.interactable = false;
        }

        resourceType = ResourceType.Paper;
        

        
        

        skilltree.UpdateAllSkillUI();
    }
    public void UnLock_Environment_Can()        //자원 해금 클릭용도
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


        skilltree.UpdateAllSkillUI();
    }
    public void UnLock_Environment_Glass()        //자원 해금 클릭용도
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



        skilltree.UpdateAllSkillUI();
    }
    public void UnLock_Environment_Plastic()        //자원 해금 클릭용도
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


        skilltree.UpdateAllSkillUI();
    }

    public void PollutionLevelUp()      //소각&매립 => 레벨시스템용도.
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;


        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;

        if (playerCity == null)
        {
            playerCity = FindObjectOfType<PlayerCity>();
        }
            if (skilltree.SkillLevels[id] == 1)
            {
                playerCity.PA -= playerCity.PA * 1.165f;
            }
            else if (skilltree.SkillLevels[id] == 2)
            {
                playerCity.PA *= 0.720f;
            }
            else if (skilltree.SkillLevels[id] == 3)
            {
                playerCity.PA *= Mathf.Round(0.445f);
            }
            else if (skilltree.SkillLevels[id] == 4)
            {
                playerCity.PA *= Mathf.Round(0.275f);
            }
            else if (skilltree.SkillLevels[id] == 5)
            {
                playerCity.PA *= Mathf.Round(0.170f);
            }
            else if (skilltree.SkillLevels[id] == 6)
            {
                playerCity.PA *= Mathf.Round(0.105f);
            }
            else if (skilltree.SkillLevels[id] == 7)
            {
                playerCity.PA *= Mathf.Round(0.065f);
            }
            else if (skilltree.SkillLevels[id] == 8)
            {
                playerCity.PA *= Mathf.Round(0.040f);
            }
            else if (skilltree.SkillLevels[id] == 9)
            {
                playerCity.PA *= Mathf.Round(0.025f);
            }
            else if (skilltree.SkillLevels[id] == 10)
            {
                playerCity.PA *= Mathf.Round(0.015f);
            }
            else if (skilltree.SkillLevels[id] == 11)
            {
                playerCity.PA *= Mathf.Round(0.010f);
            }
            else if (skilltree.SkillLevels[id] == 12)
            {
                playerCity.PA *= Mathf.Round(0.005f);
            }
            else if (skilltree.SkillLevels[id] == 13)
            {
                playerCity.PA *= Mathf.Round(0.005f);
            }
            else
            {
                playerCity.PA *= Mathf.Round(0.003f);
            }
        
        Debug.Log(playerCity.PA);
        

        skilltree.UpdateAllSkillUI();
        
    }

    
}
