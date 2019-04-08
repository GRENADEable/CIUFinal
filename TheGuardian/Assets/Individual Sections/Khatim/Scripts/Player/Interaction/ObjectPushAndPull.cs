using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPushAndPull : PlayerInteraction
{
    public override void StartInteraction()
    {
        base.StartInteraction();
        Debug.LogWarning("Object Push And Pull Started");
    }

    public override void UpdateInteraction()
    {
        Debug.LogWarning("Object Push And Pull In Progress");
    }

    public override void EndInteraction()
    {
        Debug.LogWarning("Object Push And Pull Ended");

    }
}
