using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFCSystem : MonoBehaviour
{   // deal global MFC contents

    MFCUnit unit;
    public MFCItem[] mFCItems;
    [SerializeField] UINoticer noticer;
    private static bool m_ShutDown = false;
    private static object m_Lock = new object();

    private static MFCSystem instance;

    public static MFCSystem Instance {
        get {
            if (m_ShutDown) {
                return null;
            }

            lock (m_Lock) {
                if (instance == null) {
                    instance = (MFCSystem)FindObjectOfType(typeof(MFCSystem));
                    
                    if (instance == null) {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<MFCSystem>();
                        singletonObject.name = "MFCSystem Singleton";
                    }
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
    }
    private void OnApplicationQuit() {
        m_ShutDown = true;
    }

    public void SetUnit(MFCUnit unit)
    {
        this.unit = unit;
    }
    
    public void OnClickMFC(MFCItem item)
    {Debug.Log("Clicked!");
        TurnSystem.turnCity.UpdateTrash(item.input, -item.inputCnt);
        TurnSystem.turnCity.UpdateTrash(ResourceType.Money, item.getMoney);
        noticer.Notice("제조를 수행합니다.");
        MFCUI.Instance.UIClose();
    }
}
