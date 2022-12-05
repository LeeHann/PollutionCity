using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree_Control : MonoBehaviour
{
    [HideInInspector] public RSUnit unit;
    public GameObject Skilltree;
    public GameObject ConnectorHolder;
    public GameObject SkillHolder;
    public GameObject BackGround;
    public GameObject Panel;

    //�������ִ����� �̿�
    public void OpenSkillTree()
    {
        Skilltree.SetActive(true);
        ConnectorHolder.SetActive(true);
        SkillHolder.SetActive(true);
        BackGround.SetActive(true);
        Panel.SetActive(true);
    }

    public void CloseSkillTree()
    {
        Skilltree.SetActive(false);
        ConnectorHolder.SetActive(false);
        SkillHolder.SetActive(false);
        BackGround.SetActive(false);
        Panel.SetActive(false);
    }

    public void OpenResearchBuilding()
    {
        Skilltree.SetActive(true);
        ConnectorHolder.SetActive(true);
        SkillHolder.SetActive(true);
        BackGround.SetActive(true);
        Panel.SetActive(false);
    }
}
