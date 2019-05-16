using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBlockerFSM : MonoBehaviour
{
    [Header("Rat Variables")]
    public float attackDistance;
    public float chaseDistance;
    public float fleeDistance;
    public float fleeLocation;
    public float distanceToPlayer;
    public float distanceToFleePos;
    public Transform fleePos;
    public float ratSpeed;
    public float fleeSpeed;

    public delegate void SendDeathMessage();
    public static event SendDeathMessage onPlayerDeath;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool isFleeing;
    private enum ratState { Idle, Chase, Flee, Attack };
    private ratState currCondition;
    private NavMeshAgent ratAgent;
    private Animator ratAnim;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ratAgent = GetComponent<NavMeshAgent>();
        ratAgent.speed = ratSpeed;
        ratAnim = GetComponent<Animator>();
        currCondition = ratState.Idle;

        LightMechanic.onFleeEnemy += FleeEventReceived;
        LightMechanic.onChasePlayer += ChaseEventReceived;
    }

    void OnDisable()
    {
        LightMechanic.onFleeEnemy -= FleeEventReceived;
        LightMechanic.onChasePlayer -= ChaseEventReceived;
    }

    void OnDestroy()
    {
        LightMechanic.onFleeEnemy -= FleeEventReceived;
        LightMechanic.onChasePlayer -= ChaseEventReceived;
    }

    void Update()
    {
        //Distance check to Player
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        distanceToFleePos = Vector3.Distance(transform.position, fleePos.transform.position);

        Debug.DrawRay(transform.position, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.red);

        ratAnim.SetFloat("speed", ratAgent.velocity.magnitude);

        if (distanceToFleePos <= fleeLocation)
        {
            chaseDistance = 0;
            currCondition = ratState.Idle;
            // Debug.Log("Rat Fleed");
        }

        if (isFleeing)
            currCondition = ratState.Flee;

        switch (currCondition)
        {
            case ratState.Idle:
                if (distanceToPlayer < chaseDistance && !isFleeing)
                    currCondition = ratState.Chase;

                break;

            case ratState.Chase:
                ratAgent.SetDestination(player.transform.position);
                ratAnim.SetBool("isAttacking", false);
                // Debug.Log("Chasing");

                if (distanceToPlayer > chaseDistance && !isFleeing)
                    currCondition = ratState.Idle;

                if (distanceToPlayer < attackDistance && player.activeInHierarchy && !isFleeing)
                    currCondition = ratState.Attack;
                break;

            case ratState.Attack:
                ratAnim.SetBool("isAttacking", true);
                Vector3 target = player.transform.position;
                target.y = transform.position.y;
                transform.LookAt(target);

                if (distanceToPlayer >= attackDistance && player.activeInHierarchy)
                    currCondition = ratState.Chase;
                break;

            case ratState.Flee:
                ratAgent.SetDestination(fleePos.transform.position);
                // Debug.Log("Fleeing");

                if (!isFleeing)
                    currCondition = ratState.Chase;
                break;
        }
    }

    void FleeEventReceived()
    {
        if (distanceToPlayer < fleeDistance)
            isFleeing = true;
    }

    void ChaseEventReceived()
    {
        isFleeing = false;
    }

    void OnKillPlayerEventReceived()
    {
        if (distanceToPlayer < attackDistance)
        {
            if (onPlayerDeath != null)
                onPlayerDeath();
        }
    }
}
