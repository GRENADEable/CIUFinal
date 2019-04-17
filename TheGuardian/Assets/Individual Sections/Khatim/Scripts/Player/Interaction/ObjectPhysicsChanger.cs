using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysicsChanger : MonoBehaviour
{
    private Rigidbody rg;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rg.velocity == Vector3.zero)
            rg.isKinematic = true;
        // else if (rg.velocity != Vector3.zero)
        //     rg.isKinematic = true;
    }
}
