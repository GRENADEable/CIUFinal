using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMechanic : MonoBehaviour
{
    public GameObject match;
    public delegate void FleeEnemy();
    public static event FleeEnemy OnFleeEnemy;

    public bool lightOn;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private int matchesCount;

    void Update()
    {
        //picking up a match comes here
        if (Input.GetKeyUp(KeyCode.E) && !lightOn && matchesCount > 0 && !match.activeSelf)
        {
            match.SetActive(true);
            lightOn = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) && lightOn)
        {
            match.SetActive(false);
            lightOn = false;
        }

        if (match.activeSelf)
        {
            if (OnFleeEnemy != null)
                OnFleeEnemy();
        }
    }

    /*
    void Matchlight()
    {
        if (Input.GetKeyDown(KeyCode.F) && matchesCount > 0 && !match.activeSelf)
        {
            if (fuelTimer > 0)
            {
                match.SetActive(true);
            }

            if (fuelTimer < 0)
            {
                match.SetActive(false);
            }
        }

        if (match.activeSelf)
        {
            fuelTimer -= Time.deltaTime;
            if (OnFleeEnemy != null)
                OnFleeEnemy();
        }
        if (fuelTimer <= 0)
        {
            matchesCount = 0;
            fuelTimer = maxFuelTimer;
            match.SetActive(false);
        }
    }*/

    public void GiveMatchStick()
    {
        matchesCount = 1;
        Debug.LogWarning("Matchstick Aquired");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Matchstick")
        {
            col = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Matchstick")
        {
            col = null;
        }
    }
}
