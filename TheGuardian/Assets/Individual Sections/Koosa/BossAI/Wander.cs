using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : Node
{

    public override void Execute()
    {
        base.Execute();
        BT.timer += Time.deltaTime;

        if (BT.timer >= BT.wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(BT.transform.position, BT.wanderRadius, -1);
            BT.agent.SetDestination(newPos);
            BT.timer = 0;
        }
        state=Node_State.running;
    }
    
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    
}
