using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{


    public Vector3 targetDir;

    public override void Execute()
    {
        base.Execute();
        Detecting();
    }
    public void Detecting()
    {
        if (Vector3.Distance(BT.transform.position, BT.player.transform.position) < 1)
        {
            BT.playerSpotted = true;
        }
        else
            BT.playerSpotted = false;

        


        if (BT.player.GetComponent<PlayerControls>().running || BT.playerSpotted)
        {
            BT.playerSpotted = true;
            Chasing();
            if(Vector3.Distance(BT.transform.position, BT.player.transform.position) < 0.8)
            {
                state = Node_State.success;
            }
        }
        else if (BT.distraction)
        {
            Distracted();
            if (BT.distraction && Vector3.Distance(BT.transform.position, BT.distractObject.transform.position) < 0.8)
            {
                state = Node_State.success;
            }
        }
        else
            state = Node_State.faliure;
        BT.playerSpotted = false;
    }


    public void Chasing()
    {
        Vector3 pos = new Vector3(BT.transform.position.x, BT.transform.position.y, BT.player.transform.position.z);
        BT.transform.position = Vector3.MoveTowards(BT.transform.position, pos, BT.wanderSpeed);
        state = Node_State.running;
    }

    public void Distracted()
    {
        Vector3 pos = new Vector3(BT.transform.position.x, BT.transform.position.y, BT.distractObject.transform.position.z);
        BT.transform.position = Vector3.MoveTowards(BT.transform.position, pos, BT.wanderSpeed);
        state = Node_State.running;
    }
}
