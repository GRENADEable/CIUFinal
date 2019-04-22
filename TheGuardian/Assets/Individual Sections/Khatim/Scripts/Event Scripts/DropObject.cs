using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    private Rigidbody rg;

    void OnEnable()
    {
        // ObjectThrowing.onObjectDropEvent += DropObjectEventReceived;
        rg = GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        // ObjectThrowing.onObjectDropEvent -= DropObjectEventReceived;
    }

    void DropObjectEventReceived()
    {
        // Destroy(this.gameObject.GetComponent<FixedJoint>());
        // rg.useGravity = true;
        Destroy(this.gameObject.GetComponent<FixedJoint>());
        rg.useGravity = true;
        Debug.LogWarning("Object Dropped");
    }
}
