using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree_Control : MonoBehaviour
{
    public GameObject Skilltree;
    public GameObject ConnectorHolder;
    public GameObject SkillHolder;
    public GameObject BackGround;
    public GameObject CloseBtn;
    // Start is called before the first frame update

    public void OpenSkillTree()
    {
        Skilltree.SetActive(true);
        ConnectorHolder.SetActive(true);
        SkillHolder.SetActive(true);
        BackGround.SetActive(true);
        CloseBtn.SetActive(true);
    }

    public void CloseSkillTree()
    {
        Skilltree.SetActive(false);
        ConnectorHolder.SetActive(false);
        SkillHolder.SetActive(false);
        BackGround.SetActive(false);
        CloseBtn.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
