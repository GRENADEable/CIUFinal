using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaster : MonoBehaviour
{
    public delegate void KillPlayer();
    public static event KillPlayer onKillPlayerEvent;

    void Attack()
    {
        if (onKillPlayerEvent != null)
            onKillPlayerEvent();
    }
}
