using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public delegate void SendEvents();
    public static event SendEvents onStopAudioForMainMenu;

    void OnEnable()
    {
        PlayerControls.onChangeLevelToHallway += HallwayLevel;
    }

    void OnDisable()
    {
        PlayerControls.onChangeLevelToHallway -= HallwayLevel;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("AtticLevel");
        Time.timeScale = 1;

        // if (onAtticSceneMusic != null)
        //     onAtticSceneMusic();
    }

    public void AtticLevel()
    {
        SceneManager.LoadScene("AtticLevel");
        Time.timeScale = 1;

        // if (onAtticSceneMusic != null)
        //     onAtticSceneMusic();
    }

    public void HallwayLevel()
    {
        SceneManager.LoadScene("HallwayLevel");
        Time.timeScale = 1;

        // if (onHallwaySceneMusic != null)
        //     onHallwaySceneMusic();
    }

    public void NurseryLevel()
    {
        SceneManager.LoadSceneAsync("NurseryLevel");
        Time.timeScale = 1;

        // if (onNurserySceneMusic != null)
        //     onNurserySceneMusic();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;

        // if (onStopAudioForMainMenu != null)
        //     onStopAudioForMainMenu();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.LogWarning("Game Closed");
    }
}
