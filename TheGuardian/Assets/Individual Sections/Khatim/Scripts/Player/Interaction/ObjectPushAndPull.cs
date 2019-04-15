using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPushAndPull : PlayerInteraction
{
    public FixedJoint objectFixedJoint;

    public delegate void SendEventsToPlayer();
    public static event SendEventsToPlayer constraints;
    public static event SendEventsToPlayer noConstraints;

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (interactCol.GetComponent<FixedJoint>() == null)
            interactCol.gameObject.AddComponent(typeof(FixedJoint));

        objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        objectFixedJoint.connectedBody = rgPlayer;
        // rgObject.isKinematic = false;
        rgObject.useGravity = false;

        if (constraints != null)
            constraints();
        Debug.Log("Object Push And Pull Started");
    }

    public override void UpdateInteraction()
    {
        Debug.Log("Object Push And Pull In Progress");
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
        // rgObject.useGravity = true;
        // rgObject.isKinematic = true;

        Destroy(objectFixedJoint);
        if (noConstraints != null)
            noConstraints();
        Debug.Log("Object Push And Pull Ended");
    }
}
