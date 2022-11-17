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
    public int[] SkillCaps; //스킬들의 최댓값
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
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10, 5 };   //각업그레이드마다 최대 업그레이드 값

        SkillNames = new[] { "일반쓰레기", "소각", "매립", "재활용", "종이류", "캔류", "유리류",};          //스킬이름
        SkillDescription = new[]                                                                            //스킬설명
        {
            "설명",
            "턴당 오염도 감소",
            "금화",
            "해금 요소",
            "종이류 해금",
            "캔류 해금",
            "유리류 해금",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);                                           //스킬들
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);        //ㅁ - ㅁ 선

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2 };        //일쓰랑 연결된 => 소각 , 매립
        SkillList[3].ConnectedSkills = new[] { 4, 5 ,6};      //재활용이랑 열결된 종이 유리 캔..
     


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
