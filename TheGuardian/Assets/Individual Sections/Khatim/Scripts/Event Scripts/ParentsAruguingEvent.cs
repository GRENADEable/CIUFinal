using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsAruguingEvent : MonoBehaviour
{
    public float firstKeyMove;
    public float secondKeyMove;
    public delegate void SendEvents();
    public static event SendEvents onKeyMove;
    public float distance;
    public float keyDropEventStart;
    public GameObject player;
    public AudioSource parentsAud;

    [SerializeField]
    private float time;
    private bool keyMovedOnce;
    private bool keyMovedTwice;
    [SerializeField]
    private bool eventStarted;
    private Collider eventCol;

    void OnEnable()
    {
        parentsAud = GetComponent<AudioSource>();
        eventCol = GetComponent<Collider>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= keyDropEventStart)
        {
            eventStarted = true;
            if (eventStarted)
            {
                // time += Time.deltaTime;
                time = parentsAud.time;

                if (time >= firstKeyMove && onKeyMove != null && !keyMovedOnce)
                {
                    onKeyMove();
                    keyMovedOnce = true;
                }

                if (time >= secondKeyMove && onKeyMove != null && keyMovedOnce && /*/onKeyDropAudio != null*/ !keyMovedTwice)
                {
                    onKeyMove();
                    keyMovedTwice = true;
                    eventCol.enabled = false;
                }
            }
        }
        else if (distance >= keyDropEventStart)
            eventStarted = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            parentsAud.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            parentsAud.Pause();
    }
}
