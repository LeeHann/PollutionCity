using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAI : MonoBehaviour
{
    public static GameState State {
        get {
            return state;
        }
        set {
            if (state == value) return;
            state = value;
            BGSound.instance.ChangeBGM();
        }
    }
    private static GameState state = GameState.Common;

    public void onclickstate()
    {
        if (State == GameState.Common)
            State = GameState.Warning;
        else State = GameState.Common;
    }
}
