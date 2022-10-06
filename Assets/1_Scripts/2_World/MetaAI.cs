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
            if (state == GameState.Common)
            {
                // 일반 사운드
            }
            else if (state == GameState.Warning)
            {
                // 경고 사운드
            }
        }
    }
    private static GameState state = GameState.Common;
}
