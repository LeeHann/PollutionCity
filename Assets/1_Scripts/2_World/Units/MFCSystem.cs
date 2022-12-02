using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFCSystem : MonoBehaviour
{   // deal global MFC contents

    MFCUnit unit;
    public MFCItem[] mFCItems;
    [SerializeField] UINoticer noticer;

    private static MFCSystem instance;

    public static MFCSystem Instance {
        get {
            return instance;
        }
    }

    private void Start() 
    {
        instance = this;
    }

    public void SetUnit(MFCUnit unit)
    {
        this.unit = unit;
    }
    
    public void OnClickMFC(MFCItem item)
    {
        TurnSystem.turnCity.UpdateTrash(item.input, -item.inputCnt);
        TurnSystem.turnCity.Money += item.getMoney;
        noticer.Notice("제조를 수행합니다.");
        MFCUI.Instance.UIClose();
    }
}
