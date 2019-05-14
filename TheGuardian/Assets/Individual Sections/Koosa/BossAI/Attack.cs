using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    public override void Execute()
    {
        base.Execute();
        CheckForAttack();
    }

    public void CheckForAttack()
    {
        if (BT.distanceToPlayer <= BT.distanceToAttackPlayer && BT.health > 0)
        {
            BT.playerSpotted = true;
            BT.attacking = true;
            // Debug.Log("attack" + state);
            state = Node_State.success;
            BT.anim.SetBool("attacking", true);
        }

        else if (BT.distractObject != null && Vector3.Distance(BT.transform.position, BT.distractObject.transform.position) < BT.distanceToAttackDistractObject && BT.distraction && BT.health > 0)
        {
            state = Node_State.success;
            BT.attacking = true;
            //Debug.Log("attack" + state);
            BT.anim.SetBool("attacking", true);
        }
        else
        {
            BT.anim.SetBool("attacking", false);
            BT.attacking = false;
            state = Node_State.faliure;
            //Debug.Log("attack" + state);
        }
    }
}