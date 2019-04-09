using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public override void StartInteraction()
    {
        base.StartInteraction();
        Debug.LogWarning("Object Pickup Started");
    }

    public override void UpdateInteraction()
    {
        
        Debug.LogWarning("Object Pickup In Progress");
    }

    public override void EndInteraction()
    {
        Debug.LogWarning("Object Pickup Ended");
    }
}
