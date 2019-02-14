using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractEnemyEvent : MonoBehaviour
{
    public delegate void DistractEnemy();
    public static event DistractEnemy OnDistractEnemy;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (OnDistractEnemy != null)
            {
                OnDistractEnemy();
            }
        }
    }
}
