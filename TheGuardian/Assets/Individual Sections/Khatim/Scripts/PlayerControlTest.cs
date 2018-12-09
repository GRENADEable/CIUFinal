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
    public float pushForce;
    private CharacterController charController;
    public bool iniatePuzzleLever = false;
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
        charController.SimpleMove(forward * curSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hookable")
        {
            iniatePuzzleLever = true;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rg = hit.collider.attachedRigidbody;

        if (rg == null || rg.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3)
            return;

        Vector3 dir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rg.velocity = dir * pushForce;
    }
}
