using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPlayerDead;
    public int keyCounter;
    public GameObject endHallwayDoor;

    public delegate void SendMessageToManagers();
    public static event SendMessageToManagers onPaintingsAwakeMessage;

    void OnEnable()
    {
        KeyCollector.onKeyDropEvent += OnKeyDropEventReceived;
    }

    void OnDisable()
    {
        KeyCollector.onKeyDropEvent -= OnKeyDropEventReceived;
    }

    void OnDestroy()
    {
        KeyCollector.onKeyDropEvent -= OnKeyDropEventReceived;
    }

    void Update()
    {
        if (endHallwayDoor != null)
        {
            if (keyCounter >= 4)
            {
                endHallwayDoor.SetActive(false);
            }
            else
            {
                endHallwayDoor.SetActive(true);
            }
        }

        if (keyCounter >= 1 && onPaintingsAwakeMessage != null)
        {
            onPaintingsAwakeMessage();
        }
    }

    void OnKeyDropEventReceived()
    {
        keyCounter++;
        Debug.Log("Key Received");
    }
}
