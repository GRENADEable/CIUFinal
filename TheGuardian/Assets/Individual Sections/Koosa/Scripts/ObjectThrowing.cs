using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    public float throwingForce;
    // public float raycastDistance;
    // public float height;
    public delegate void Pickup();
    public static event Pickup onObjectDropEvent;

    [SerializeField]
    private bool isInteracting;
    [SerializeField]
    private Collider pickupCol;
    // private RaycastHit hitInfo;

    void Update()
    {
        // Debug.DrawRay(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.red);

        // if (Physics.Raycast(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * raycastDistance, out hitInfo)
        // && hitInfo.collider.tag == "PickUp" && Input.GetKey(KeyCode.F)
        // && !isInteracting)
        if (Input.GetKey(KeyCode.F) && pickupCol != null && !isInteracting)
        {
            PickUpFunctionality();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isInteracting && pickupCol != null)
        {
            ThrowingFunctionality();
        }

        if (Input.GetKeyUp(KeyCode.F) && isInteracting && pickupCol != null)
        {
            DroppingFunctionality();
        }
    }

    public void PickUpFunctionality()
    {
        // hitInfo.collider.gameObject.AddComponent(typeof(FixedJoint));
        // hitInfo.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        // hitInfo.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
        // hitInfo.rigidbody.useGravity = false;
        pickupCol.gameObject.AddComponent(typeof(FixedJoint));
        pickupCol.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        pickupCol.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
        pickupCol.GetComponent<Rigidbody>().useGravity = false;
        isInteracting = true;
        Debug.LogWarning("Object Picked Up");
    }

    public void ThrowingFunctionality()
    {
        // Destroy(hitInfo.collider.gameObject.GetComponent<FixedJoint>());
        // hitInfo.rigidbody.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
        // hitInfo.rigidbody.useGravity = true;
        Destroy(pickupCol.gameObject.GetComponent<FixedJoint>());
        Rigidbody objectRg = pickupCol.GetComponent<Rigidbody>();
        objectRg.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
        objectRg.useGravity = true;
        isInteracting = false;
        Debug.LogWarning("Object Thrown");
    }

    public void DroppingFunctionality()
    {
        // Destroy(hitInfo.collider.gameObject.GetComponent<FixedJoint>());
        // hitInfo.rigidbody.useGravity = true;
        // isInteracting = false;
        if (onObjectDropEvent != null)
            onObjectDropEvent();

        isInteracting = false;
        // Debug.LogWarning("Object LetGo");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            pickupCol = other;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        {
            pickupCol = null;
        }
    }
}


