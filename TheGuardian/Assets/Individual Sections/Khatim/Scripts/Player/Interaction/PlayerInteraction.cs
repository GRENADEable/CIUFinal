using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Collider interactCol;
    public Rigidbody rgPlayer;
    public Rigidbody rgObject;

    public virtual void StartInteraction()
    {
        rgPlayer = GetComponent<Rigidbody>();
        rgObject = interactCol.GetComponent<Rigidbody>();
        // any commog code .... 
    }

    public virtual void UpdateInteraction()
    {

    }

    public virtual void EndInteraction()
    {

    }
}