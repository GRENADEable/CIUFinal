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
    public float timeToWait;
    public float lightOnDuration;
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
        isEyesOpen = false;
    }

    IEnumerator LightChange()
    {
        while (true)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, defaultLightDirection.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, firstLightDirection.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, defaultLightDirection.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, secondLightDirection.y, transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    void Update()
    {
        Vector3 tarDir = player.transform.position - transform.position;
        angle = Vector3.Angle(tarDir, transform.forward);

        if (angle < detectionFov * 0.5f)
        {
            currCondition = 2;
            paintingEyeLight.color = Color.red;
        }
        else
        {
            currCondition = 1;
            paintingEyeLight.color = Color.white;
        }

        switch (currCondition)
        {
            case 1: //Null
                break;

            case 2: //Attack
                Debug.LogWarning("Attacking Player");
                break;
        }
    }
}
