using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // public enum GameState { MainMenu, Started, Ended, Paused };
    // public GameState currGameState;
    // public bool isPlayerDead;
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

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        // Scene currScene = SceneManager.GetActiveScene();
        // string sceneName = currScene.name;

        // if (sceneName == "MainMenu")
        // {
        //     currGameState = GameState.MainMenu;
        // }

        // if (sceneName == "TheGuadianGameScene")
        // {
        //     currGameState = GameState.Started;
        // }

        // if (sceneName == "TheGuadianGameScene" && isPlayerDead)
        // {
        //     Debug.LogWarning("PlayerDead");
        // }

        // if (sceneName == "TheGuadianGameScene" && isGamePaused)
        // {

        // }
    }
}
