using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainmenuPanel;
    public GameObject settingsPanel;

    [Header("In Game UI")]
    public GameObject deathScreen;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject cheatPanel;

    [Header("References Obejcts")]
    public GameObject levelOneTitleText;
    private GameManager gm;

    // public delegate void SendEventToPlayer();
    // public static event SendEventToPlayer onSendEvent;

    void OnEnable()
    {
        RatFSM.onDeadPlayerScreen += OnDeadPlayerScreenReceived;
        RatBlockerFSM.onDeadPlayerScreen += OnDeadPlayerScreenReceived;
        PaintingsAI.onPlayerDeath += OnDeadPlayerScreenReceived;
        gm = GetComponent<GameManager>();
    }

    void OnDisable()
    {
        RatFSM.onDeadPlayerScreen -= OnDeadPlayerScreenReceived;
        RatBlockerFSM.onDeadPlayerScreen -= OnDeadPlayerScreenReceived;
        PaintingsAI.onPlayerDeath -= OnDeadPlayerScreenReceived;
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
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");

        if (pausePanel != null && cheatPanel != null && deathScreen != null)
        {
            pausePanel.SetActive(false);
            cheatPanel.SetActive(false);
            deathScreen.SetActive(false);
        }

        if (levelOneTitleText != null)
        {
            levelOneTitleText.SetActive(true);
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
        gm.isPlayerDead = true;
        deathScreen.SetActive(true);
        Debug.LogWarning("Death Screen UI Activated");
    }
}
