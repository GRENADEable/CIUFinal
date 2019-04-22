using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysicsChanger : MonoBehaviour
{
    public float raycastDistance;
    private Rigidbody rg;
    private RaycastHit hit;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.blue);

        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.forward * raycastDistance, Color.blue);

        bool objCheckUp = Physics.Raycast(transform.position, Vector3.up, out hit, raycastDistance);
        bool objCheckDown = Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance);
        bool objCheckForward = Physics.Raycast(transform.position, Vector3.forward, out hit, raycastDistance);
        bool objCheckBackward = Physics.Raycast(transform.position, Vector3.back, out hit, raycastDistance);

        if (objCheckUp || objCheckDown || objCheckForward || objCheckBackward)
        {
            // Debug.Log("Object Grounded");
        }
    }
}
