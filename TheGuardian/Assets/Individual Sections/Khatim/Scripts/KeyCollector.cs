using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    public delegate void SendEventsToManager();
    public static event SendEventsToManager onKeyDropEvent;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PickUp")
        {
            if (onKeyDropEvent != null)
                onKeyDropEvent();

            Destroy(this.gameObject);
        }
    }
}
