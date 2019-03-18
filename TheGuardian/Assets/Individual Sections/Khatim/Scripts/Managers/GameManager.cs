using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayerDead;
    
    public delegate void SendMessageToPlayer();
    public SendMessageToPlayer onSendMessage;

    // public static event SendMessageToPlayer onChangePlayerMovementVariables;

    // public bool isGamePaused;
    // public bool isGameOver;
    // public delegate void ChangeState();

    void Awake()
    {
        //Makes Script Singleton
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        // DontDestroyOnLoad(this.gameObject);
    }
}
