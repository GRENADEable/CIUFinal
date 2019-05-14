using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree : MonoBehaviour
{
    public Node root;
    public GameObject player;
    public float chaseSpeed;
    public float movespeed;
    public bool attacking;
    public bool playerSpotted;
    public float health;
    public bool distraction;
    public Transform target;
    public float timer;
    public Vector3 startPos;
    public float wanderDelta;
    public float wanderSpeed;
    public GameObject distractObject;
    public int waypointTarget;
    public GameObject[] waypoints;
    public Animator anim;
    public float timeLeft = 30;
    public Animator playerAnim;
    // public AnimationClip approach;
    public bool approaching;
    public void Start()
    {
        playerAnim = player.gameObject.GetComponent<Animator>();
        approaching = false;
        startPos = transform.position;
        SelectorNode selector = new SelectorNode();
        root = selector;

        SequenceNode sequence = new SequenceNode();
        selector.children.Add(sequence);
        sequence.children.Add(new Chase());
        sequence.children.Add(new Attack());
        selector.children.Add(new Wander());
        root.BT = this;
        root.Start();
    }
    public void Update()
    {
        timeLeft -= Time.deltaTime;
        if (distractObject != null || distraction)
        {
            if (!distractObject.activeSelf || distractObject == null || health <= 0)
            {
                distractObject = null;
                distraction = false;
            }
        }
        if (health <= 0)
        {
            attacking = false;
            anim.SetBool("Approach", false);
            anim.SetBool("injury", false);
            anim.SetBool("attacking", false);
            anim.SetBool("Retreating", true);
        }
        root.Execute();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "PickUp" && (other.gameObject.activeSelf || other.gameObject != null) && health > 0)
        {
            distraction = true;
            distractObject = other.gameObject;
        }
        else if(other.gameObject.tag == "Player" && health > 0)
        {
            playerSpotted = true;
        }
        else
        {
            distractObject = null;
            distraction = false;
        }
    }
    public void KillPlayer()
    {
        anim.Play("FinalBossAttack");
        player.gameObject.SetActive(false);
    }
    public void DamageBoss()
    {
        anim.Play("FinalBossInjury");
        // anim.SetBool("attacking", false);
        health--;
        Debug.Log("Damaged");
    }
    public void DestroyDistractionObject()
    {
        distractObject.SetActive(false);
    }
    public void Distraction( GameObject obj)
    {
        if (Vector3.Distance(transform.position, obj.transform.position) < 1.5)
        {


            distraction = true;
            distractObject = obj;
        }
    }

}
