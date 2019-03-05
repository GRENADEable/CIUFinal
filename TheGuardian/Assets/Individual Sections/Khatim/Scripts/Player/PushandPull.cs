using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushandPull : MonoBehaviour
{
    public float interactionDistance;
    public float interactionDistanceHeight;

    // private Vector3 interactionPos;
    private bool isInteracting;

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * interactionDistanceHeight, transform.TransformDirection(Vector3.forward) * interactionDistance, Color.blue);
        bool interaction = Physics.Raycast(transform.position + Vector3.up * interactionDistanceHeight, transform.TransformDirection(Vector3.forward) * interactionDistance, out hit);

        if (interaction && Input.GetKey(KeyCode.E) && hit.collider.tag == "Interact" && !isInteracting)
        {
            // Sets bool to true, adds fixed joint component and links fixed joint from other gameobject to ours and turns off gravity.
            isInteracting = true;
            hit.collider.gameObject.AddComponent(typeof(FixedJoint));
            hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
            hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            hit.rigidbody.useGravity = false;

            // isInteracting = true;
            // hit.rigidbody.useGravity = false;
            // hit.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            // hit.collider.transform.SetParent(this.gameObject.transform);
            // hit.collider.gameObject.transform.localPosition = interactionPos;
            Debug.LogWarning("Object Attached");
        }

        if (Input.GetKeyUp(KeyCode.E) && isInteracting)
        {
            // Sets bool to false, removes fixed joint from the other gameobject with ours, turns on  gravity of the other object and destroys the fixed joint component.
            Destroy(hit.collider.gameObject.GetComponent<FixedJoint>());
            isInteracting = false;
            // hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = false;
            // hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = null;
            hit.rigidbody.useGravity = true;

            // hit.rigidbody.useGravity = true;
            // hit.rigidbody.constraints = RigidbodyConstraints.None;
            // hit.collider.transform.SetParent(null);
            // isInteracting = false;
            Debug.LogWarning("Object Detached");
        }
    }
}
