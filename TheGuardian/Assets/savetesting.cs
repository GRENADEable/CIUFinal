using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savetesting : SaveSystem {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if( Input.GetKeyDown(KeyCode.S))
        {
            SaveCharacter(this.gameObject, 0);
        }

        if( Input.GetKeyDown(KeyCode.L))
        {
           transform.position = Loadharacter(0);
        }
    }
}
