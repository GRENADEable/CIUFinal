using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheesePickup : PlayerInteraction
{
    public Transform pivotDummyForObjectPickup;
    public Rigidbody pickedUpObj;
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
        courageAnim.SetBool("isInteractingCheese", true);
        Debug.Log("Object Picked Up");
    }
    public override void UpdateInteraction()
    {
        if (Input.GetButton("Throw"))
        {
            courageAnim.SetBool("isInteractingCheese", false);
            courageAnim.SetTrigger("throw");
            Debug.Log("Object Thrown");
        }
    }
    public override void EndInteraction()
    {
        courageAnim.SetBool("isInteractingCheese", false);
        courageAnim.SetTrigger("drop");
        base.EndInteraction();
        Debug.Log("Cheese Pickup Ended");
    }

    void PickCheese()
    {
        plyControls.isPickingObject = true;
        pickedUpObj.useGravity = false;
        pickedUpObj.isKinematic = true;
        pickedUpObj.transform.parent = pivotDummyForObjectPickup;
        pickedUpObj.transform.localPosition = new Vector3(-0.012f, -0.0343f, 0.0366f);
        pickedUpObj.transform.localEulerAngles = new Vector3(-43.143f, -53.571f, 40.085f);
    }

    void ThrowCheese()
    {
        pickedUpObj.transform.parent = null;
        pickedUpObj.isKinematic = false;
        pickedUpObj.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
        pickedUpObj.useGravity = true;
        pickedUpObj = null;
        plyControls.isPickingObject = false;
    }

    void DropCheese()
    {
        pickedUpObj.transform.parent = null;
        pickedUpObj.useGravity = true;
        pickedUpObj.isKinematic = false;
        pickedUpObj = null;
        plyControls.isPickingObject = false;

    }
}
