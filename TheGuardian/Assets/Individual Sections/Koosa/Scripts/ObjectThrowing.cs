using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    public float throwingForce;
    public Vector3 objectToBeThrownPosition;
    public float distance;
    public float height;
    public GameObject objectToBeThrown;
    public Rigidbody objectToBeThrownRB;
    [SerializeField]
    private bool isInteracting;
    // public Vector3 objectToBeThrownOriginalPos;

    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * distance, Color.red);

        if (Physics.Raycast(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * distance, out hitInfo)
        && hitInfo.collider.tag == "PickUp" && Input.GetKey(KeyCode.F)
        && !isInteracting)
        {

            //Rigidbody objectToBeThrownRB;
            objectToBeThrown = hitInfo.transform.gameObject;
            objectToBeThrownRB = objectToBeThrown.GetComponent<Rigidbody>();
            objectToBeThrownRB.velocity = Vector3.zero;
            objectToBeThrownRB.angularVelocity = Vector3.zero;
            objectToBeThrownRB.useGravity = false;
            objectToBeThrownRB.detectCollisions = true;
            //objectToBeThrownRB.constraints = RigidbodyConstraints.FreezeAll;
            objectToBeThrown.transform.SetParent(this.gameObject.transform);
            objectToBeThrown.transform.localPosition = objectToBeThrownPosition;
            isInteracting = true;
            Debug.LogWarning("Object Picked Up");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isInteracting)
        {
            objectToBeThrownRB.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            objectToBeThrownRB.useGravity = true;
            objectToBeThrownRB.constraints = RigidbodyConstraints.None;
            objectToBeThrown.transform.SetParent(null);
            // objectToBeThrown = null;
            isInteracting = false;
            Debug.LogWarning("Object Thrown");
        }
        if(Input.GetKeyDown(KeyCode.G) && isInteracting)
        {
            objectToBeThrownRB.useGravity = true;
            objectToBeThrownRB.constraints = RigidbodyConstraints.None;
            objectToBeThrown.transform.SetParent(null);
            isInteracting = false;
            Debug.LogWarning("Object LetGo");

        }

        if (objectToBeThrown != null)
        {
            objectToBeThrown.transform.localPosition = objectToBeThrownPosition;
            objectToBeThrownRB.velocity = Vector3.zero;
            objectToBeThrownRB.angularVelocity = Vector3.zero;
            objectToBeThrownRB.useGravity = false;
            objectToBeThrownRB.detectCollisions = true;
        }
           
    }
}


