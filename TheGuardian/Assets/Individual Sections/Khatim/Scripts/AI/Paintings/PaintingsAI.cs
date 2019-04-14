using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsAI : MonoBehaviour
{
    [Header("Paintings Variables")]
    public float rotationX;
    public float maxAngleY;
    public float rotationZ;
    public float speed;
    public Vector3 raycastHeight;
    public float lookatSpeed;

    [Header("Wait Timers")]
    public float killDelay;
    public float detectionDelay;
    // public float paintingWaitTime;

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
    [SerializeField]
    private float delta;
    private RaycastHit hit;
    private float curTimer;
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
                TimeChanger();
                LightRotator(delta);

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
                    //Send Message To Kill the Player
                    // OnSlowPlayer();
                    if (onPlayerDeath != null)
                        onPlayerDeath();

                    player.SetActive(false);
                    Debug.Log("Player Dead");
                    // killTimer = 0;
                }

                // Vector3 target = player.transform.position;
                // target.y = transform.position.y;
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

    void TimeChanger()
    {
        time = time + Time.deltaTime;
        delta = Mathf.Sin(time * speed);
    }

    void LightRotator(float delta)
    {
        transform.localRotation = Quaternion.Euler(rotationX, delta * maxAngleY, rotationZ);
    }
}
