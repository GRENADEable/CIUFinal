using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyDrop : MonoBehaviour
{
    public AudioSource keyDropSource;

    void OnEnable()
    {
        keyDropSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            keyDropSource.Play();
            this.enabled = false;
        }
    }
}
