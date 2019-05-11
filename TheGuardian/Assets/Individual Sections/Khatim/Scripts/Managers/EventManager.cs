using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject brokenBoardSection;
    public GameObject woodenPlank;
    public GameObject fadeToBlackObj;

    public GameObject[] paintingsEyes;

    public Collider ropeBreakCol;
    public float moveForce;
    public GameObject keyReference;

    void OnEnable()
    {
        PlayerControls.onRopeBreakMessage += OnRopeBreakEventReceived;
        GameManager.onPaintingsAwakeMessage += OnPaintingsAwakeMessageEventReceived;
        ParentsAruguingEvent.onKeyMove += OnKeyMoveReceived;
        PlayerControls.onKeyMove += OnKeyMoveReceived;

        if (keyReference == null)
            Debug.LogWarning("Add Key Reference");

        if (brokenBoardSection == null)
            Debug.LogWarning("Add Borken Board Reference");

        if (fadeToBlackObj == null)
            Debug.LogWarning("Add Fade To Black Obj Reference");
        else
            fadeToBlackObj.SetActive(false);

        if (woodenPlank == null)
            Debug.LogWarning("Add Wooden Plank Reference");

        if (ropeBreakCol == null)
            Debug.LogWarning("Add Rope Break Collider Reference");

        if (paintingsEyes == null)
            Debug.LogWarning("Add Paintings Reference");
    }

    void OnDisable()
    {
        PlayerControls.onRopeBreakMessage -= OnRopeBreakEventReceived;
        GameManager.onPaintingsAwakeMessage -= OnPaintingsAwakeMessageEventReceived;
        ParentsAruguingEvent.onKeyMove -= OnKeyMoveReceived;
        PlayerControls.onKeyMove -= OnKeyMoveReceived;
    }

    void OnDestroy()
    {
        ParentsAruguingEvent.onKeyMove -= OnKeyMoveReceived;
        PlayerControls.onRopeBreakMessage -= OnRopeBreakEventReceived;
        GameManager.onPaintingsAwakeMessage -= OnPaintingsAwakeMessageEventReceived;
        PlayerControls.onKeyMove -= OnKeyMoveReceived;
    }
    void OnRopeBreakEventReceived()
    {
        if (ropeBreakCol != null)
        {
            Destroy(ropeBreakCol.GetComponent<HingeJoint>());
            Destroy(ropeBreakCol.GetComponent<Collider>());
            brokenBoardSection.SetActive(false);
            woodenPlank.SetActive(false);
            // fadeToBlackObj.SetActive(true);
            OnEnableTranistionReceived();
            Debug.Log("Rope Broken");
        }
    }

    void OnPaintingsAwakeMessageEventReceived()
    {
        for (int i = 0; i < paintingsEyes.Length; i++)
        {
            paintingsEyes[i].SetActive(true);
        }
        GameManager.onPaintingsAwakeMessage -= OnPaintingsAwakeMessageEventReceived;
        Debug.Log("Paintings Awake");
    }

    void OnKeyMoveReceived()
    {
        keyReference.GetComponent<Rigidbody>().AddForce(keyReference.transform.up * moveForce + keyReference.transform.forward * moveForce);
        Debug.Log("Key Moved");
    }

    void OnEnableTranistionReceived()
    {
        fadeToBlackObj.SetActive(true);
        Debug.Log("Fade To Black Obj Active");
    }
}
