using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : Node
{
    public override void Execute()
    {
        base.Execute();
        float min = BT.startPos.z;
        float max = BT.startPos.z + 1.3f;
        float max2 = BT.startPos.z - 0.01f;


        //MoveRight(min, max);
        // MoveLeft(min, max2);

        Waypoints();
        // BT.wanderSpeed = Random.Range(0f, 1f);
        // BT.wanderDelta = Random.Range(0f, 1f);
        // BT.startPos.z += BT.wanderDelta * Mathf.Sin(BT.wanderSpeed * Time.time);
        // BT.transform.position = BT.startPos;

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
        // int number = Random.Range(0,4);
        if (!BT.anim.GetCurrentAnimatorStateInfo(0).IsName("FinalBossApproach"))
            BT.transform.position = Vector3.MoveTowards(BT.transform.position, BT.waypoints[BT.waypointTarget].transform.position, BT.wanderSpeed);
        
        if (Vector3.Distance(BT.transform.position, BT.waypoints[BT.waypointTarget].transform.position) < 0.1)
        {
             BT.waypointTarget = Random.Range(0,BT.waypoints.Length-1);
            if (BT.waypointTarget == BT.waypoints.Length)
            {

                Debug.Log("patrol" + state);
            }
        }
        /*
        if (number > 0 && (BT.waypointTarget >= 0 || BT.waypointTarget <= 4))
        {
            BT.waypointTarget++;
            Debug.Log("yes");
        }
        if (number < 0 && (BT.waypointTarget >= 0 || BT.waypointTarget <= 4) && BT.waypointTarget>0)
        {
            Debug.Log("no");
            BT.waypointTarget--;
        }
        */
        else
        {
            state = Node_State.faliure;
        }
    }
}
