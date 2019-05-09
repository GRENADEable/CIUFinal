using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPlayerDead;
    public int keyCounter;

    public delegate void SendMessageToManagers();
    public static event SendMessageToManagers onPaintingsAwakeMessage;
    public static event SendMessageToManagers onFirstKeyIllustration;
    public static event SendMessageToManagers onSecondKeyIllustration;
    public static event SendMessageToManagers onThirdKeyIllustration;
    // public static event SendMessageToManagers onKeyMove;
    public static event SendMessageToManagers onIncreaseEyeSpeed;

    void OnEnable()
    {
        KeyCollector.onKeyCounterUpdate += OnKeyDropEventReceived;
    }

    void OnDisable()
    {
        KeyCollector.onKeyCounterUpdate -= OnKeyDropEventReceived;
    }

    void OnDestroy()
    {
        KeyCollector.onKeyCounterUpdate -= OnKeyDropEventReceived;
    }

    void Update()
    {
        if (keyCounter >= 1 && onPaintingsAwakeMessage != null && onFirstKeyIllustration != null)
        {
            onPaintingsAwakeMessage();
            onFirstKeyIllustration();
        }

        if (keyCounter >= 2 && onIncreaseEyeSpeed != null && onSecondKeyIllustration != null)
        {
            onIncreaseEyeSpeed();
            onSecondKeyIllustration();
        }

        if (keyCounter >= 3 && onThirdKeyIllustration != null)
            onThirdKeyIllustration();
    }

    void OnKeyDropEventReceived()
    {
        keyCounter++;
        Debug.Log("Key Received");

        // if (onKeyMove != null)
        //     onKeyMove();
    }
}
