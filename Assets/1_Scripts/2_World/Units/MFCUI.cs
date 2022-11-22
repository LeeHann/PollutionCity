using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MFCUI : MonoBehaviour
{
    MFCUnit unit = null;
    [SerializeField] GameObject ui, bg;
    [SerializeField] Button[] MFCBtns;

    private static bool m_ShutDown = false;
    private static object m_Lock = new object();

    private static MFCUI instance;

    public static MFCUI Instance {
        get {
            if (m_ShutDown) {
                return null;
            }

            lock (m_Lock) {
                if (instance == null) {
                    instance = (MFCUI)FindObjectOfType(typeof(MFCUI));
                    
                    if (instance == null) {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<MFCUI>();
                        singletonObject.name = "MFCUI Singleton";
                    }
                    // DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
    }
    private void OnApplicationQuit() {
        m_ShutDown = true;
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
