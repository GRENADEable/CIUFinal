using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushandPull : MonoBehaviour
{
    public Vector3 raycastHeight;
    public float interactionDistance;

    [SerializeField]
    private bool isInteracting;

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance)
        && Input.GetKey(KeyCode.E) 
        && hit.collider.tag == "Interact" && !isInteracting)
        {
            // Sets bool to true, adds fixed joint component and links fixed joint from other gameobject to ours and turns off gravity.
            isInteracting = true;
            hit.collider.gameObject.AddComponent(typeof(FixedJoint));
            hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
            hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            hit.rigidbody.useGravity = false;
            Debug.LogWarning("Object Attached");
        }

        if ((Input.GetKeyUp(KeyCode.E) || !Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance)) 
        && isInteracting)
        {
            // Sets bool to false, removes fixed joint from the other gameobject with ours, turns on  gravity of the other object and destroys the fixed joint component.
            Destroy(hit.collider.gameObject.GetComponent<FixedJoint>());
            isInteracting = false;
            // hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = false;
            // hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            hit.rigidbody.useGravity = true;
            Debug.LogWarning("Object Detached");
        }
    }
}
