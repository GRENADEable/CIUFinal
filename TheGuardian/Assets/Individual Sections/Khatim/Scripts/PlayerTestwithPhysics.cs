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
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);

        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance) && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogWarning(hit.transform.tag);
            Jump();
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance) && Input.GetKeyDown(KeyCode.E) && hit.collider.tag == "Interact")
        {
            Debug.LogWarning(hit.transform.name);
            hit.transform.parent = this.transform;
            hit.rigidbody.isKinematic = true;
            Debug.LogWarning("Object Attached");
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance) && Input.GetKeyUp(KeyCode.E) && hit.collider.tag == "Interact")
        {
            hit.transform.parent = null;
            hit.rigidbody.isKinematic = false;
            Debug.LogWarning("Object Detached");
        }

        /*if (objectToInteract != null)
        {
            distance = Vector3.Distance(transform.position, objectToInteract.transform.position);
        }

        if (distance > maxInteract)
            objectToInteract = null;
        else if (distance < minInteract)*/

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

    /*void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Interact" && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            Debug.LogWarning("Object Attached");
            other.gameObject.transform.parent = this.transform;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            isInteracting = true;
        }

        if (other.gameObject.tag == "Interact" && Input.GetKeyUp(KeyCode.E) && isInteracting)
        {
            Debug.LogWarning("Object Detached");
            other.gameObject.transform.parent = null;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            isInteracting = false;
        }
    }*/
}