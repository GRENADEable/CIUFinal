using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeClimbing : PlayerInteraction
{
    public override void StartInteraction()
    {
        base.StartInteraction();
        Debug.LogWarning("Rope Climibing Started");
    }

    public override void UpdateInteraction()
    {
        Debug.LogWarning("Rope Climibing In Progress");
    }

    public override void EndInteraction()
    {
        Debug.LogWarning("Rope Climibing Ended");
    }
}
