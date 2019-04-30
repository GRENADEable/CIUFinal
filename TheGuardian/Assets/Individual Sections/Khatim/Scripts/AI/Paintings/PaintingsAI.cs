using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsAI : MonoBehaviour
{
    [Header("Paintings Variables")]
    public float rotationX;
    public float maxAngleY;
    public float rotationZ;
    public float lookaroundSpeed;
    public float increasedLookaroundSpeed;
    public Vector3 playerHeadRaycast;
    public Vector3 playerRightHandRaycast;
    public Vector3 playerLeftHandRaycast;
    public float lookatSpeed;

    [Header("Wait Timers")]
    public float killDelay;
    public float detectionDelay;
    public float timeToWaitBetweenRotations;

    [Header("Rotation Pauses")]
    public float firstRotationSmaller; // smaller value than the the bigger value 
    public float firstRotationBigger;
    public float secondRotationSmaller;
    public float secondRotationBigger;
    public float thirdRotationSmaller;
    public float thirdRotationBigger;

    public delegate void SendEventsToManager();
    public static event SendEventsToManager onPlayerDeath;

    [Header("Paintings FoV")]
    public float detectionFov;
    [SerializeField]
    private float angle;
    [SerializeField]
    private GameObject player;
    private Light paintingEyeLight;
    private float time;
    private float delta;
    private RaycastHit hit;
    private float currTimer;
    [SerializeField]
    private bool isPlayerHiding;
    private Vector3 tarDir;

    private enum paintingState { Looking_Around, Attack, Wait };
    private paintingState currCondition;

    void OnEnable()
    {
        paintingEyeLight = GetComponent<Light>();
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
        Debug.DrawRay(transform.position, (player.transform.position + playerHeadRaycast) - transform.position, Color.red);
        // Debug.DrawRay(transform.position, (player.transform.position + playerRightHandRaycast) - transform.position, Color.blue);
        // Debug.DrawRay(transform.position, (player.transform.position + playerLeftHandRaycast) - transform.position, Color.yellow);

        Physics.Raycast(transform.position, (player.transform.position + playerHeadRaycast) - transform.position, out hit);
        // Physics.Raycast(transform.position, (player.transform.position + playerRightHandRaycast) - transform.position, out hit);
        // Physics.Raycast(transform.position, (player.transform.position + playerLeftHandRaycast) - transform.position, out hit);

        Debug.Log(hit.collider);

        isPlayerHiding = !(hit.collider.tag == "Player");

        tarDir = player.transform.position - transform.position;
        angle = Vector3.Angle(tarDir, transform.forward);

        switch (currCondition)
        {
            case paintingState.Looking_Around:
                LookAround();
                paintingEyeLight.color = Color.white;

                if (angle < detectionFov && !isPlayerHiding)
                {
                    currTimer += Time.deltaTime;

                    if (currTimer >= detectionDelay)
                        SwitchState(paintingState.Attack);
                }

                // Debug.Log("Looking Around");
                break;

            case paintingState.Attack:
                paintingEyeLight.color = Color.red;

                if (angle > detectionFov || isPlayerHiding)
                    SwitchState(paintingState.Looking_Around);

                // Need to Clamp the Y Rotation when Player is in the FoV of the AI 
                Quaternion lookAtPlayerPos = Quaternion.LookRotation(tarDir.normalized, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookAtPlayerPos, lookatSpeed * Time.deltaTime);

                currTimer += Time.deltaTime;
                //Animation Playes;
                if (currTimer >= killDelay)
                {
                    if (onPlayerDeath != null)
                        onPlayerDeath();

                    player.SetActive(false);
                    Debug.Log("Player Dead");
                }
                // Debug.Log("Attacking Player");
                break;

            case paintingState.Wait:
                currTimer += Time.deltaTime;
                if (angle < detectionFov && !isPlayerHiding)
                    SwitchState(paintingState.Attack);
                if (currTimer >= timeToWaitBetweenRotations)
                    SwitchState(paintingState.Looking_Around);

                // Debug.Log("Waiting");
                break;
        }
    }
    void SwitchState(paintingState state)
    {
        currTimer = 0;
        currCondition = state;
    }

    void LookAround()
    {
        time = time + Time.deltaTime;
        delta = Mathf.Sin(time * lookaroundSpeed);
        transform.localRotation = Quaternion.Euler(rotationX, delta * maxAngleY, rotationZ);
        if ((transform.rotation.eulerAngles.y >= firstRotationSmaller && transform.rotation.eulerAngles.y <= firstRotationBigger) || (transform.rotation.eulerAngles.y >= firstRotationBigger && transform.rotation.eulerAngles.y <= firstRotationSmaller))
        {
            SwitchState(paintingState.Wait);
            // Debug.Log("doing the first rotation");
        }
        if ((transform.rotation.eulerAngles.y >= secondRotationSmaller && transform.rotation.eulerAngles.y <= secondRotationBigger) || (transform.rotation.eulerAngles.y >= secondRotationBigger && transform.rotation.eulerAngles.y <= secondRotationSmaller))
        {
            SwitchState(paintingState.Wait);
            // Debug.Log("doing the second rotation");
        }
        if ((transform.rotation.eulerAngles.y >= thirdRotationSmaller && transform.rotation.eulerAngles.y <= thirdRotationBigger) || (transform.rotation.eulerAngles.y >= thirdRotationBigger && transform.rotation.eulerAngles.y <= thirdRotationSmaller))
        {
            SwitchState(paintingState.Wait);
            // Debug.Log("doing the third rotation");
        }
    }

    void OnIncreasedSpeedEventReceived()
    {
        lookaroundSpeed = increasedLookaroundSpeed;
        GameManager.onIncreaseEyeSpeed -= OnIncreasedSpeedEventReceived;
        // Debug.Log("Paintings Speed Increased");
    }
}
