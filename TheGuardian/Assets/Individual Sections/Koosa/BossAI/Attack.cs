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
        if (Vector3.Distance(BT.transform.position, BT.player.transform.position) < 0.8 && BT.health > 0)
        {
            BT.anim.SetBool("Approach", false);
            BT.playerSpotted = true;
            BT.attacking = true;
            Debug.Log("attack" + state);
            state = Node_State.success;
            BT.anim.Play("FinalBossAttack");
            BT.chaseSpeed = 0;
            BT.anim.SetBool("attacking", true);
        }

        else if ( BT.distractObject != null && Vector3.Distance(BT.transform.position, BT.distractObject.transform.position) < 0.8 && BT.distraction && BT.health > 0)
        {
            //play attack animation
            BT.anim.SetBool("Approach", false);
            state = Node_State.success;
            BT.attacking = true;
            Debug.Log("attack" + state);
            //BT.anim.Play("FinalBossAttack");
            BT.anim.SetBool("attacking", true);
        }
        else
        {
            BT.anim.SetBool("attacking", false);
            BT.attacking = false;
            state = Node_State.faliure;
            BT.chaseSpeed = BT.movespeed;
            Debug.Log("attack" + state);

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
