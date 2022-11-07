using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Build : MonoBehaviour
{
    //public Button ConstructButton;
    //public HexFeatureManager features;
    //public HexMesh hexMesh;
    //public HexGridChunk hexGridChunk;
    //public HexGrid hexgrid;

    public GameObject Panel;
    public GameObject Liv_Btn;
    public GameObject Res_Btn;
    public GameObject Ind_Btn;
    public GameObject Res_Next;
    public GameObject Ind_Next;
    bool Activate;
    HexGrid hexGrid;
    HexFeatureManager features;
    HexGridChunk gridChunk;
    HexCell cell;
    Vector3 position;



    void isSpecial()
    {

    }

    void Build()
    {
        
    }

    public void OpenPanel()
    {
        Debug.Log("Button Clicked");
        if(Panel != null)
        {
            bool isActivate = Panel.activeSelf;
            
            Panel.SetActive(!isActivate);
        }

        Liv_Btn.SetActive(true);
        Res_Btn.SetActive(false);
        Ind_Btn.SetActive(false);
        Res_Next.SetActive(true);
        Ind_Next.SetActive(true);

        
    }

    public void ClosePanel()
    {
        Panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {

    }
}
