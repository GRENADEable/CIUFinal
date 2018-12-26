using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RatFSM : MonoBehaviour
{
    [Header("Rat Variables")]
    public float attackDistance;
    public float chaseDistance;
    public float distanceToPlayer;
    public float attackDelay;
    public float maxJumpCooldownTime;

    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;
    public GameObject player;
    [SerializeField]
    private bool isAttacking;
    private int currCondition;
    private int wanderCondition = 1;
    private int chaseCondition = 2;
    private int attackCondition = 3;
    private NavMeshAgent ratAgent;
    private Transform target;
    private float timer;
    private float attackTime;
    void Start()
    {
        isAttacking = false;
        ratAgent = GetComponent<NavMeshAgent>();
        timer = maxWanderTimer;
    }

    void Update()
    {
        //Distance check to Player
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer > chaseDistance && currCondition != wanderCondition)
        {
            //Wander
            currCondition = 1;
        }

        if (distanceToPlayer < chaseDistance && currCondition != chaseCondition)
        {
            //Chase Player
            currCondition = 2;
        }

        if (distanceToPlayer < attackDistance && currCondition != attackCondition)
        {
            //Attack Player
            currCondition = 3;
        }

        if (isAttacking)
        {
            attackTime += Time.deltaTime;

            if (attackTime >= attackDelay)
            {
                //Attack
                attackTime = 0;
                Debug.LogWarning("Attacking Player");
            }
        }
    }

    void FixedUpdate()
    {
        switch (currCondition)
        {
            case 1: //Wander Condition
                timer += Time.deltaTime;

                if (timer >= maxWanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    ratAgent.SetDestination(newPos);
                    timer = 0;
                }
                // Debug.LogWarning("Wandering");
                break;

            case 2: //Chase Condition
                ratAgent.SetDestination(player.transform.position);
                // Debug.LogWarning("Chasing Player");
                isAttacking = false;
                break;

            case 3: //Attack Condition
                isAttacking = true;
                break;

            case 4: //Null Condition
                break;
        }
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
