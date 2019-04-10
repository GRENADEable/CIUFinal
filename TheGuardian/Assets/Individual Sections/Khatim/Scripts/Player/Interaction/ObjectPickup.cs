using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public override void StartInteraction()
    {
        base.StartInteraction();
        Debug.LogWarning("Object Pickup Started");
    }

    public override void UpdateInteraction()
    {
        
        Debug.LogWarning("Object Pickup In Progress");
    }

    public override void EndInteraction()
    {
        Debug.LogWarning("Object Pickup Ended");
    }

   // void ObjectPickup()
    // {
    //     //Replaced it with trigger collider because the raycast was not accurate when the distance was increased or decreased.
    //     interactCol.gameObject.AddComponent(typeof(FixedJoint));
    //     interactCol.transform.position = pivotDummy.position;

    //     interactCol.gameObject.GetComponent<FixedJoint>().connectedBody = rgCourageRightHand;
    //     interactCol.GetComponent<Rigidbody>().useGravity = false;
    //     Debug.LogWarning("Object Picked Up");
    // }

    // void ObjectDrop()
    // {
    //     //To avoid the three lines of code to not run. I moved those three lines of code under the DropObject Class.
    //     // if (onObjectDropEvent != null)
    //     // {
    //     //     isPickingObject = false;
    //     //     onObjectDropEvent();
    //     // }

    //     interactCol.GetComponent<Rigidbody>().useGravity = true;
    //     Destroy(interactCol.gameObject.GetComponent<FixedJoint>());
    //     Debug.LogWarning("Object LetGo");
    // }

    // void ObjectThrow()
    // {
    //     Destroy(interactCol.gameObject.GetComponent<FixedJoint>());
    //     Rigidbody objectRg = interactCol.GetComponent<Rigidbody>();
    //     objectRg.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
    //     objectRg.useGravity = true;
    //     Debug.LogWarning("Object Thrown");
    // }
}
