using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveKeyTesting : SaveSystem
{
    public GameObject[] keys;
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.V))
        //     SaveKeyPosition(keys[1].gameObject, 0);

        // if (Input.GetKeyDown(KeyCode.B))
        //     SaveKeyPosition(keys[2].gameObject, 1);

        // if (Input.GetKeyDown(KeyCode.N))
        //     SaveKeyPosition(keys[3].gameObject, 2);
        if (Input.GetKeyDown(KeyCode.N))
        {
            SaveKeyPosition(keys[0].gameObject, 0);
            SaveKeyPosition(keys[1].gameObject, 1);
            SaveKeyPosition(keys[2].gameObject, 2);
            Debug.Log("All Keys Saved");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            keys[0].transform.position = LoadKeyPosition(0);
            keys[1].transform.position = LoadKeyPosition(1);
            keys[2].transform.position = LoadKeyPosition(2);
            Debug.Log("All Keys Loaded");
        }
    }
}
