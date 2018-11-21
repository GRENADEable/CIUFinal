using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    private float moveInput;
    private float rotateInput;

    private Vector3 directionOfMovement;

    public CharacterController characterController;

	void Start ()
    {
        characterController = GetComponent<CharacterController>();
    }
	
	void Update ()
    {
        moveInput = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Horizontal");

        directionOfMovement = transform.TransformDirection(Vector3.forward);
    }

    void FixedUpdate()
    {
        transform.Rotate(0, rotateInput * rotateSpeed, 0);
        characterController.SimpleMove(directionOfMovement * moveSpeed * moveInput);
    }
}
