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
    public float baitDuration;

    public delegate void SendDeathMessage();
    public static event SendDeathMessage onPlayerDeath;

    [SerializeField]
    private GameObject player;
    private enum ratState { Wander, Chase, Wait, Distract, Attack };
    private ratState currCondition;
    private NavMeshAgent ratAgent;
    private Vector3 tarDir;
    private float timer;
    private Animator ratAnim;
    public GameObject bait;
    [SerializeField]
    private bool isDistracted;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ratAgent = GetComponent<NavMeshAgent>();
        ratAgent.speed = ratSpeed;
        ratAnim = GetComponentInChildren<Animator>();
        timer = maxWanderTimer;
        currCondition = ratState.Wander;

        DistractEnemyEvent.OnDistractEnemy += DistractEventReceived;
        Broadcaster.onKillPlayerEvent += OnKillPlayerEventReceived;
    }

    void OnDisable()
    {
        DistractEnemyEvent.OnDistractEnemy -= DistractEventReceived;
        Broadcaster.onKillPlayerEvent -= OnKillPlayerEventReceived;
    }

    void OnDestroy()
    {
        DistractEnemyEvent.OnDistractEnemy -= DistractEventReceived;
        Broadcaster.onKillPlayerEvent -= OnKillPlayerEventReceived;
    }

    void Update()
    {
        //Distance check to Player
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        Debug.DrawRay(transform.position, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.red);
        tarDir = player.transform.position - this.transform.position;
        angle = Vector3.Angle(this.tarDir, this.transform.forward);

        ratAnim.SetFloat("speed", ratAgent.velocity.magnitude);

        switch (currCondition)
        {
            case ratState.Wander:
                ratAnim.SetBool("isAttacking", false);
                Wander();
                if ((distanceToPlayer <= chaseDistance && angle < fov || distanceToPlayer < closeDistance) && !isDistracted && player.activeInHierarchy)
                    currCondition = ratState.Chase;
                break;

            case ratState.Chase:
                ratAnim.SetBool("isAttacking", false);
                ratAgent.SetDestination(player.transform.position);
                Debug.Log("Chasing Player");

                if (distanceToPlayer >= chaseDistance && !isDistracted)
                    currCondition = ratState.Wander;

                if (distanceToPlayer <= attackDistance && !isDistracted && player.activeInHierarchy)
                    currCondition = ratState.Attack;
                break;

            case ratState.Attack:
                ratAnim.SetBool("isAttacking", true);
                if (!player.activeInHierarchy)
                    currCondition = ratState.Wander;

                if (distanceToPlayer >= attackDistance && !isDistracted && player.activeInHierarchy)
                    currCondition = ratState.Chase;
                break;

            case ratState.Distract:
                ratAgent.SetDestination(bait.transform.position);
                Debug.Log("Distracting Enemy");
                break;
        }
    }

    static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
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

    void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= maxWanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            ratAgent.SetDestination(newPos);
            timer = 0;
        }
        // Debug.Log("Wandering");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }

    // void DistractEventReceived()
    // {
    //     DistractEnemy();
    // }

    void DistractEventReceived()
    {
        StartCoroutine(Distract());
    }

    IEnumerator Distract()
    {
        isDistracted = true;

        if (isDistracted)
            currCondition = ratState.Distract;

        yield return new WaitForSeconds(baitDuration);
        isDistracted = false;
        currCondition = ratState.Wander;
    }

    void OnKillPlayerEventReceived()
    {
        if (distanceToPlayer < attackDistance)
        {
            // player.SetActive(false);
            // Debug.Log("Attacking Player");

            if (onPlayerDeath != null)
                onPlayerDeath();
        }
    }
}
