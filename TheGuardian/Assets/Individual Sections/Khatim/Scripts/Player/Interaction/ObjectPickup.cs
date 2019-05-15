using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public delegate void SendEventToManager();
    public static event SendEventToManager onKeyDropEvent;
    public Transform pivotDummyForObjectPickup;
    public Rigidbody pickedUpObj;
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
        pickedUpObj = interactCol.GetComponentInParent<Rigidbody>();
        courageAnim.SetBool("isInteracting", true);

        // if (interactCol.GetComponent<FixedJoint>() == null)
        //     interactCol.gameObject.AddComponent(typeof(FixedJoint));

        // courageAnim.SetBool("isInteracting", true);
        // plyControls.isPickingObject = true;
        // objectFixedJoint = interactCol.GetComponent<FixedJoint>();
        // interactCol.transform.position = pivotDummyForObjectPickup.position;
        // interactCol.transform.localEulerAngles = new Vector3(0f, 0, 90f);
        // objectFixedJoint.connectedBody = pivotDummyForObjectPickup;
        // rgObject.useGravity = false;
        Debug.Log("Object Picked Up");
    }
    public override void UpdateInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            courageAnim.SetBool("isInteracting", false);
            courageAnim.SetTrigger("throw");

            // courageAnim.SetBool("isInteracting", false);
            // courageAnim.SetTrigger("throw");

            // Destroy(objectFixedJoint);
            // rgObject.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
            // rgObject.useGravity = true;
            // plyControls.isPickingObject = false;
            Debug.Log("Object Thrown");
        }
    }
    public override void EndInteraction()
    {
        courageAnim.SetBool("isInteracting", false);
        courageAnim.SetTrigger("drop");
        base.EndInteraction();

        // courageAnim.SetBool("isInteracting", false);
        // courageAnim.SetTrigger("drop");
        // rgObject.useGravity = true;
        // Destroy(objectFixedJoint);
        // objectFixedJoint = null;
        // base.EndInteraction();
        // plyControls.isPickingObject = false;
        Debug.Log("Object Pickup Ended");
    }

    void PickObject()
    {
        plyControls.isPickingObject = true;
        pickedUpObj.useGravity = false;
        pickedUpObj.isKinematic = true;
        pickedUpObj.transform.parent = pivotDummyForObjectPickup;
        pickedUpObj.transform.localPosition = new Vector3(0f, -0.0045f, -0.0009f);
        pickedUpObj.transform.localEulerAngles = new Vector3(125.973f, -104.731f, -257.542f);
    }

    void DropObject()
    {
        pickedUpObj.transform.parent = null;
        pickedUpObj.useGravity = true;
        pickedUpObj.isKinematic = false;
        pickedUpObj = null;
        plyControls.isPickingObject = false;

        if (onKeyDropEvent != null)
            onKeyDropEvent();
    }

    void ThrowObject()
    {
        pickedUpObj.transform.parent = null;
        pickedUpObj.isKinematic = false;
        pickedUpObj.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
        pickedUpObj.useGravity = true;
        pickedUpObj = null;
        plyControls.isPickingObject = false;
    }
}
