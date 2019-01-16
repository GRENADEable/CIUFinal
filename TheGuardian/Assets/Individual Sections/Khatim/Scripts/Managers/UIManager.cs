using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainmenuPanel;
    public GameObject settingsPanel;
    void Awake()
    {
        mainmenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
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
}
