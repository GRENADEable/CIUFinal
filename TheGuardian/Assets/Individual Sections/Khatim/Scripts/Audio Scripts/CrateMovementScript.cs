using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMovementScript : MonoBehaviour
{
    public AudioSource crateMovementAud;
    private Rigidbody rg;
    public bool isMoving;

    void OnEnable()
    {
        rg = GetComponent<Rigidbody>();
        isMoving = false;
    }
    void Update()
    {
        if (!rg.useGravity && rg.velocity != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;


        if (isMoving)
        {
            crateMovementAud.Play();
            isMoving = false;
        }
        else
            crateMovementAud.Stop();
    }
}
