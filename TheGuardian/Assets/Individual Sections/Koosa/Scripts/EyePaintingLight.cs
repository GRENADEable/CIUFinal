using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePaintingLight : MonoBehaviour
{
    [Header("Paintings Variables")]
    // public float attackDistance;
    // public float distanceToPlayer;
    public Vector3 defaultLightDirection;
    public Vector3 firstLightDirection;
    public Vector3 secondLightDirection;
    // public delegate void SlowPlayer();
    // public static event SlowPlayer OnSlowPlayer;
    [Header("Wait Timers")]
    public float lighOnDuration;
    public float lighOffDuration;
    public float killDelay;
    [SerializeField]
    private float killTimer;
    [Header("Paintings FoV")]
    public float detectionFov;
    [SerializeField]
    private float angle;
    [SerializeField]
    private GameObject player;
    private int currCondition;
    private Light paintingEyeLight;
    private bool isEyesOpen;

    void Start()
    {
        paintingEyeLight = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(LightChange());
    }
    void Update()
    {
        Vector3 tarDir = player.transform.position - transform.position;
        angle = Vector3.Angle(tarDir, transform.forward);

        if (angle < detectionFov * 0.5f && isEyesOpen)
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

    IEnumerator LightChange()
    {
        while (true)
        {
            paintingEyeLight.enabled = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, defaultLightDirection.y, transform.rotation.eulerAngles.z);
            isEyesOpen = true;
            yield return new WaitForSeconds(lighOnDuration);

            paintingEyeLight.enabled = false;
            isEyesOpen = false;
            yield return new WaitForSeconds(lighOffDuration);

            paintingEyeLight.enabled = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, firstLightDirection.y, transform.rotation.eulerAngles.z);
            isEyesOpen = true;
            yield return new WaitForSeconds(lighOnDuration);

            paintingEyeLight.enabled = false;
            isEyesOpen = false;
            yield return new WaitForSeconds(lighOffDuration);

            paintingEyeLight.enabled = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, defaultLightDirection.y, transform.rotation.eulerAngles.z);
            isEyesOpen = true;
            yield return new WaitForSeconds(lighOnDuration);

            paintingEyeLight.enabled = false;
            isEyesOpen = false;
            yield return new WaitForSeconds(lighOffDuration);

            paintingEyeLight.enabled = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, secondLightDirection.y, transform.rotation.eulerAngles.z);
            isEyesOpen = true;
            yield return new WaitForSeconds(lighOnDuration);

            paintingEyeLight.enabled = false;
            isEyesOpen = false;
            yield return new WaitForSeconds(lighOffDuration);
        }
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
