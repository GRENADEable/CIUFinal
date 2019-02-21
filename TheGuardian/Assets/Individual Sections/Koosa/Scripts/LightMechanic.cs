using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMechanic : MonoBehaviour
{
    public float fuel;
    public int matches;
    public GameObject match;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {


        //picking up a match comes here
        Matchlight();
	}

    void Matchlight()
    {
        if(Input.GetKeyDown(KeyCode.J) && matches>0 && !match.activeSelf)
        {
            if(fuel > 0)
            {
                match.SetActive(true);
            }

            if (fuel < 0)
            {
                match.SetActive(false);
            }
        }

        if(match.activeSelf)
        {
            fuel -= Time.deltaTime;
        }
        if (fuel <= 0 )
        {
            matches = 0 ;
            match.SetActive(false);
        }
    }
}
