using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public static GameManager instance;
    public bool isPlayerDead;
    public int keyCounter;
    public GameObject endHallwayDoor;

    public delegate void SendMessageToManagers();
    public static event SendMessageToManagers onPaintingsAwakeMessage;

    void OnEnable()
    {
        ObjectThrowing.onKeyDropEvent += OnKeyDropEventReceived;
    }

    void OnDisable()
    {
        ObjectThrowing.onKeyDropEvent -= OnKeyDropEventReceived;
    }
    // void Awake()
    // {
    //     //Makes Script Singleton
    //     if (instance == null)
    //         instance = this;

    //     else if (instance != null)
    //         Destroy(gameObject);

    //     // DontDestroyOnLoad(this.gameObject);
    // }

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

        if (keyCounter >= 2 && onPaintingsAwakeMessage != null)
        {
            onPaintingsAwakeMessage();
        }
    }

    void OnKeyDropEventReceived()
    {
        keyCounter++;
        Debug.LogWarning("Key Received");
    }
}
