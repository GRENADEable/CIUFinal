using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnable : MonoBehaviour
{
    public Light lanternLight;
    public Collider lanternCol;

    public delegate void SendEvents();
    public static event SendEvents onFadeOut;

    void OnEnable()
    {
        lanternLight.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Interact") && onFadeOut != null)
        {
            onFadeOut();
            lanternLight.enabled = true;
            lanternCol.enabled = false;
        }
    }
}