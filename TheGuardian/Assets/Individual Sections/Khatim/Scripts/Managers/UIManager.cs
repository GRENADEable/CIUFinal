using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : GameManager
{
    [Header("Main Menu UI")]
    public GameObject mainmenuPanel;
    public GameObject settingsPanel;

    [Header("Main Menu UI")]
    public GameObject deathScreen;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject cheatPanel;

    // public delegate void SendEventToPlayer();
    // public static event SendEventToPlayer onSendEvent;

    void OnEnable()
    {
        RatFSM.onDeadPlayerScreen += OnDeadPlayerScreenReceived;
        RatBlockerFSM.onDeadPlayerScreen += OnDeadPlayerScreenReceived;
    }

    void OnDisable()
    {
        RatFSM.onDeadPlayerScreen -= OnDeadPlayerScreenReceived;
        RatBlockerFSM.onDeadPlayerScreen -= OnDeadPlayerScreenReceived;

    }
    void Awake()
    {
        if (mainmenuPanel != null && settingsPanel != null)
        {
            mainmenuPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }

        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        cheatPanel = GameObject.FindGameObjectWithTag("CheatPanel");

        if (pausePanel != null && cheatPanel != null)
        {
            pausePanel.SetActive(false);
            cheatPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (cheatPanel != null && pausePanel != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !cheatPanel.activeSelf)
                PauseorUnpause();

            if (Input.GetKeyDown(KeyCode.Tab) && !pausePanel.activeSelf)
                CheatPanelToggle();
        }
    }

    public void PauseorUnpause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);

        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void CheatPanelToggle()
    {
        cheatPanel.SetActive(!cheatPanel.activeSelf);
    }

    public void Settings()
    {
        mainmenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SettingsToMainMenu()
    {
        settingsPanel.SetActive(false);
        mainmenuPanel.SetActive(true);
    }

    void OnDeadPlayerScreenReceived()
    {
        isPlayerDead = true;
        deathScreen.SetActive(true);
    }
}
