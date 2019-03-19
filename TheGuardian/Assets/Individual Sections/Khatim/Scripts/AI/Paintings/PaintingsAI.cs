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

    [Header("Wait Timers")]
    public float killDelay;
    private float killTimer;

    [Header("Paintings FoV")]
    public float detectionFov;

    [SerializeField]
    private float angle;
    [SerializeField]
    private GameObject player;
    private int currCondition;
    private Light paintingEyeLight;

    private float time;
    private float delta;

    void Start()
    {
        paintingEyeLight = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        TimeChanger();
        LightRotator(delta);

        Vector3 tarDir = player.transform.position - transform.position;
        angle = Vector3.Angle(tarDir, transform.forward);

        if (angle < detectionFov * 0.5f)
        {
            currCondition = 2;
            paintingEyeLight.color = Color.red;
        }
        else
        {
            killTimer = 0;
            currCondition = 1;
            paintingEyeLight.color = Color.white;
        }

        switch (currCondition)
        {
            case 1: //Null
                break;

            case 2: //Attack
                KillPlayer();
                // Debug.LogWarning("Attacking Player");
                break;
        }
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

    void KillPlayer()
    {
        killTimer += Time.deltaTime;
        //Animation Playes;
        if (killTimer >= killDelay)
        {
            //Send Message To Kill the Player
            // OnSlowPlayer();
            player.SetActive(false);
            Debug.LogWarning("Player Dead");
            // killTimer = 0;
        }
    }
}
