using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsAITest : MonoBehaviour
{
    [Header("Paintings Variables")]
    public float attackDistance;
    public float distanceToPlayer;
    public Vector3 defaultLightDirection;
    public Vector3 firstLightDirection;
    public Vector3 secondLightDirection;
    public float changeDirection;

    [Header("Paintings FoV")]
    public float fov;
    [SerializeField]
    private float angle;
    private GameObject player;
    private int currCondition;
    private Vector3 tarDir;
    private Light paintingEyeLight;
    [SerializeField]
    private float time;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        paintingEyeLight = GetComponent<Light>();
        paintingEyeLight.color = Color.white;
        // paintingEyeLight.enabled = true;
        this.transform.localEulerAngles = defaultLightDirection;
        time = 0f;
    }

    // void Update()
    // {
    //     distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
    //     Debug.DrawRay(transform.position, transform.forward * attackDistance, Color.green);

    //     tarDir = player.transform.position - this.transform.position;
    //     angle = Vector3.Angle(this.tarDir, this.transform.forward);

    //     if (distanceToPlayer > attackDistance)
    //     {
    //         currCondition = 1;
    //     }

    //     if (distanceToPlayer < attackDistance && angle < fov)
    //     {
    //         currCondition = 2;
    //     }

    //     switch (currCondition)
    //     {
    //         case 1: //Null
    //             break;

    //         case 2: //Attack
    //             Debug.LogWarning("Attacking Player");
    //             break;
    //     }
    // }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        // if (time >= changeDirection)
        // {
        if (this.transform.localEulerAngles != defaultLightDirection && time >= changeDirection)
        {
            this.transform.localEulerAngles = defaultLightDirection;
            Debug.LogWarning("Default Light Direction");
            time = 0f;
        }

        if (this.transform.localEulerAngles != firstLightDirection && time >= changeDirection)
        {
            this.transform.localEulerAngles = firstLightDirection;
            Debug.LogWarning("First Light Direction");
            time = 0f;
        }

        if (this.transform.localEulerAngles != secondLightDirection && time >= changeDirection)
        {
            this.transform.localEulerAngles = secondLightDirection;
            Debug.LogWarning("Second Light Direction");
            time = 0f;
        }
        // }
    }
}
