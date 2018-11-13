using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappelingHook : MonoBehaviour
{
    #region Public Variables

    [Header("GameObjects")]

    public GameObject grappelingHook;
    public GameObject grappelingHookHolder;

    [Header("Floats")]

    public float grappelingHookLaunchSpeed;
    public float playerLaunchSpeed;

    public float maxDistance;
    public float curDistance; // distance while the grappeling hook is stationary
    public float distanceFromHook; // distance while grappeling hook is in motion

    [Header("Bools")]

    public bool launched;
    public bool hooked;
    #endregion

    #region Unity Callbacks
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !launched)
            launched = true;

        if (launched && !hooked)
        {
            HookLaunch();

            if (curDistance >= maxDistance)
                HookReturn();
        }
        
        if(hooked)
        {
            PlayerLaunch();

            if (distanceFromHook < 1)
                HookReturn();
        }
    }
    #endregion

    #region My Functions
    public void HookLaunch()
    {
        grappelingHook.transform.Translate(Vector3.forward * grappelingHookLaunchSpeed * Time.deltaTime);
        curDistance = Vector3.Distance(transform.position, grappelingHook.transform.position);
    }

    public void HookReturn()
    {
        grappelingHook.transform.position = grappelingHookHolder.transform.position;
        hooked = false;
        launched = false;
    }

    public void PlayerLaunch()
    {
        transform.position = Vector3.MoveTowards(transform.position, grappelingHook.transform.position, playerLaunchSpeed * Time.deltaTime);
        distanceFromHook = Vector3.Distance(transform.position, grappelingHook.transform.position);
    }
    #endregion

}
