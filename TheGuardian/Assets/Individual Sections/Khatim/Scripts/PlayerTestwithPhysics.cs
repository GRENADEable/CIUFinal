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
    public float interactionDistance;
    public float distance;
    public float maxInteract;
    public float minInteract;
    private GameObject objectToInteract;
    private Rigidbody rg;
    [SerializeField]
    private bool isInteracting;
    [SerializeField]
    private GameObject interactableObj;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);

        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance) && Input.GetKeyDown(KeyCode.Space) && !isInteracting && hit.collider.tag == "Ground")
        {
            //Debug.LogWarning(hit.transform.tag);
            Jump();
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance) && Input.GetKeyDown(KeyCode.E) && hit.collider.tag == "Interact" && !isInteracting)
        {
            //Debug.LogWarning(hit.transform.name);
            isInteracting = true;
            interactableObj = hit.collider.gameObject;
            interactableObj.GetComponent<FixedJoint>().enableCollision = true;
            interactableObj.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            hit.rigidbody.useGravity = false;
            Debug.LogWarning("Object Attached");
        }
        else if (Input.GetKeyUp(KeyCode.E) && isInteracting)
        {
            isInteracting = false;
            interactableObj.GetComponent<FixedJoint>().enableCollision = false;
            interactableObj.GetComponent<FixedJoint>().connectedBody = null;
            hit.rigidbody.useGravity = true;
            Debug.LogWarning("Object Detached");
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