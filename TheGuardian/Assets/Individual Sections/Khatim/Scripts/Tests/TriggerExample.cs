using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    private EventManager eventMasterScript;

    void Start()
    {
        SetInitialReferences();
    }
    void OnTriggerEnter(Collider other)
    {
        eventMasterScript.CallMyGeneralEvent();
    }

    void SetInitialReferences()
    {
        eventMasterScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventManager>();
    }
}
