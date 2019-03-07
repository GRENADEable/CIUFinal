using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMechanic : MonoBehaviour
{
    public float maxFuelTimer;
    public int matchesCount;
    public GameObject match;
    public delegate void FleeEnemy();
    public static event FleeEnemy OnFleeEnemy;

    [SerializeField]
    private float fuelTimer;
    private Collider col;

    void Update()
    {
        //picking up a match comes here
        if (col != null && Input.GetKey(KeyCode.E) && col.gameObject.tag == "Matchstick")
        {
            col.gameObject.SetActive(false);
            matchesCount = 1;
        }

        Matchlight();
    }

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
    }

    public void GiveMatchStick()
    {
        matchesCount++;
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
