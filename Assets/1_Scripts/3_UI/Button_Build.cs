using UnityEngine;

public class Button_Build : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Liv_Pan;
    [SerializeField] GameObject Res_Pan;
    [SerializeField] GameObject Ind_Pan;
    [SerializeField] GameObject Liv_Next_Btn;
    [SerializeField] GameObject Res_Next_Btn;
    [SerializeField] GameObject Ind_Next_Btn;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActivate = Panel.activeSelf;
            
            Panel.SetActive(!isActivate);
        }
    
        Liv_Pan.SetActive(false);
        Res_Pan.SetActive(false);
        Ind_Pan.SetActive(false);
        Liv_Next_Btn.SetActive(true);
        Res_Next_Btn.SetActive(true);
        Ind_Next_Btn.SetActive(true);            
    }

    public void Liv_Next()
    {
        Liv_Pan.SetActive(true);
        Res_Pan.SetActive(false);
        Ind_Pan.SetActive(false);
        Liv_Next_Btn.SetActive(true);
        Res_Next_Btn.SetActive(true);
        Ind_Next_Btn.SetActive(true);
    }

    public void Res_Next()
    {
        Liv_Pan.SetActive(false);
        Res_Pan.SetActive(true);
        Ind_Pan.SetActive(false);
        Liv_Next_Btn.SetActive(true);
        Res_Next_Btn.SetActive(true);
        Ind_Next_Btn.SetActive(true);
    }

    public void Ind_Next()
    {
        Liv_Pan.SetActive(false);
        Res_Pan.SetActive(false);
        Ind_Pan.SetActive(true);
        Liv_Next_Btn.SetActive(true);
        Res_Next_Btn.SetActive(true);
        Ind_Next_Btn.SetActive(true);
    }

    public void ClosePanel()
    {
        Panel.SetActive(false);
    }
}
