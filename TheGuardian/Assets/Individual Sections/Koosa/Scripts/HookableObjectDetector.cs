using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookableObjectDetector : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider hookedObject)
    {
        if(hookedObject.tag == "Hookable")
        {
            player.GetComponent<GrappelingHook>().hooked = true;
        }
    }
}
