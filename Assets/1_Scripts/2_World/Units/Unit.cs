using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
	Explorer,
	Researcher,
	Manufacturer
}
public class Unit : MonoBehaviour
{
    public UnitType unitType;	
}
