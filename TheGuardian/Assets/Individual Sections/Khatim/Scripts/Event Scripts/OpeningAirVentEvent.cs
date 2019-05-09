using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningAirVentEvent : MonoBehaviour
{
    public Transform airVentMovePos;
    public GameObject key_2;

    public delegate void SendEvents();
    public static event SendEvents onVentIllustration;

    void OnEnable()
    {
        key_2.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E) && onVentIllustration != null)
        {
            onVentIllustration();
            transform.position = airVentMovePos.position;
            GetComponent<Collider>().enabled = false;
            key_2.SetActive(true);
        }
    }
}
