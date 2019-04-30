using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DollsFSM : MonoBehaviour
{
    [Header("Doll Variables")]
    public float attackDistance;
    public float chaseDistance;
    public Vector3 rangeOffset;
    public float distanceToPlayer;
    public float dollSpeed;
    public GameObject player;

    // public delegate void SendMessages();
    // public static event SendMessages onDeadPlayerScreen;
    // public static event SendMessages onDollChaseStart;

    private NavMeshAgent dollAgent;
    private Animator dollAnim;
    private enum dollState { Idle, Chase, Attack };
    private dollState currCondition;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dollAgent = GetComponent<NavMeshAgent>();
        dollAgent.speed = dollSpeed;
        dollAnim = GetComponent<Animator>();

        // DollsFSM.onDollChaseStart += OnDollChaseStartEventReceived;
    }


    void OnDisable()
    {
        // DollsFSM.onDollChaseStart -= OnDollChaseStartEventReceived;
    }

    void OnDestroy()
    {
        // DollsFSM.onDollChaseStart -= OnDollChaseStartEventReceived;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position + rangeOffset, player.transform.position);
        Debug.DrawRay(transform.position + rangeOffset, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position + rangeOffset, transform.forward * attackDistance, Color.red);

        if (dollAgent.velocity.magnitude < 0.1f)
        {
            dollAnim.SetBool("isIdle", true);
            // Debug.Log("Idle Animation");
        }

        if (dollAgent.velocity.magnitude > 0.2f)
        {
            dollAnim.SetBool("isIdle", false);
            // Debug.Log("Not Idle Animation");
        }

        switch (currCondition)
        {
            case dollState.Idle:
                // dollAnim.SetBool("isIdle", true);
                // Debug.Log("Doll Idle State");

                if (distanceToPlayer <= chaseDistance && player.activeInHierarchy)
                    currCondition = dollState.Chase;
                break;

            case dollState.Chase:
                // dollAnim.SetBool("isIdle", false);
                dollAgent.SetDestination(player.transform.position);
                // Debug.Log("Doll Chasing State");

                // if (distanceToPlayer >= chaseDistance)
                //     currCondition = dollState.Idle;

                // if (distanceToPlayer <= attackDistance && player.activeInHierarchy)
                //     currCondition = dollState.Attack;
                break;

            case dollState.Attack:
                player.SetActive(false);
                // Debug.Log("Attacking Player State");

                // if (onDeadPlayerScreen != null)
                //     onDeadPlayerScreen();

                if (!player.activeInHierarchy)
                    currCondition = dollState.Idle;

                if (distanceToPlayer >= attackDistance)
                    currCondition = dollState.Chase;

                break;
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player" && onDollChaseStart != null)
    //         onDollChaseStart();

    //     GetComponent<Collider>().enabled = false;
    // }

    // void OnDollChaseStartEventReceived()
    // {
    //     currCondition = dollState.Chase;
    //     DollsFSM.onDollChaseStart -= OnDollChaseStartEventReceived;
    //     Debug.Log("Doll Chase Event Started");
    // }
}
