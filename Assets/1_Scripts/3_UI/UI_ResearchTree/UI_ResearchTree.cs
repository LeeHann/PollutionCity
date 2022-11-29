using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



/*
 Money로 연구하는거 아님 고쳐야함

 Money 사용은 타일구매 , 건물구매만
 한턴에 1연구만 가능하도록 ( 플레이어 턴일때 기능 활성화 되도록 )
 턴에 연구가 끝나면 더이상 연구유닛은 연구 못하도록
 
 
  
  
  
  
 
 */
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
        Money = 100;

        SkillLevels = new int[8];                       //
        SkillCaps = new[] { 1, 30, 30, 1, 1, 1, 1, 1 };   //각업그레이드마다 최대 업그레이드 값

        SkillNames = new[] { "일반쓰레기", "소각", "매립", "재활용", "종이류", "캔류", "유리류", "플라스틱류",};          //스킬이름
        SkillDescription = new[]                                                                            //스킬설명
        {
            "",
            "턴당 오염도 감소",
            "턴당 오염도 감소",
            "해금 요소",
            "종이류 해금",
            "캔류 해금",
            "유리류 해금",
            "플라스틱류 해금",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);                                           //스킬들
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);        //ㅁ - ㅁ 선

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2 };        //일쓰랑 연결된 => 소각 , 매립
        SkillList[3].ConnectedSkills = new[] { 4, 5 ,6, 7};      //재활용이랑 열결된 종이 유리 캔..


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
