using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatAITest : MonoBehaviour
{
    [Header("Rat Variables")]
    public float attackDistance;
    public float chaseDistance;
    public float closeDistance;
    public float distanceToPlayer;
    public float baitDuration;
    [Header("Rat FoV")]
    public float fov;
    private float angle;
    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;
    public GameObject bait;

    private GameObject player;
    [SerializeField]
    private int currCondition;
    // private int wanderCondition = 1;
    // private int chaseCondition = 2;
    // private int attackCondition = 3;
    // private int waitCondition = 4;
    // private int distractCondition = 5;
    private NavMeshAgent ratAgent;
    private Vector3 tarDir;
    private float timer;
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
        ratAgent = GetComponent<NavMeshAgent>();
        timer = maxWanderTimer;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        Debug.DrawRay(transform.position, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.red);

        tarDir = player.transform.position - this.transform.position;
        angle = Vector3.Angle(this.tarDir, this.transform.forward);

        if (distanceToPlayer > chaseDistance && !isDistracted /*&& currCondition != wanderCondition*/)
        {
            //Wander
            currCondition = 1;
        }

        if ((distanceToPlayer < chaseDistance && angle < fov || distanceToPlayer < closeDistance) && !isDistracted /*&& currCondition != chaseCondition*/)
        {
            //Chase Player
            currCondition = 2;
        }

        if (distanceToPlayer < attackDistance /*&& currCondition != attackCondition*/ && player != null)
        {
            //Attack Player
            currCondition = 3;
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
                Debug.LogWarning("Wandering");
                break;

            case 2: //Chase Condition
                ratAgent.SetDestination(player.transform.position);
                Debug.LogWarning("Chasing Player");
                // isAttacking = false;
                break;

            case 3: //Attack Condition
                // isAttacking = true;
                Debug.LogWarning("Attacking");
                break;

            case 4: //Wait Condition
                break;

            case 5: //Distract Condition
                ratAgent.SetDestination(bait.transform.position);
                Debug.LogWarning("Distracting Enemy");
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

        // while (true) //Don't use whie loop without yield return new WaitForSeconds. Or else you are stuck in an infinite loop.
        // {
        if (isDistracted)
        {
            currCondition = 5;
        }
        yield return new WaitForSeconds(baitDuration);
        isDistracted = false;
        // }
    }
}
