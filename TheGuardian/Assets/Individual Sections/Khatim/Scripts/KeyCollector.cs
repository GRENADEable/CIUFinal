using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField]
    private GameObject key;
    private PlayerControls ply;
    private Collider keyCollectorCol;
    public delegate void SendEventsToManager();
    public static event SendEventsToManager onKeyCounterUpdate;

    void Start()
    {
        ply = GameObject.FindObjectOfType<PlayerControls>();
        keyCollectorCol = GetComponent<Collider>();
    }

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
        key.transform.localEulerAngles = new Vector3(0f, 90f, 90f);
        key.transform.parent = this.transform;
        key.transform.position = this.transform.position;
        key.gameObject.tag = "Untagged";
        key.GetComponent<Rigidbody>().isKinematic = true;
        ply.ResetInteraction();
        ObjectPickup.onKeyDropEvent -= OnKeyDropEventReceived;
        this.keyCollectorCol.enabled = false;
        if (onKeyCounterUpdate != null) //Updates KeyCounter on GameManager GameObject
            onKeyCounterUpdate();

        Debug.Log("Player Added Key to Keyhole");
    }
}
