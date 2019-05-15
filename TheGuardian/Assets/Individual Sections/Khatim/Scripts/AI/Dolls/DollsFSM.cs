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
    public static event SendDeathMessage onDeadPlayer;

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
        VCamManager.onDollAIChange += OnDollAIChangeReceived;
    }

    void OnDisable()
    {
        VCamManager.onDollAIChange -= OnDollAIChangeReceived;
    }

    void OnDestroy()
    {
        VCamManager.onDollAIChange -= OnDollAIChangeReceived;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position + rangeOffset, player.transform.position);
        Debug.DrawRay(transform.position + rangeOffset, transform.forward * chaseDistance, Color.green);
        Debug.DrawRay(transform.position + rangeOffset, transform.forward * attackDistance, Color.red);

        dollAnim.SetFloat("speed", dollAgent.velocity.magnitude);

        switch (currCondition)
        {
            case dollState.Idle:
                dollAnim.SetBool("isAttacking", false);
                // Debug.Log("Doll Idle State");

                if (distanceToPlayer <= chaseDistance && player.activeInHierarchy)
                    currCondition = dollState.Chase;
                break;

            case dollState.Chase:
                dollAnim.SetBool("isAttacking", false);
                dollAgent.SetDestination(player.transform.position);
                // Debug.Log("Doll Chasing State");

                if (distanceToPlayer <= attackDistance && player.activeInHierarchy)
                    currCondition = dollState.Attack;
                break;

            case dollState.Attack:
                dollAnim.SetBool("isAttacking", true);
                // Debug.Log("Attacking Player State");

                if (!player.activeInHierarchy)
                    currCondition = dollState.Idle;

                if (distanceToPlayer >= attackDistance && player.activeInHierarchy)
                    currCondition = dollState.Chase;
                break;
        }
    }

    void OnDollAIChangeReceived()
    {
        chaseDistance = 0;
        currCondition = dollState.Idle;
        VCamManager.onDollAIChange -= OnDollAIChangeReceived;
        Debug.Log("Doll AI State Change Received");
    }

    void KillPlayer()
    {
        if (distanceToPlayer < attackDistance)
        {
            player.SetActive(false);
            // Debug.Log("Attacking Player");

            if (onDeadPlayer != null)
                onDeadPlayer();
        }
    }
}
