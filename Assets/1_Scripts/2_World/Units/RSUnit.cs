using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RSUnit : Unit, IPointerClickHandler
{
    [HideInInspector] public SkillTree_Control skillTree;

    public void OnPointerClick(PointerEventData e)
    {
        skillTree.unit = this;
        skillTree.OpenResearchBuilding();
    }
}
