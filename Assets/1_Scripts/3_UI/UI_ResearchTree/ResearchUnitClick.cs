using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchUnitClick : Unit
{
    SkillTree_Control skillTreeControl;

    void Start()
    {
        skillTreeControl = GetComponent<SkillTree_Control>();
    }
    void Update()
    {
        ListenInput();
    }

    private void ListenInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == GetComponent<BoxCollider>())
                    {
                        if (skillTreeControl == null)
                        {
                            skillTreeControl = FindObjectOfType<SkillTree_Control>();
                        }
                        //if (TurnUnit == false)
                        //{
                        //    skillTreeControl.OpenSkillTree();
                        //}
                        //else
                            skillTreeControl.OpenResearchBuilding();
                    }
                        
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == GetComponent<BoxCollider>())
                {
                    if (skillTreeControl == null)
                    {
                        skillTreeControl = FindObjectOfType<SkillTree_Control>();
                    }
                    //if (TurnUnit == false)
                    //{
                    //    skillTreeControl.OpenSkillTree();
                    //}
                    //else
                        skillTreeControl.OpenResearchBuilding();
                }
                
                
            }
        }
    }

}
