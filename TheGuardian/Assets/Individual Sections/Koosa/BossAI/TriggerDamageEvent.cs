﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDamageEvent : MonoBehaviour
{
    public UnityEvent damageBoss;
    public UnityEvent killPlayer;
    public BehaviourTree BT;
    public bool entered = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageBoss" && BT.attacking)
        {
            damageBoss.Invoke();
            Destroy(other);
            Debug.Log("yikes");
            entered = true;
            other.gameObject.SetActive(false);
        }
         if (other.gameObject.tag == "Player" && BT.attacking)
        {
            killPlayer.Invoke();
        }
        if (other.gameObject.tag == "PickUp" && BT.attacking)
        {
            entered = true;
            other.gameObject.SetActive(false);
        }
    }

}