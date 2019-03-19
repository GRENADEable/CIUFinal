using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DollsFSM : MonoBehaviour
{
    [Header("Doll Variables")]
    public float dollSpeed;
    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;

    private NavMeshAgent dollAgent;
    private Animator dollAnim;
    private int currCondition;
    private float timer;

    void Start()
    {
        dollAgent = GetComponent<NavMeshAgent>();
        dollAgent.speed = dollSpeed;
        dollAnim = GetComponent<Animator>();
        timer = maxWanderTimer;
    }

    void Update()
    {
        if (dollAgent.velocity.magnitude < 0.1f)
        {
            dollAnim.SetBool("isIdle", true);
            Debug.LogWarning("Idle");
        }

        if (dollAgent.velocity.magnitude > 0.2f)
        {
            dollAnim.SetBool("isIdle", false);
            Debug.LogWarning("Not Idle");
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= maxWanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            dollAgent.SetDestination(newPos);
            timer = 0;
        }
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
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
