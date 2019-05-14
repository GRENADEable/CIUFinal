using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : Node
{
    public override void Execute()
    {
        base.Execute();
        Waypoints();
        state = Node_State.running;
    }

    public void Waypoints()
    {
        if (BT.timeLeft > 0)
        {
            BT.anim.SetBool("Approach", true);
        }
        else
            BT.anim.SetBool("Approach", false);

        BT.anim.SetBool("attacking", false);
        BT.attacking = false;
        if (!BT.anim.GetCurrentAnimatorStateInfo(0).IsName("FinalBossApproach"))
            BT.transform.position = Vector3.MoveTowards(BT.transform.position, BT.waypoints[BT.waypointTarget].transform.position, BT.wanderSpeed);

        if (Vector3.Distance(BT.transform.position, BT.waypoints[BT.waypointTarget].transform.position) < 0.1)
        {
            BT.waypointTarget = Random.Range(0, BT.waypoints.Length - 1);
            if (BT.waypointTarget == BT.waypoints.Length)
            {

                Debug.Log("patrol" + state);
            }
        }
        else
        {
            state = Node_State.faliure;
        }
    }
}