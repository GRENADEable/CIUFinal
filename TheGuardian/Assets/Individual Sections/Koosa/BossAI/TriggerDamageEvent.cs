using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDamageEvent : MonoBehaviour
{
    public UnityEvent damageBoss;
    public UnityEvent killPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageBoss")
        {
            damageBoss.Invoke();
        }
        if (other.gameObject.tag == "Player")
        {
            killPlayer.Invoke();
        }
    }

}
