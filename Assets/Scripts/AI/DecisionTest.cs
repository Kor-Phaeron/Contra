using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decision Test")]
public class DecisionTest : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return LookForInput();   
    }

    private bool LookForInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }

        return false;
    }
}
