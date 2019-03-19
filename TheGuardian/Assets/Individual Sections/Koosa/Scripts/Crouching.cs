using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    public CharacterController ch;
    public float startHeight;

    public void Start()
    {
        startHeight = ch.height;
    }

    public void Update()
    {
        float newH = startHeight;

        if (Input.GetKey("c"))
        {
            // calculate new height
            newH = 0.5f * startHeight;
        }

        var lastHeight = ch.height;

        // lerp CharacterController height
        ch.height = Mathf.Lerp(ch.height, newH, 5.0f * Time.deltaTime);

        // fix vertical position
        Vector3 playerVector = transform.position;
        playerVector.y += (ch.height - lastHeight) * 0.5f;
        transform.position = new Vector3(transform.position.x, playerVector.y, transform.position.z);
    }
}

