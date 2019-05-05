using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Collider interactCol;
    public Rigidbody rgPlayer;
    public Rigidbody rgObject;
    private LightMechanic playerLight;

    public virtual void StartInteraction()
    {
        rgObject = interactCol.GetComponent<Rigidbody>();
        playerLight = GetComponent<LightMechanic>();
        playerLight.match.SetActive(false);
        playerLight.lightOn = false;
        playerLight.enabled = false;
        // any commog code .... 
    }

    public virtual void UpdateInteraction()
    {

    }

    public virtual void EndInteraction()
    {
        playerLight.enabled = true;
        rgObject = null;
    }
}