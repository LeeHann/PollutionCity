using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MFCUI : MonoBehaviour
{
    MFCUnit unit = null;
    [SerializeField] GameObject ui, bg;
    [SerializeField] Button[] MFCBtns;

    private static MFCUI instance;

    public static MFCUI Instance {
        get {
                return instance;
        }
    }

    private void Start() {
        instance = this;
    }

    public void UIOpen()
    {
        MFCSystem mFCSystem = MFCSystem.Instance;
        for (int i=0; i<MFCBtns.Length; i++)
        {
            if (
                TurnSystem.turnCity.GetTrash(mFCSystem.mFCItems[i].input) >= 
                mFCSystem.mFCItems[i].inputCnt
            )
            {
                MFCBtns[i].interactable = true;
            }
            else MFCBtns[i].interactable = false;
        }
    
        ui.SetActive(true);
        bg.SetActive(true);
    }

    public void SetUnit(MFCUnit unit)
    {
        this.unit = unit;
    }

    public void UIClose()
    {
        this.unit = null;
        ui.SetActive(false);
        bg.SetActive(false);
    }

}
