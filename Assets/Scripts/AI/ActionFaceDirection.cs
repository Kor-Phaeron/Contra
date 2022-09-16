using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Face Direction", fileName ="ActionFaceDirection")]
public class NewBehaviourScript : AIAction
{
    public override void Act(StateController controller)
    {
        FaceDirection(controller);
    }

    private void FaceDirection(StateController controller)
    {
        if (controller.rb.velocity == new Vector2(1f, controller.rb.velocity.y))
        {
            Debug.Log("Left");
            controller.rb.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (controller.rb.velocity == new Vector2(-1f, controller.rb.velocity.y))
        {
            Debug.Log("Left");
            controller.rb.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
