using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MFCSystem : MonoBehaviour
{   // deal global MFC contents

    private static MFCSystem _instance = null;
    public static MFCSystem Instance
    {
        get {
            if (_instance == null)
            {
                _instance = (MFCSystem)FindObjectOfType(typeof(MFCSystem));
            
                if (_instance == null) {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<MFCSystem>();
                    singletonObject.name = typeof(MFCSystem).ToString() + " (Singleton)";
                }
            }
            return _instance; 
        }
    }
    

}
