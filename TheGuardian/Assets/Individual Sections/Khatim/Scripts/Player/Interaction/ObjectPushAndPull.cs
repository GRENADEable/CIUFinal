using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPushAndPull : PlayerInteraction
{
    public FixedJoint objectFixedJoint;
    public override void StartInteraction()
    {
        base.StartInteraction();
        interactCol.gameObject.AddComponent(typeof(FixedJoint));
        objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        objectFixedJoint.enableCollision = true;
        objectFixedJoint.connectedBody = rgPlayer;
        rgObject.isKinematic = false;
        rgObject.useGravity = false;

        // interactCol.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        // interactCol.gameObject.GetComponent<FixedJoint>().connectedBody = rgPlayer;
        // interactCol.GetComponent<Rigidbody>().isKinematic = false;
        // interactCol.GetComponent<Rigidbody>().useGravity = false;
        // Debug.LogWarning("Object Attached");
        Debug.Log("Object Push And Pull Started");
    }

    public override void UpdateInteraction()
    {
        Debug.Log("Object Push And Pull In Progress");
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
        rgObject.useGravity = true;
        Destroy(objectFixedJoint);

        // interactCol.GetComponent<Rigidbody>().useGravity = true;
        // Destroy(interactCol.gameObject.GetComponent<FixedJoint>());
        // Debug.LogWarning("Object LetGo");
        Debug.Log("Object Push And Pull Ended");
    }
}
