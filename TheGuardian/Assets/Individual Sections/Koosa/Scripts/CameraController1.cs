﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{

    public Transform lookAtTransform;
    public Transform playerCameraTransform;

    public float zDistanceFromLookAt;
    public float yDistanceFromLookAt;
    public float xDistanceFromLookAt;
    public float cameraSmoothing;

    public List<Transform> targetTransforms = new List<Transform>();
    public List<Transform> targetsToAdd = new List<Transform>();

    private Camera playerCamera;

    private Vector3 cameraVelocity;
    public float distance;

    public bool ignore = false;


    void Start()
    {
        playerCameraTransform = this.transform;
        playerCamera = gameObject.GetComponent<Camera>();
        lookAtTransform = GameObject.FindObjectOfType<PlayerControlTest>().transform;
    }

    void LateUpdate()
    {
        Vector3 leverPosition = new Vector3(66, 27, 616);
        distance = lookAtTransform.position.z - targetsToAdd[2].position.z;
        Vector3 Offset = new Vector3(xDistanceFromLookAt, yDistanceFromLookAt, zDistanceFromLookAt); 
        //Vector3 center = GetEncapsulatingBounds().center;

        if (targetTransforms.Count == 1 && !ignore)
            transform.position = Offset + lookAtTransform.position;
        if (lookAtTransform.GetComponent<PlayerControlTest>().iniatePuzzleLever)
        {
            transform.position = Vector3.SmoothDamp(transform.position, leverPosition, ref cameraVelocity, cameraSmoothing);
            ignore = true;
        }

        /*
         if ((lookAtTransform.position.z - targetsToAdd[2].position.z) > -50)
         {
            foreach (Transform i in targetsToAdd)
            {
                if (!targetTransforms.Contains(i))
                    targetTransforms.Add(i);
            }
            transform.position = Offset + lookAtTransform.position+ center;
            //transform.position = Vector3.SmoothDamp(transform.position, (lookAtTransform.position + Offset + GetEncapsulatingBounds().center), ref cameraVelocity, cameraSmoothing);
        }
    }

    public Bounds GetEncapsulatingBounds()
    {
        Bounds bounds = new Bounds(targetsToAdd[0].position, Vector3.zero);

        for(int i = 0; i < targetsToAdd.Count; i++)
        {
            bounds.Encapsulate(targetsToAdd[i].position);
        }

        return bounds;

    }
    */
    }
}