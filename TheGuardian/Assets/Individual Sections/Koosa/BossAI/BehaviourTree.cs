using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public Node root;
    public GameObject player;
    public float chaseSpeed;
    public float movespeed;
    public bool attacking;
    public bool playerSpotted;
    public float health;
    public RaycastHit hit;
    public float angle;
    public float maxDistance;
    public float vision;
    public bool distraction;
    public Transform target;
    public NavMeshAgent agent;
    public float timer;

    public void Start()
    {
        SelectorNode selector = new SelectorNode();
        root = selector;

        SequenceNode sequence = new SequenceNode();
        selector.children.Add(sequence);
        sequence.children.Add(new Chase());
        sequence.children.Add(new Attack());
        selector.children.Add(new Wander());
        root.BT = this;
        root.Start();
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }
    private void Update()
    {
        root.Execute();
    }

    void OnDrawGizmos()
    {
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, vision);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, angle);
        }

    }
}
