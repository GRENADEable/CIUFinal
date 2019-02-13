using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerControls ply;
    void Start()
    {

    }

    void Update()
    {
        if (ply.col != null && Input.GetKeyDown(KeyCode.E) && ply.col.gameObject.tag == "Rope")
        {
            ply.ColliderHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ply && other.tag == "Rope")
        {
            ply.col = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ply && other.tag == "Rope")
        {
            ply.col = null;
        }
    }
}
