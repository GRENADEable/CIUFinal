using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField]
    private GameObject key;
    public delegate void SendEventsToManager();
    public static event SendEventsToManager onKeyCounterUpdate;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            key = other.gameObject;
            ObjectPickup.onKeyDropEvent += OnKeyDropEventReceived;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        {
            key = null;
            ObjectPickup.onKeyDropEvent -= OnKeyDropEventReceived;
        }
    }

    void OnKeyDropEventReceived()
    {
        key.transform.parent = this.transform;
        // key.GetComponent<Rigidbody>().useGravity = false;
        if (onKeyCounterUpdate != null) //Updates KeyCounter on GameManager GameObject
            onKeyCounterUpdate();

        Debug.Log("Player Added Key to Keyhole");
    }
}
