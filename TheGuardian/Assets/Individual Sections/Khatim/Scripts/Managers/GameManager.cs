using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayerDead;
    public int keyCounter;
    public GameObject endHallwayDoor;

    public delegate void SendMessageToPlayer();
    public SendMessageToPlayer onSendMessage;

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
        if (keyCounter >= 4)
        {
            endHallwayDoor.SetActive(false);
        }
        else
        {
            endHallwayDoor.SetActive(true);
        }
    }

    void OnKeyDropEventReceived()
    {
        keyCounter++;
        Debug.LogWarning("Key Received");
    }
}
