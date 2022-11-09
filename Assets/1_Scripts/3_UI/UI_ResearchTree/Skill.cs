using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UI_ResearchTree;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skilltree.SkillLevels[id]}/{skilltree.SkillCaps[id]}" +        //��ųƮ�� ���� & �ִ�ġ
            $"\n{skilltree.SkillNames[id]}";                                                //��ų�̸�

        DescriptionText.text = $"{skilltree.SkillDescription[id]}\nCost : {skilltree.Money}/1 Money";

        GetComponent<Image>().color = skilltree.SkillLevels[id] >= skilltree.SkillCaps[id] ? Color.yellow   //����max�ٴٸ��� �����
            : skilltree.Money >= 1 ? Color.green : Color.white;                                             //��ȭ������ �ʷ� �ٽ����� ������� ǥ��

        foreach(var connectedSkill in ConnectedSkills)
        {
            skilltree.SkillList[connectedSkill].gameObject.SetActive(skilltree.SkillLevels[id] > 0);
            skilltree.ConnectorList[connectedSkill].SetActive(skilltree.SkillLevels[id] > 0);
        }

    }

    public void Buy()
    {
        if (skilltree.Money < 1 || skilltree.SkillLevels[id] >= skilltree.SkillCaps[id])
            return;

        skilltree.Money -= 1;
        skilltree.SkillLevels[id]++;
        skilltree.UpdateAllSkillUI();
    }
}
