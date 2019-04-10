using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeClimbing : PlayerInteraction
{
    public override void StartInteraction()
    {
        base.StartInteraction();
        Debug.Log("Rope Climibing Started");
    }

    public override void UpdateInteraction()
    {
        Debug.Log("Rope Climibing In Progress");
    }

    public override void EndInteraction()
    {
        Debug.Log("Rope Climibing Ended");
    }
}
