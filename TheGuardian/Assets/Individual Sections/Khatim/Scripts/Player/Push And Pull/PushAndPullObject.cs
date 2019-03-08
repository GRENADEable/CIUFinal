using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPullObject : MonoBehaviour
{
    private Rigidbody rg;

    void OnEnable()
    {
        PlayerControlTest.onObjectDetatchEvent += DetatchObjectEventReceived;
    }

    void OnDisable()
    {
        PlayerControlTest.onObjectDetatchEvent -= DetatchObjectEventReceived;
    }


    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void DetatchObjectEventReceived()
    {
        Destroy(this.gameObject.GetComponent<FixedJoint>());
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        rg.useGravity = true;
        Debug.LogWarning("Object Detached");
    }

}
