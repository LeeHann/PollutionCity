using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_ResearchTree : MonoBehaviour
{
    public static UI_ResearchTree skilltree;
    private void Awake() => skilltree = this;

    public int[] SkillLevels;
    public int[] SkillCaps; //��ų���� �ִ�
    public string[] SkillNames;
    public string[] SkillDescription;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    public int Money;

    private void Start()
    {
        Money = 20;

        SkillLevels = new int[7];                       //
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10, 5 };   //�����׷��̵帶�� �ִ� ���׷��̵� ��

        SkillNames = new[] { "�Ϲݾ�����", "�Ұ�", "�Ÿ�", "��Ȱ��", "���̷�", "ĵ��", "������",};          //��ų�̸�
        SkillDescription = new[]                                                                            //��ų����
        {
            "����",
            "�ϴ� ������ ����",
            "��ȭ",
            "�ر� ���",
            "���̷� �ر�",
            "ĵ�� �ر�",
            "������ �ر�",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);                                           //��ų��
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);        //�� - �� ��

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2 };        //�Ͼ��� ����� => �Ұ� , �Ÿ�
        SkillList[3].ConnectedSkills = new[] { 4, 5 ,6};      //��Ȱ���̶� ����� ���� ���� ĵ
     


        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
