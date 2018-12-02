using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestwithPhysics : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float jumpForce;
    public float rotationSpeed;
    public float raycastDistance;
    public Vector3 raycastHeight;
    private Rigidbody rg;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {

        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance) && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogWarning(hit.transform.tag);
            Jump();
        }
    }
    void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");

        Vector3 clampedSpeed = Vector3.ClampMagnitude(forward, maxSpeed);

        rg.AddForce(clampedSpeed * curSpeed, ForceMode.Impulse);
    }

    void Jump()
    {
        rg.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
