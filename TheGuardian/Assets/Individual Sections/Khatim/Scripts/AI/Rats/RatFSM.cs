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
    public float ratSpeed;
    [Header("Rat FoV")]
    public float fov;
    private float angle;
    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;
    public GameObject deathScreen;
    public float baitDuration;
    // [SerializeField]
    // private bool isAttacking;
    [SerializeField]
    private GameObject player;
    private int currCondition;
    private NavMeshAgent ratAgent;
    private Vector3 tarDir;
    private float timer;
    // private float attackTime;
    private Animator ratAnim;
    public GameObject bait;
    [SerializeField]
    private bool isDistracted;

    void OnEnable()
    {
        DistractEnemyEvent.OnDistractEnemy += DistractEventReceived;
    }


    void OnDisable()
    {
        DistractEnemyEvent.OnDistractEnemy -= DistractEventReceived;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        deathScreen.SetActive(false);
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
        Debug.DrawRay(transform.position, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.red);
        tarDir = player.transform.position - this.transform.position;
        angle = Vector3.Angle(this.tarDir, this.transform.forward);

        if (distanceToPlayer > chaseDistance && !isDistracted)
        {
            //Wander
            currCondition = 1;
        }

        if ((distanceToPlayer < chaseDistance && angle < fov || distanceToPlayer < closeDistance) && !isDistracted)
        {
            //Chase Player
            currCondition = 2;
        }

        if (distanceToPlayer < attackDistance && !isDistracted && player != null)
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
            currCondition = 1;
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
                // isAttacking = false;
                break;

            case 3: //Attack Condition
                // isAttacking = true;
                player.SetActive(false);
                deathScreen.SetActive(true);
                // Debug.LogWarning("Attacking");
                break;

            case 4: //Wait Condition
                break;

            case 5: //Distract Condition
                ratAgent.SetDestination(bait.transform.position);
                // Debug.LogWarning("Distracting Enemy");
                break;

            case 6: //Null Condition
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

    void DistractEventReceived()
    {
        DistractEnemy();
        // Debug.LogWarning("Enemy Distracted");
    }

    void DistractEnemy()
    {
        StartCoroutine(Distract());
    }

    IEnumerator Distract()
    {
        isDistracted = true;

        if (isDistracted)
        {
            currCondition = 5;
        }
        yield return new WaitForSeconds(baitDuration);
        isDistracted = false;
    }
}
