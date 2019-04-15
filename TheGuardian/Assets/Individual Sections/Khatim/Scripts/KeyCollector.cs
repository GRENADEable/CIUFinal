using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ObjectPickup.onKeyDrop += OnKeyDropEventReceived;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ObjectPickup.onKeyDrop -= OnKeyDropEventReceived;
        }
    }

    void OnKeyDropEventReceived()
    {
        Debug.Log("Player Added Key to Keyhole");
    }
}
