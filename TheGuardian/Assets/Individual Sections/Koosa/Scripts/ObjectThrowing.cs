using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : MonoBehaviour
{
    public float throwingForce;
    public Vector3 objectToBeThrownPosition;
    public float distance;
    public float height;
    [SerializeField]
    private bool isInteracting;
    private RaycastHit hitInfo;

    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * distance, Color.red);

        if (Physics.Raycast(transform.position + Vector3.up * height, transform.TransformDirection(Vector3.forward) * distance, out hitInfo)
        && hitInfo.collider.tag == "PickUp" && Input.GetKey(KeyCode.F)
        && !isInteracting)
        {
            PickUpFunctionality();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isInteracting)
        {
            ThrowingFunctionality();
        }

        if (Input.GetKeyDown(KeyCode.G) && isInteracting)
        {
            DroppingFunctionality();
        }
    }

    public void PickUpFunctionality()
    {
        hitInfo.collider.gameObject.AddComponent(typeof(FixedJoint));
        hitInfo.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        hitInfo.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
        hitInfo.rigidbody.useGravity = false;
        isInteracting = true;
        Debug.LogWarning("Object Picked Up");
    }

    public void ThrowingFunctionality()
    {
        Destroy(hitInfo.collider.gameObject.GetComponent<FixedJoint>());
        hitInfo.rigidbody.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
        hitInfo.rigidbody.useGravity = true;
        isInteracting = false;
        Debug.LogWarning("Object Thrown");
    }

    public void DroppingFunctionality()
    {
        Destroy(hitInfo.collider.gameObject.GetComponent<FixedJoint>());
        hitInfo.rigidbody.useGravity = true;
        isInteracting = false;
        Debug.LogWarning("Object LetGo");
    }
}


