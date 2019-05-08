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
    public void Detecting()
    {
        if (BT.player.GetComponent<PlayerControlsTestKoosa>().running  && BT.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("CourageRun"))
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
        Vector3 pos = new Vector3(BT.transform.position.x, BT.transform.position.y, BT.distractObject.transform.position.z);
        BT.transform.position = Vector3.MoveTowards(BT.transform.position, pos, BT.wanderSpeed);
        state = Node_State.success;
    }
}
