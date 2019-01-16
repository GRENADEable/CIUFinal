using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    public float throwingForce;
    public Vector3 objectToBeThrownPosition;
    public float distance;
    public RaycastHit hitInfo;

    public bool canGrab = true;
    public bool grabbing = false;
    public GameObject objectToBeThrown;
    public Vector3 objectToBeThrownOriginalPos;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.F) && canGrab && !grabbing)
        {
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) - new Vector3(0,0,2), out hitInfo, 10);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) - new Vector3(0, 0, 2) * 10, Color.green);
            // Physics.Raycast(transform.position - new Vector3(0, 2, 0), Vector3.forward, out hitInfo, 5);
            // Physics.Raycast(transform.position - new Vector3(0,2.5f,0), Vector3.forward, out hitInfo, 5);
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.tag == ("PickUp"))
            {
                objectToBeThrownOriginalPos = hitInfo.collider.transform.position;
                objectToBeThrown = hitInfo.collider.gameObject;
                hitInfo.collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
                hitInfo.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                hitInfo.collider.GetComponent<Rigidbody>().useGravity = false;
                hitInfo.collider.transform.SetParent(this.gameObject.transform);
                hitInfo.collider.gameObject.transform.localPosition = objectToBeThrownPosition;
                canGrab = false;
                grabbing = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !canGrab && grabbing)
        {
            objectToBeThrown.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            hitInfo.collider.GetComponent<Rigidbody>().useGravity = true;
            hitInfo.collider.transform.SetParent(null);
            objectToBeThrown = null;
            canGrab = true;
            grabbing = false;
        }
    }
}
    

