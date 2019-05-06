using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{


    public Vector3 targetDir;

    public override void Execute()
    {
        base.Execute();
        BT.playerSpotted = false;
        Detecting();
    }

    public void Targeting()
    {
        targetDir = (BT.player.transform.position - BT.transform.position).normalized;
        Physics.Raycast(BT.transform.position, targetDir.normalized, out BT.hit);
        BT.angle = Vector3.Angle(targetDir.normalized, BT.transform.forward);
        Debug.DrawRay(BT.transform.position, BT.player.transform.position - BT.transform.position, Color.red);
    }
    public void Detecting()
    {
        Targeting();
        if (BT.player.GetComponent<PlayerControlsTestKoosa>().running)
        {
            BT.playerSpotted = true;
            Chasing();
        }
        else if (BT.distraction)
        {
            Distracted();
        }
        else
            state = Node_State.faliure;
        BT.playerSpotted = false;
    }


    public void Chasing()
    {
        Vector3 pos = new Vector3(BT.transform.position.x, BT.transform.position.y, BT.player.transform.position.z);
        BT.transform.position = Vector3.MoveTowards(BT.transform.position, pos, BT.wanderSpeed);
        state = Node_State.success;
    }

    public void Distracted()
    {
        BT.agent.SetDestination(BT.distractObject.transform.position);
        state = Node_State.success;
    }
}
