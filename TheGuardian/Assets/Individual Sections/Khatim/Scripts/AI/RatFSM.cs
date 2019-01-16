using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RatFSM : MonoBehaviour
{
    [Header("Rat Variables")]
    public float attackDistance;
    public float chaseDistance;
    public float closeDistance;
    public float distanceToPlayer;
    // public float attackDelay;
    private Vector3 ratMagnitude;
    //public float maxJumpCooldownTime;
    public float ratSpeed;
    [Header("Rat FoV")]
    public float fov;
    [SerializeField]
    private float angle;
    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;
    public GameObject deathScreen;

    // [SerializeField]
    // private bool isAttacking;
    [SerializeField]
    private GameObject player;
    private int currCondition;
    private int wanderCondition = 1;
    private int chaseCondition = 2;
    private int attackCondition = 3;
    private NavMeshAgent ratAgent;
    private Transform target;
    private Vector3 tarDir;
    private float timer;
    private float attackTime;
    private Animator ratAnim;

    void Start()
    {
        deathScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        // isAttacking = false;
        ratAgent = GetComponent<NavMeshAgent>();
        ratAgent.speed = ratSpeed;
        ratAnim = GetComponentInChildren<Animator>();
        timer = maxWanderTimer;
    }

    void Update()
    {
        //Distance check to Player
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        tarDir = player.transform.position - this.transform.position;
        angle = Vector3.Angle(this.tarDir, this.transform.forward);
        ratMagnitude = ratAgent.velocity;

        if (distanceToPlayer > chaseDistance && currCondition != wanderCondition)
        {
            //Wander
            currCondition = 1;
        }

        if ((distanceToPlayer < chaseDistance && angle < fov || distanceToPlayer < closeDistance) && currCondition != chaseCondition)
        {
            //Chase Player
            currCondition = 2;
        }

        if (distanceToPlayer < attackDistance && currCondition != attackCondition && player != null)
        {
            //Attack Player
            currCondition = 3;
        }

        if (ratAgent.velocity.magnitude < 0.1f)
        {
            ratAnim.SetBool("isIdle", true);
            // Debug.LogWarning("Idle");
        }
        if (ratAgent.velocity.magnitude > 0.2f)
        {
            ratAnim.SetBool("isIdle", false);
            // Debug.LogWarning("Not Idle");
        }

        if (!player.activeInHierarchy)
        {
            currCondition = 1;
            deathScreen.SetActive(true);
        }

        // if (isAttacking)
        // {
        //     attackTime += Time.deltaTime;

        //     if (attackTime >= attackDelay)
        //     {
        //         //Attack
        //         attackTime = 0;
        //         Debug.LogWarning("Attacking Player");
        //     }
        // }
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
                Debug.LogWarning("Chasing Player");
                // isAttacking = false;
                break;

            case 3: //Attack Condition
                // isAttacking = true;
                player.SetActive(false);
                Debug.LogWarning("Attacking");
                break;

            case 4: //Wait Condition
                break;

            case 5: //Null Condition
                break;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        //Sets a random position inside the sphere and that is multiplied with the distance and the center of the sphere.
        Vector3 randomPos = Random.insideUnitSphere * dist;

        //Vector 3 position is returned to the origin parameter.
        randomPos += origin;

        NavMeshHit hit;

        //Bool check if the random position is suitable on the navmesh. If true, then return the hit position.
        NavMesh.SamplePosition(randomPos, out hit, dist, layermask);

        return hit.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
