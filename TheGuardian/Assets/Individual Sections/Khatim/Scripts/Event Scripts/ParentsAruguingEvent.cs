using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsAruguingEvent : MonoBehaviour
{
    public float firstKeyMove;
    public float secondKeyMove;

    public delegate void SendMessageToManagers();
    public static event SendMessageToManagers onKeyMove;

    [SerializeField]
    private float time;
    private bool keyMovedOnce;
    private bool keyMovedTwice;
    private bool keyMovedThrice;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            time += Time.deltaTime;

            if (time >= firstKeyMove && onKeyMove != null && !keyMovedOnce)
            {
                onKeyMove();
                keyMovedOnce = true;
            }


            if (time >= secondKeyMove && onKeyMove != null && keyMovedOnce && !keyMovedTwice)
            {
                onKeyMove();
                keyMovedTwice = true;
            }
        }
    }
}
