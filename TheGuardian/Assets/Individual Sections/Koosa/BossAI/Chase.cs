using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{
    public override void Execute()
    {
        base.Execute();
        Detecting();
    }
    public void Detecting()
    {
        if (BT.distanceToPlayer < BT.distanceForPlayerToBeSpotted)
            BT.playerSpotted = true;
        else
            BT.playerSpotted = false;

        if ((BT.player.GetComponent<PlayerControls>().running && BT.distanceToPlayer < BT.distanceForPlayerToBeSpotted) || BT.playerSpotted)
        {
            BT.playerSpotted = true;
            Chasing();
            if (BT.distanceToPlayer <= BT.distanceToAttackPlayer)
            {
                state = Node_State.success;
            }
        }
        else if (BT.distraction)
        {
            Distracted();
            if (BT.distraction && Vector3.Distance(BT.transform.position, BT.distractObject.transform.position) < BT.distanceToAttackDistractObject)
                state = Node_State.success;
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
