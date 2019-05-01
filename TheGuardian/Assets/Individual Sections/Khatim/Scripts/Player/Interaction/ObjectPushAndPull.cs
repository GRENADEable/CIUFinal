using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPushAndPull : PlayerInteraction
{
    public FixedJoint objectFixedJoint;
    // public Rigidbody rgCourageHead;

    public delegate void SendEventsToPlayer();
    public static event SendEventsToPlayer constraints;
    public static event SendEventsToPlayer noConstraints;
    public Animator courageAnim;

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (interactCol.GetComponent<FixedJoint>() == null)
            interactCol.gameObject.AddComponent(typeof(FixedJoint));

        objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        objectFixedJoint.connectedBody = rgPlayer;
        // rgObject.isKinematic = false;
        rgObject.useGravity = false;
        // courageAnim.Play("CouragePush");

        if (constraints != null)
            constraints();
        // Debug.Log("Object Push And Pull Started");
    }

    public override void UpdateInteraction()
    {
        // Debug.Log("Object Push And Pull In Progress");
    }

    public override void EndInteraction()
    {
        rgObject.useGravity = true;
        // rgObject.isKinematic = true;
        Destroy(objectFixedJoint);
        objectFixedJoint = null;
        base.EndInteraction();
        if (noConstraints != null)
            noConstraints();

        base.EndInteraction();
        // Debug.Log("Object Push And Pull Ended");
    }
}
