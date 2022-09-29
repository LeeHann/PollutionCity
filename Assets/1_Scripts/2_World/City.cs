using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int Money {
        get {
            return money;
        }
        set{
            money = value;
        }
    }
    private int money;

}
