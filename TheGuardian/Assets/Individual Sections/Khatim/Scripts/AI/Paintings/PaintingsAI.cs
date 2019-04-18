using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsAI : MonoBehaviour
{
    [Header("Paintings Variables")]
    // public float rotationX;
    // public float maxAngleY;
    // public float rotationZ;
    // public float lookaroundSpeed;
    public Vector3 raycastHeight;
    public float lookatSpeed;

    [Header("Wait Timers")]
    public float killDelay;
    public float detectionDelay;
    // public float timeToWaitBetweenRotations;
    // public float paintingWaitTime;

    // [Header("Rotation Pauses")]
    // public float firstRotationSmaller; // smaller value than the the bigger value 
    // public float firstRotationBigger;
    // public float secondRotationSmaller;
    // public float secondRotationBigger;
    // public float thirdRotationSmaller;
    // public float thirdRotationBigger;

    public delegate void SendEventsToManager();
    public static event SendEventsToManager onPlayerDeath;

    [Header("Paintings FoV")]
    public float detectionFov;
    [SerializeField]
    private float angle;
    [SerializeField]
    private GameObject player;
    private Light paintingEyeLight;
    // private float time;
    // private float delta;
    private RaycastHit hit;
    private float curTimer;
    [SerializeField]
    private bool isPlayerHiding;
    private Vector3 tarDir;
    private EYEpaintings2 eyeLightMovement;

    private enum paintingState { Looking_Around, Attack, Wait };
    private paintingState currCondition;

    void OnEnable()
    {
        paintingEyeLight = GetComponent<Light>();
        eyeLightMovement = GetComponent<EYEpaintings2>();
        player = GameObject.FindGameObjectWithTag("Player");
        currCondition = paintingState.Looking_Around;
        GameManager.onIncreaseEyeSpeed += OnIncreasedSpeedEventReceived;
    }

    void OnDisable()
    {
        GameManager.onIncreaseEyeSpeed -= OnIncreasedSpeedEventReceived;
    }

    void OnDestroy()
    {
        GameManager.onIncreaseEyeSpeed -= OnIncreasedSpeedEventReceived;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, (player.transform.position + raycastHeight) - transform.position, Color.red);
        Physics.Raycast(transform.position, (player.transform.position + raycastHeight) - transform.position, out hit);

        isPlayerHiding = !(hit.collider.tag == "Player");

        tarDir = player.transform.position - transform.position;
        angle = Vector3.Angle(tarDir, transform.forward);

        switch (currCondition)
        {
            case paintingState.Looking_Around:
                // StartCoroutine(LookingAround());
                eyeLightMovement.enabled = true;
                paintingEyeLight.color = Color.white;

                if (angle < detectionFov && !isPlayerHiding)
                {
                    curTimer += Time.deltaTime;

                    if (curTimer >= detectionDelay)
                        SwitchState(paintingState.Attack);
                }

                Debug.Log("Looking Around");
                break;

            case paintingState.Attack:
                eyeLightMovement.enabled = false;
                eyeLightMovement.StopAllCoroutines();
                paintingEyeLight.color = Color.red;

                if (angle > detectionFov || isPlayerHiding)
                {
                    SwitchState(paintingState.Looking_Around);
                }
                // Need to Clamp the Y Rotation when Player is in the FoV of the AI 
                Quaternion lookAtPlayerPos = Quaternion.LookRotation(tarDir.normalized, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookAtPlayerPos, lookatSpeed * Time.deltaTime);

                curTimer += Time.deltaTime;
                //Animation Playes;
                if (curTimer >= killDelay)
                {
                    if (onPlayerDeath != null)
                        onPlayerDeath();

                    player.SetActive(false);
                    Debug.Log("Player Dead");
                }
                Debug.Log("Attacking Player");
                break;

            case paintingState.Wait:
                break;
        }
    }
    void SwitchState(paintingState state)
    {
        curTimer = 0;
        currCondition = state;
    }

    // void TimeChanger()
    // {
    //     time = time + Time.deltaTime;
    //     delta = Mathf.Sin(time * lookaroundSpeed);
    // }

    // void LightRotator(float delta)
    // {
    //     transform.localRotation = Quaternion.Euler(rotationX, delta * maxAngleY, rotationZ);
    // }

    void OnIncreasedSpeedEventReceived()
    {
        eyeLightMovement.lookaroundSpeed = 5f;
    }
}
