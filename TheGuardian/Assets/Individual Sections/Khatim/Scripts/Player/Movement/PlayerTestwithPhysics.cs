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

        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance) && Input.GetKeyDown(KeyCode.Space) && !isInteracting)
        {
            //Debug.LogWarning(hit.transform.tag);
            rg.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        //Checks if a raycast hit something and a key is pressed and the collider that the raycase hit is a specific tag and is not interacting.
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance) && Input.GetKeyDown(KeyCode.E) && hit.collider.tag == "Interact" && !isInteracting)
        {
            //Debug.LogWarning(hit.transform.name);
            //Sets bool to true, stores the gameobject in a variable, adds component fixed joint, connects a fixed joint from the other gameobject with ours and turns off gravity of the other object.
            isInteracting = true;
            hit.collider.gameObject.AddComponent(typeof(FixedJoint));
            hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
            hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            hit.rigidbody.useGravity = false;

            //interactableObj = hit.collider.gameObject;
            // interactableObj.AddComponent(typeof(FixedJoint));
            // interactableObj.GetComponent<FixedJoint>().enableCollision = true;
            // interactableObj.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            Debug.LogWarning("Object Attached");
        }
        //Checks if a raycast hit something and a key is not pressed and the collider that the raycase hit is a specific tag and is not interacting.
        else if (Input.GetKeyUp(KeyCode.E) && isInteracting)
        {
            //Sets bool to false, removes fixed joint from the other gameobject with ours, turns on  gravity of the other object and destroys the fixed joint component.
            isInteracting = false;
            hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = false;
            hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            hit.rigidbody.useGravity = true;
            Destroy(hit.collider.gameObject.GetComponent<FixedJoint>());

            // interactableObj.GetComponent<FixedJoint>().enableCollision = false;
            // interactableObj.GetComponent<FixedJoint>().connectedBody = null;
            // Destroy(interactableObj.GetComponent<FixedJoint>());
            // hit.rigidbody.useGravity = true;
            // interactableObj = null;
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
}