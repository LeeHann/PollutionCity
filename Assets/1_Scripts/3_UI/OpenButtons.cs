using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OpenButtons : MonoBehaviour
{
    public Button_Build button_Build;
    BuildSet buildSet;
    public void Res_Next()
    {
        button_Build.Liv_Btn.SetActive(false);
        button_Build.Res_Btn.SetActive(true);
        button_Build.Ind_Btn.SetActive(false);
        button_Build.Res_Next.SetActive(true);
        button_Build.Ind_Next.SetActive(true);


        //for (int i = 0; i <= 2;)
        //{
        //    if (i == 0)
        //    {
        //        Button1.SetActive(true);
        //        Button2.SetActive(false);
        //        Button3.SetActive(false);
        //    }
        //    else if (i == 1)
        //    {
        //        Button2.SetActive(true);
        //        Button1.SetActive(false);
        //        Button3.SetActive(false);
        //    }
        //    else if(i == 2)
        //    {
        //        Button3.SetActive(true);
        //        Button1.SetActive(false);
        //        Button2.SetActive(false);
        //    }
        //    i++;
        //    if(i >=3 )
        //    {
        //        i = 0;
        //    }
        //}
        //buildSet.LivingBuild.SetActive(true);
        //for(int i = 0; i < BuildingLists.Length;)
        //{
        //    if (BuildingLists[i] == null)
        //        Debug.Log("List is Empty");
        //        if (BuildingLists[0])
        //        {
        //            BuildingLists[0].interactable = true;
        //            BuildingLists[1].interactable = false;
        //            BuildingLists[2].interactable = false;
        //            //GetComponent<Button>().interactable = true;
        //        }
        //        else if (BuildingLists[1])
        //        {
        //            BuildingLists[1].interactable = true;
        //            BuildingLists[0].interactable = false;
        //            BuildingLists[2].interactable = false;
        //            // GetComponent<Button>().interactable = true;
        //        }
        //        else if (BuildingLists[2])
        //        {
        //            BuildingLists[2].interactable = true;
        //            BuildingLists[1].interactable = false;
        //            BuildingLists[0].interactable = false;
        //            //GetComponent<Button>().interactable = true;
        //        }
        //        else
        //            i = 0;


        //}

    }

    public void Ind_Next()
    {
        button_Build.Liv_Btn.SetActive(false);
        button_Build.Res_Btn.SetActive(false);
        button_Build.Ind_Btn.SetActive(true);
        button_Build.Res_Next.SetActive(true);
        button_Build.Ind_Next.SetActive(true);
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
