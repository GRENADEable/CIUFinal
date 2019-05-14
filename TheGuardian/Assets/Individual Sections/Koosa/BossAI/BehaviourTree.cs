using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree : MonoBehaviour
{
    [Header("Distance Checks")]
    public float distanceToAttackPlayer; //0.8
    public float distanceToAttackDistractObject;//0.8
    public float distanceForPlayerToBeSpotted; //1
    public float distractionArea; //1.5
    public float distanceToPlayer;

    [Header("Bool Checks")]
    public bool attacking;
    public bool playerSpotted;
    public bool distraction;
    private Node root;
    public GameObject player;
    [Header("Boss Variables")]
    public float health;
    public float wanderSpeed;
    [HideInInspector]
    public GameObject distractObject;
    public int waypointTarget;
    public GameObject[] waypoints;
    public Animator anim;
    public float timeLeft;
    public Animator playerAnim;

    public void Start()
    {
        playerAnim = player.gameObject.GetComponent<Animator>();
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
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

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
        else if (other.gameObject.tag == "Player" && health > 0)
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
        health--;
        Debug.Log("Damaged");
    }
    public void DestroyDistractionObject()
    {
        distractObject.SetActive(false);
    }
    public void Distraction(GameObject obj)
    {
        if (Vector3.Distance(transform.position, obj.transform.position) < distractionArea)
        {
            distraction = true;
            distractObject = obj;
        }
    }
}