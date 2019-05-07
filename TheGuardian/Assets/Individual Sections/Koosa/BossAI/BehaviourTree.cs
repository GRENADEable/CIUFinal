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
    public Animation attack;
    public void Start()
    {
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
        root.Execute();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp")
        {
            distraction = true;
            distractObject = other.gameObject;
        }
    }
}
