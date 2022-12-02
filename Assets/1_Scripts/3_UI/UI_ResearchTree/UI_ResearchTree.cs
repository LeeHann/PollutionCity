using System.Collections.Generic;
using UnityEngine;

public class UI_ResearchTree : MonoBehaviour
{
    public int[] SkillLevels;
    public int[] SkillCaps; //스킬들의 최댓값
    public string[] SkillNames;
    public string[] SkillDescription;

    public List<Skill> SkillList = new List<Skill>();
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    private void Start()
    {
        SkillLevels = new int[8];
        SkillCaps = new[] { 1, 30, 30, 1, 1, 1, 1, 1 };   //각업그레이드마다 최대 업그레이드 값

        SkillNames = new[] { "일반쓰레기", "소각", "매립", "재활용", "종이", "캔", "유리", "플라스틱",};          //스킬이름
        SkillDescription = new[]                                                                            //스킬설명
        {
            "",
            "턴당 오염도 감소",
            "턴당 오염도 감소",
            "해금 요소",
            "종이 해금",
            "캔 해금",
            "유리 해금",
            "플라스틱 해금",
        };

        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);        //ㅁ - ㅁ 선

        for (var i = 1; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2 };        //일쓰랑 연결된 => 소각 , 매립
        SkillList[3].ConnectedSkills = new[] { 4, 5 ,6, 7};      //재활용이랑 열결된 종이 유리 캔..

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }
}
