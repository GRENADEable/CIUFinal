using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        charController.SimpleMove(forward * curSpeed);
    }
}
