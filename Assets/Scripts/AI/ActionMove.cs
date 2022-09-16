using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Move", fileName = "Action Move")]
public class ActionMove : AIAction
{
    public override void Act(StateController controller)
    {
        controller.transform.Translate(Vector3.left * 4f * Time.deltaTime);
    }
}
