using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public delegate void SendEventToManager();
    public static event SendEventToManager onKeyDropEvent;
    public Rigidbody pivotDummyForObjectPickup;
    public FixedJoint objectFixedJoint;
    public float throwingForce;
    private PlayerControls plyControls;
    private Animator courageAnim;

    void OnEnable()
    {
        plyControls = GetComponent<PlayerControls>();
        courageAnim = GetComponent<Animator>();
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        courageAnim.SetBool("isInteracting", true);
        plyControls.isPickingObject = true;
        rgObject.useGravity = false;
        rgObject.isKinematic = true;
        interactCol.transform.parent = pivotDummyForObjectPickup.transform;
        interactCol.transform.position = pivotDummyForObjectPickup.position;
        interactCol.transform.localEulerAngles = new Vector3(0f, 0, 90f);

        // if (interactCol.GetComponent<FixedJoint>() == null)
        //     interactCol.gameObject.AddComponent(typeof(FixedJoint));

        // courageAnim.SetBool("isInteracting", true);
        // objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        // interactCol.transform.position = pivotDummyForObjectPickup.position;
        // interactCol.transform.localEulerAngles = new Vector3(0f, 0, 90f);
        // objectFixedJoint.connectedBody = pivotDummyForObjectPickup;
        // rgObject.useGravity = false;
        // interactCol.GetComponent<Collider>().isTrigger = true;
        // Debug.Log("Object Picked Up");
    }
    public override void UpdateInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            courageAnim.SetBool("isInteracting", false);
            courageAnim.SetTrigger("throw");
            interactCol.transform.parent = null;
            rgObject.isKinematic = false;
            rgObject.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            rgObject.useGravity = true;
            plyControls.isPickingObject = false;

            // courageAnim.SetBool("isInteracting", false);
            // courageAnim.SetTrigger("throw");
            // Destroy(objectFixedJoint);
            // rgObject.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            // rgObject.useGravity = true;
            // // interactCol.GetComponent<Collider>().isTrigger = false;
            // plyControls.isPickingObject = false;
            // // Debug.Log("Object Thrown");
        }
    }
    public override void EndInteraction()
    {
        courageAnim.SetBool("isInteracting", false);
        courageAnim.SetTrigger("drop");
        rgObject.useGravity = true;
        rgObject.isKinematic = false;
        interactCol.transform.parent = null;
        base.EndInteraction();
        plyControls.isPickingObject = false;

        // courageAnim.SetBool("isInteracting", false);
        // courageAnim.SetTrigger("drop");
        // rgObject.useGravity = true;
        // Destroy(objectFixedJoint);
        // objectFixedJoint = null;
        // interactCol.GetComponent<Collider>().isTrigger = false;
        // base.EndInteraction();
        // plyControls.isPickingObject = false;
        // Debug.Log("Object Pickup Ended");

        if (onKeyDropEvent != null)
            onKeyDropEvent();
    }
}
