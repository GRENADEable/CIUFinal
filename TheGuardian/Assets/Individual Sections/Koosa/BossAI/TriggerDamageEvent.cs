using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDamageEvent : MonoBehaviour
{
    public UnityEvent damageBoss;
    public UnityEvent killPlayer;
    public BehaviourTree BT;
    public bool entered = false;
    public delegate void SendEvents();
    public static event SendEvents onDeadPlayer;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageBoss" && BT.attacking)
        {
            damageBoss.Invoke();
            Debug.Log("yikes");
            entered = true;
            // other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Player" && BT.attacking)
        {
            killPlayer.Invoke();
            if (onDeadPlayer != null)
                onDeadPlayer();
        }
        if (other.gameObject.tag == "PickUp" && BT.attacking)
        {
            entered = true;
            other.gameObject.SetActive(false);
        }
    }
}