using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savetesting : SaveSystem
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveCharacter(this.gameObject, 0);
            Debug.Log("Saved Player Position");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = Loadharacter(0);
            Debug.Log("Loaded Player Position");
        }
    }
}
