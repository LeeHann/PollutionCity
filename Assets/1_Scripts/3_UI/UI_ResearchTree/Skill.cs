using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UI_ResearchTree;
public class Skill : Unit
{
    ResourceType resourceType;
    HexCell cell;
    HexCell cell;
    HexCell cell;

public class Skill : MonoBehaviour
{
    [SerializeField] UI_ResearchTree skilltree;
    public int id;
    public RSUnit unit {
        get { return skillTree_Control.unit; }
        set { skillTree_Control.unit = value;}
    }

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;
    public Button UpgradePaperBtn;
    public Button UpgradeCanBtn;
    public Button UpgradeGlassBtn;
    public Button UpgradePlasticBtn;

    [SerializeField] Image m_Image;


    public City city
    {
    City _City;


    public void UpdateUI()
    {
        TitleText.text = $"{skilltree.SkillLevels[id]}/{skilltree.SkillCaps[id]}" +        //스킬트리 레벨 & 최대치
            $"\n{skilltree.SkillNames[id]}";                                                //스킬이름

        DescriptionText.text = $"{""}";//$"{skilltree.SkillDescription[id]}\n{"턴당한번만 가능합니다"}";

        m_Image.color = skilltree.SkillLevels[id] >= skilltree.SkillCaps[id] ? 
                        Color.white   // 레벨max다다르면 흰색
                        : Color.green; // 연구 가능 초록

        foreach(var connectedSkill in ConnectedSkills)
        {
            skilltree.SkillList[connectedSkill].gameObject.SetActive(skilltree.SkillLevels[id] > 0);
            skilltree.ConnectorList[connectedSkill].SetActive(skilltree.SkillLevels[id] > 0);
        }
    }

    public void UnLock()        // 일쓰 & 재활용 버튼 클릭용도
    {
        if (skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.SkillLevels[id]++;
        skilltree.UpdateAllSkillUI();
    }

    public void UnLock_Environment_Paper()       
    {
        if (skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])//&& ActionResearcher(action) = 0
            return;

        skilltree.SkillLevels[id]++;
        if(skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradePaperBtn.interactable = false;
        }

        city.UpdateResearch(2);
        skilltree.UpdateAllSkillUI();
        unit.TurnUnit = false;
        unit = null;
        skillTree_Control.CloseSkillTree();
    }

    public void UnLock_Environment_Can()
    {
        if (skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.SkillLevels[id]++;
        if (skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradeCanBtn.interactable = false;
        }

        city.UpdateResearch(3);
        skilltree.UpdateAllSkillUI();
        unit.TurnUnit = false;
        unit = null;
        skillTree_Control.CloseSkillTree();
    }

    public void UnLock_Environment_Glass() 
    {
        if (skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.SkillLevels[id]++;
        if (skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradeGlassBtn.interactable = false;
        }

        city.UpdateResearch(4);
        skilltree.UpdateAllSkillUI();
        unit.TurnUnit = false;
        unit = null;
        skillTree_Control.CloseSkillTree();
    }

    public void UnLock_Environment_Plastic() 
    {
        if (skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.SkillLevels[id]++;
        if (skilltree.SkillLevels[id] == skilltree.SkillCaps[id])
        {
            UpgradePlasticBtn.interactable = false;
        }

        city.UpdateResearch(5);
        skilltree.UpdateAllSkillUI();
        unit.TurnUnit = false;
        unit = null;
        skillTree_Control.CloseSkillTree();
    }

    public void PollutionLevelUp()  
    {
        if (skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.SkillLevels[id]++;
    
        if (skilltree.SkillLevels[id] == 1)
        {
            city.PA -= (int)(city.PA * 0.01165f);
        }
        else if (skilltree.SkillLevels[id] == 2)
        {
            city.PA -= (int)(city.PA * 0.00720f);
        }
         else if (skilltree.SkillLevels[id] == 3)
        {
            city.PA   -= (int)(city.PA * 0.00445f);
        }
        else if (skilltree.SkillLevels[id] == 4)
        {
            city.PA -= (int)(city.PA * 0.00275f);
        }
        else if (skilltree.SkillLevels[id] == 5)
        {
            city.PA -= (int)(city.PA * 0.00170f);
        }
        else if (skilltree.SkillLevels[id] == 6)
        {
            city.PA -= (int)(city.PA * 0.00105f);
        }
        else if (skilltree.SkillLevels[id] == 7)
        {
            city.PA -= (int)(city.PA * 0.00065f);
        }
        else if (skilltree.SkillLevels[id] == 8)
        {
            city.PA -= (int)(city.PA * 0.00040f);
        }
        else if (skilltree.SkillLevels[id] == 9)
        {
            city.PA -= (int)(city.PA * 0.00025f);
        }
        else if (skilltree.SkillLevels[id] == 10)
        {
            city.PA -= (int)(city.PA * 0.00015f);
        }
        else if (skilltree.SkillLevels[id] == 11)
        {
            city.PA -= (int)(city.PA * 0.00010f);
        }
        else if (skilltree.SkillLevels[id] == 12)
        {
            city.PA -= (int)(city.PA * 0.00005f);
        }
        else if (skilltree.SkillLevels[id] == 13)
        {
            city.PA -= (int)(city.PA * 0.00005f);
        }
        else
        {
            city.PA -= (int)(city.PA * 0.00003f);
        }
        skilltree.UpdateAllSkillUI();
        unit.TurnUnit = false;
        unit = null;
        skillTree_Control.CloseSkillTree();
    }

    public void OpenResBuild()
    {
        skilltree.UpdateAllSkillUI();
    }

}
