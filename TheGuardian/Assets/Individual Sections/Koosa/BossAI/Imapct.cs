using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Imapct : MonoBehaviour
{
    public UnityEvent distractEnemy;
    public float desiredMagnitude;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > desiredMagnitude)
        {
            Debug.Log("NICE NICE");
            distractEnemy.Invoke();
        }
    }
}
