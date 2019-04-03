﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // public static SceneManage instance;


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
        SceneManager.LoadScene("AtticAndNurseryLevel");
        Time.timeScale = 1;
    }

    public void AtticLevel()
    {
        SceneManager.LoadScene("AtticAndNurseryLevel");
        Time.timeScale = 1;
    }

    public void HallwayLevel()
    {
        SceneManager.LoadScene("HallwayLevel");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.LogWarning("Game Closed");
    }
}
