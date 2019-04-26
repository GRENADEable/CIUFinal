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

    public delegate void SendDeathMessage();
    public static event SendDeathMessage onDeadPlayerScreen;

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
    }


    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position + rangeOffset, player.transform.position);
        Debug.DrawRay(transform.position + rangeOffset, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position + rangeOffset, transform.forward * attackDistance, Color.red);

        switch (currCondition)
        {
            case dollState.Idle:
                dollAnim.SetBool("isIdle", true);
                Debug.Log("Doll Idle");

                if (distanceToPlayer <= chaseDistance && player.activeInHierarchy)
                    currCondition = dollState.Chase;
                break;

            case dollState.Chase:
                dollAnim.SetBool("isIdle", false);
                dollAgent.SetDestination(player.transform.position);
                Debug.Log("Doll Chasing");

                if (distanceToPlayer >= chaseDistance)
                    currCondition = dollState.Idle;

                // if (distanceToPlayer <= attackDistance && player.activeInHierarchy)
                //     currCondition = dollState.Attack;
                break;

            case dollState.Attack:
                player.SetActive(false);
                Debug.Log("Attacking Player");

                if (onDeadPlayerScreen != null)
                    onDeadPlayerScreen();

                if (!player.activeInHierarchy)
                    currCondition = dollState.Idle;
                break;
        }
    }
}
