using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    public float throwingForce;
    public Vector3 objectToBeThrownPosition;
    public float distance;
    public float height;
    // public GameObject objectToBeThrown;
    [SerializeField]
    private bool isInteracting;
    // public Vector3 objectToBeThrownOriginalPos;

    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward), Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward), out hitInfo, distance)
        && hitInfo.collider.tag == "PickUp" && Input.GetKey(KeyCode.F)
        && !isInteracting)
        {
            // objectToBeThrown = hitInfo.collider.gameObject;
            // hitInfo.rigidbody.velocity = Vector3.zero;
            // hitInfo.rigidbody.angularVelocity = Vector3.zero;
            hitInfo.rigidbody.useGravity = false;
            hitInfo.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            hitInfo.collider.transform.SetParent(this.gameObject.transform);
            hitInfo.collider.gameObject.transform.localPosition = objectToBeThrownPosition;
            isInteracting = true;
            Debug.LogWarning("Object Picked Up");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isInteracting)
        {
            hitInfo.rigidbody.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            hitInfo.rigidbody.useGravity = true;
            hitInfo.rigidbody.constraints = RigidbodyConstraints.None;
            hitInfo.collider.transform.SetParent(null);
            // objectToBeThrown = null;
            isInteracting = false;
            Debug.LogWarning("Object Thrown");
        }
    }
}


