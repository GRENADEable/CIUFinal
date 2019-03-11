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
    }

    public void NewGame()
    {
        SceneManager.LoadScene("AtticAndNurseryLevel");
        Time.timeScale = 1;
    }

    public void AtticLevel()
    {
        SceneManager.LoadScene("AtticAndNurseryLevel");
    }

    public void HallwayLevel()
    {
        SceneManager.LoadScene("HallwayLevel");
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
