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
