using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float raycastDistance;
    public float jumpSpeed;
    public Vector3 raycastHeight;
    private CharacterController charController;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + raycastHeight, transform.forward * raycastDistance, Color.green);
        if (Physics.Raycast(transform.position + raycastHeight, transform.forward, out hit, raycastDistance) && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogWarning(hit.transform.name);
            //Make Character Jump Over Object.
        }
    }
    void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        //charController.SimpleMove(forward * curSpeed);
    }
}
