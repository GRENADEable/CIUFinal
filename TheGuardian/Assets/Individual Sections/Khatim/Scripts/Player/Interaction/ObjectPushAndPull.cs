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
    private Animator courageAnim;
    private PlayerControls plyControls;

    void OnEnable()
    {
        plyControls = GetComponent<PlayerControls>();
        courageAnim = GetComponent<Animator>();
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        courageAnim.SetBool("isPushing", true);
        plyControls.isPushingOrPulling = true;
        if (interactCol.GetComponent<FixedJoint>() == null)
            interactCol.gameObject.AddComponent(typeof(FixedJoint));

        objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        objectFixedJoint.connectedBody = rgPlayer;
        // rgObject.isKinematic = false;
        rgObject.useGravity = false;
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
        plyControls.isPushingOrPulling = false;
        courageAnim.SetBool("isPushing", false);
        // Debug.Log("Object Push And Pull Ended");
    }
}
