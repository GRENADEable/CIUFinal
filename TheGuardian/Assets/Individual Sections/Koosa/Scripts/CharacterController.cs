using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       float x = Input.GetAxis("Mouse X");
        transform.Rotate(0, x, 0);
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 10;

        transform.Translate(0, 0, z);

    }
}
