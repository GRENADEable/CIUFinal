using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningAirVentEvent : MonoBehaviour
{
    public Transform airVentMovePos;

    public delegate void SendEvents();
    public static event SendEvents onVentIllustration;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E) && onVentIllustration != null)
        {
            onVentIllustration();
            transform.position = airVentMovePos.position;
            GetComponent<Collider>().enabled = false;
        }
    }
}
