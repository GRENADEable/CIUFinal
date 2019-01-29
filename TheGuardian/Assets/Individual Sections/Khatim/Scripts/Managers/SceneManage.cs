using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("TheGuadianGameScene");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.LogWarning("Game Closed");
    }
}
