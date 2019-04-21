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
    public static event SendEventsToManager onFirstKeyIllustration;
    // public static event SendEventsToManager onSecondKeyIllustration;
    // public static event SendEventsToManager onThirdKeyIllustration;

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
        key.transform.parent = this.transform;
        key.gameObject.tag = "Untagged";
        key.GetComponent<Rigidbody>().isKinematic = true;
        ply.ResetInteraction();
        ObjectPickup.onKeyDropEvent -= OnKeyDropEventReceived;
        this.keyCollectorCol.enabled = false;
        if (onKeyCounterUpdate != null) //Updates KeyCounter on GameManager GameObject
            onKeyCounterUpdate();

        // if (key.name == "Key" && onFirstKeyIllustration != null)
        //     onFirstKeyIllustration();

        // if (key.name == "Key_2" && onSecondKeyIllustration != null)
        //     onSecondKeyIllustration();

        // if (key.name == "Key_3" && onThirdKeyIllustration != null)
        //     onThirdKeyIllustration();

        Debug.Log("Player Added Key to Keyhole");
    }
}
