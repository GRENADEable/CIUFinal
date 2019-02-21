using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBlockerFSM : MonoBehaviour
{
    [Header("Rat Variables")]
    public float attackDistance;
    public float chaseDistance;
    public float distanceToPlayer;
    public Transform fleePos;
    public float ratSpeed;
    public GameObject deathScreen;
    [SerializeField]
    private GameObject player;
    private int currCondition;
    private NavMeshAgent ratAgent;
    private Animator ratAnim;


    void OnEnable()
    {
        LightMechanic.OnFleeEnemy += FleeEventReceived;
    }

    void OnDisable()
    {
        LightMechanic.OnFleeEnemy -= FleeEventReceived;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (deathScreen != null)
            deathScreen.SetActive(false);

        ratAgent = GetComponent<NavMeshAgent>();
        ratAgent.speed = ratSpeed;
        ratAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Distance check to Player
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        // Debug.DrawRay(transform.position, transform.forward * fleeDistance, Color.white);
        Debug.DrawRay(transform.position, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.red);

        if (distanceToPlayer < chaseDistance)
        {
            //Chase Player
            currCondition = 1;
        }

        if (distanceToPlayer < attackDistance && player != null)
        {
            //Attack Player
            currCondition = 2;
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

        // if (!player.activeInHierarchy)
        // {
        //     currCondition = 3;
        //     deathScreen.SetActive(true);
        // }
    }

    void FixedUpdate()
    {
        switch (currCondition)
        {
            case 1: //Chase Condition
                ratAgent.SetDestination(player.transform.position);
                break;

            case 2: //Attack Condition
                player.SetActive(false);
                break;

            case 3: //Flee Condition
                    // Vector3 playerDir = this.transform.position - player.transform.position;
                    // Vector3 fleePos = this.transform.position + playerDir;
                    // ratAgent.SetDestination(fleePos);
                ratAgent.SetDestination(fleePos.transform.position);
                Debug.LogWarning("Fleeing");
                break;

            case 4: //Null Condition
                break;
        }
    }

    void FleeEventReceived()
    {
        FleeEnemy();
    }

    void FleeEnemy()
    {
        StartCoroutine(Flee());
    }

    IEnumerator Flee()
    {
        if (distanceToPlayer < chaseDistance)
            currCondition = 3;
        yield return 0;
    }
}
