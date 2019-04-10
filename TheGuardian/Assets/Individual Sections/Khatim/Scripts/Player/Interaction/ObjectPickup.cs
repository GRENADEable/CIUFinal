using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public Transform pivotDummy;
    public Rigidbody rgCourageRightHand;
    public FixedJoint objectFixedJoint;
    public float throwingForce;


    public override void StartInteraction()
    {
        base.StartInteraction();
        if (interactCol.GetComponent<FixedJoint>() == null)
            interactCol.gameObject.AddComponent(typeof(FixedJoint));

        objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        interactCol.transform.position = pivotDummy.position;
        objectFixedJoint.connectedBody = rgCourageRightHand;
        rgObject.useGravity = false;
        Debug.Log("Object Picked Up");
    }
    public override void UpdateInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(objectFixedJoint);
            rgObject.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            rgObject.useGravity = true;
            Debug.Log("Object Thrown");
        }
    }
    public override void EndInteraction()
    {
        rgObject.useGravity = true;
        Destroy(objectFixedJoint);
        Debug.Log("Object Pickup Ended");
    }
}
