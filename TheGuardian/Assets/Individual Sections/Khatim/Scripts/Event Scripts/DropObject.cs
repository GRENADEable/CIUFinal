using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    private Rigidbody rg;

    void OnEnable()
    {
        ObjectThrowing.onObjectDropEvent += DropObjectEventReceived;
    }

    void OnDisable()
    {
        ObjectThrowing.onObjectDropEvent -= DropObjectEventReceived;
    }

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void DropObjectEventReceived()
    {
        Destroy(this.gameObject.GetComponent<FixedJoint>());
        rg.useGravity = true;
        Debug.LogWarning("Object Dropped");
    }
}
