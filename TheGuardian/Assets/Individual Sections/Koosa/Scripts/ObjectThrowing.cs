using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    public float throwingForce;
    public float raycastDistance;
    public float height;
    public Vector3 objectDistance;

    public delegate void ObjectThrowingMessage();
    public static event ObjectThrowingMessage onObjectDropEvent;
    public static event ObjectThrowingMessage onKeyDropEvent;

    [SerializeField]
    private Rigidbody rgCourageRightHand;
    // [SerializeField]
    // private GameObject playerHead;

    // [SerializeField]
    // private bool isInteracting;
    [SerializeField]
    private Collider pickupCol;
    // private RaycastHit hitInfo;
    [SerializeField]
    private PlayerControls plyControls;

    void OnEnable()
    {
        plyControls = GetComponent<PlayerControls>();
        rgCourageRightHand = GetComponentInChildren<Rigidbody>();
        // playerHead = GameObject.FindGameObjectWithTag("PlayerHead");
    }

    void Update()
    {
        // Debug.DrawRay(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.red);
        // bool interact = Physics.Raycast(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward), out hitInfo, raycastDistance);

        // if (interact && hitInfo.collider.tag == "PickUp" && Input.GetKeyDown(KeyCode.F)
        // && !plyControls.isPickingObject)
        if (Input.GetKey(KeyCode.F) && pickupCol != null && !plyControls.isPickingObject)
        {
            PickUpFunctionality();
        }
        if (Input.GetKeyDown(KeyCode.Space) && plyControls.isPickingObject && pickupCol != null)
        // if (Input.GetKeyDown(KeyCode.Space) && plyControls.isPickingObject && interact)
        {
            ThrowingFunctionality();
        }

        // if (Input.GetKeyUp(KeyCode.F) && isInteracting && pickupCol != null)
        // if ((Input.GetKey(KeyCode.G) && isInteracting) || (pickupCol == null && isInteracting))
        if (Input.GetKeyDown(KeyCode.G) && plyControls.isPickingObject)
        {
            DroppingFunctionality();
        }
    }

    public void PickUpFunctionality()
    {
        // hitInfo.collider.gameObject.AddComponent(typeof(FixedJoint));
        // // hitInfo.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        // // hitInfo.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
        // hitInfo.transform.position = playerHead.transform.position + objectDistance;

        // hitInfo.collider.gameObject.GetComponent<FixedJoint>().connectedBody = rgCourageRightHand;
        // hitInfo.rigidbody.useGravity = false;

        //Replaced it with trigger collider because the raycast was not accurate when the distance was increased or decreased.
        pickupCol.gameObject.AddComponent(typeof(FixedJoint));
        // pickupCol.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        // pickupCol.transform.position = playerHead.transform.position;

        pickupCol.gameObject.GetComponent<FixedJoint>().connectedBody = rgCourageRightHand;
        pickupCol.GetComponent<Rigidbody>().useGravity = false;
        plyControls.isPickingObject = true;
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
        plyControls.isPickingObject = false;
        Debug.LogWarning("Object Thrown");
    }

    public void DroppingFunctionality()
    {
        // Destroy(hitInfo.collider.gameObject.GetComponent<FixedJoint>());
        // hitInfo.rigidbody.useGravity = true;
        // isInteracting = false;

        //To avoid the three lines of code to not run. I moved those three lines of code under the DropObject Class.
        if (onObjectDropEvent != null)
        {
            plyControls.isPickingObject = false;
            onObjectDropEvent();
        }
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


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "KeyCollector" && Input.GetKeyDown(KeyCode.G) && plyControls.isPickingObject)
        {
            if (onKeyDropEvent != null)
            {
                Destroy(pickupCol.gameObject);
                // Destroy(hitInfo.collider.gameObject);
                onKeyDropEvent();
            }
        }
    }
}


