using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu UI")]
    public GameObject mainmenuPanel;

    [Header("In Game UI")]
    public GameObject changeLevelPanel;
    [Header("Hallway UI")]
    public GameObject firstKeyIllustration;
    public GameObject secondKeyIllustration;
    public GameObject thirdKeyIllustration;
    public GameObject ventKeyIllustration;

    private GameObject deathScreen;
    private GameObject pausePanel;
    private GameObject cheatPanel;

    [Header("References Obejcts")]
    public GameObject levelTitleText;

    void OnEnable()
    {
        PlayerControls.onChangeLevelText += OnChangeLevelTextReceived;
        PlayerControls.onDeadPlayer += OnDeadPlayerReceived;
        DollsFSM.onDeadPlayer += OnDeadPlayerReceived;
        TriggerDamageEvent.onDeadPlayer += OnDeadPlayerReceived;

        RatFSM.onPlayerDeath += OnDeadPlayerReceived;
        RatBlockerFSM.onPlayerDeath += OnDeadPlayerReceived;

        GameManager.onFirstKeyIllustration += OnFirstKeyIllustrationReceived;
        GameManager.onSecondKeyIllustration += OnSecondKeyIllustrationReceived;
        GameManager.onThirdKeyIllustration += OnThirdKeyIllustrationReceived;

        OpeningAirVentEvent.onVentIllustration += OnVentIllustrationReceived;
    }

    void OnDisable()
    {
        PlayerControls.onChangeLevelText -= OnChangeLevelTextReceived;
        PlayerControls.onDeadPlayer -= OnDeadPlayerReceived;
        DollsFSM.onDeadPlayer -= OnDeadPlayerReceived;
        TriggerDamageEvent.onDeadPlayer -= OnDeadPlayerReceived;

        RatFSM.onPlayerDeath -= OnDeadPlayerReceived;
        RatBlockerFSM.onPlayerDeath -= OnDeadPlayerReceived;

        GameManager.onFirstKeyIllustration -= OnFirstKeyIllustrationReceived;
        GameManager.onSecondKeyIllustration -= OnSecondKeyIllustrationReceived;
        GameManager.onThirdKeyIllustration -= OnThirdKeyIllustrationReceived;

        OpeningAirVentEvent.onVentIllustration -= OnVentIllustrationReceived;
    }

    void OnDestroy()
    {
        PlayerControls.onChangeLevelText -= OnChangeLevelTextReceived;
        PlayerControls.onDeadPlayer -= OnDeadPlayerReceived;
        DollsFSM.onDeadPlayer -= OnDeadPlayerReceived;
        TriggerDamageEvent.onDeadPlayer -= OnDeadPlayerReceived;

        RatFSM.onPlayerDeath -= OnDeadPlayerReceived;
        RatBlockerFSM.onPlayerDeath -= OnDeadPlayerReceived;

        GameManager.onFirstKeyIllustration -= OnFirstKeyIllustrationReceived;
        GameManager.onSecondKeyIllustration -= OnSecondKeyIllustrationReceived;
        GameManager.onThirdKeyIllustration -= OnThirdKeyIllustrationReceived;

        OpeningAirVentEvent.onVentIllustration -= OnVentIllustrationReceived;
    }

    void Awake()
    {
        if (mainmenuPanel != null)
        {
            mainmenuPanel.SetActive(true);
        }
        else
            Debug.LogWarning("Main Menu UI");

        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        cheatPanel = GameObject.FindGameObjectWithTag("CheatPanel");
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");

        if (pausePanel != null && cheatPanel != null && deathScreen != null)
        {
            pausePanel.SetActive(false);
            cheatPanel.SetActive(false);
            deathScreen.SetActive(false);
        }
        else
            Debug.LogWarning("Add Cheat Panel References");

        if (levelTitleText != null)
            levelTitleText.SetActive(true);
        else
            Debug.LogWarning("Add Level Text UI");

        if (changeLevelPanel != null)
            changeLevelPanel.SetActive(false);
        else
            Debug.LogWarning("Add Change Level Panel");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !cheatPanel.activeSelf)
            PauseorUnpause();

        if (Input.GetKeyDown(KeyCode.Tab) && !pausePanel.activeSelf)
            CheatPanelToggle();
    }

    public void PauseorUnpause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);

        if (pausePanel.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1f;
    }

    public void CheatPanelToggle()
    {
        cheatPanel.SetActive(!cheatPanel.activeSelf);
    }

    void OnChangeLevelTextReceived()
    {
        changeLevelPanel.SetActive(true);
    }

    void OnDeadPlayerReceived()
    {
        deathScreen.SetActive(true);
        PlayerControls.onDeadPlayer -= OnDeadPlayerReceived;
    }

    void OnVentIllustrationReceived()
    {
        ventKeyIllustration.SetActive(true);
        OpeningAirVentEvent.onVentIllustration -= OnVentIllustrationReceived;
    }

    void OnFirstKeyIllustrationReceived()
    {
        firstKeyIllustration.SetActive(true);
        GameManager.onFirstKeyIllustration -= OnFirstKeyIllustrationReceived;
    }

    void OnSecondKeyIllustrationReceived()
    {
        secondKeyIllustration.SetActive(true);
        GameManager.onSecondKeyIllustration -= OnSecondKeyIllustrationReceived;
    }

    void OnThirdKeyIllustrationReceived()
    {
        thirdKeyIllustration.SetActive(true);
        GameManager.onThirdKeyIllustration -= OnThirdKeyIllustrationReceived;
    }
}