using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{

    public float speed;
    public Animator animator;





    public override void Execute()
    {
        base.Execute();
        CheckForAttack();
    }


    public void CheckForAttack()
    {
        if (Vector3.Distance(BT.transform.position, BT.player.transform.position) < 3)
        {
            // BT.enemy.SetInteger("enemyanm", 3);
            BT.playerSpotted = false;
            BT.attacking = true;
            Debug.Log("attack" + state);
            state = Node_State.success;
            KillPlayer();
            BT.chaseSpeed = 0;
        }
        else
        {
            // BT.playerspotted = true;
            BT.attacking = false;
            state = Node_State.faliure;
            BT.chaseSpeed = BT.movespeed;
        }
    }
    public void KillPlayer()
    {
        //playerdeath
    }
    public void DamageBoss()
    {
        BT.health--;
        //increase the speed of attack animations as health decreases
    }
    public void Attacking()
    {
       //place attack animation here
    }
}
