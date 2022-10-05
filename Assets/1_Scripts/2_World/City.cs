using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public PlayerSit Sit {
        get {
            return sit;
        }
        private set {
            sit = value;
        }
    }

    private PlayerSit sit;
    public int Money {
        get {
            return money;
        }
        set{
            money = value;
        }
    }
    private int money;

    private void Start() 
    {
    }
}
