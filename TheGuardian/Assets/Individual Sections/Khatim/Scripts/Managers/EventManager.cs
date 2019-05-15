using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject brokenBoardSection;
    public GameObject woodenPlank;
    public GameObject fadeOutObj;

    public GameObject[] paintingsEyes;

    public Collider ropeBreakCol;
    public float moveForce;
    public GameObject keyReference;
    public GameObject trunk;
    // public GameObject areanaLockObj;
    public float trunkMoveForce;
    public GameObject finalBoss;

    void OnEnable()
    {
        PlayerControls.onRopeBreak += OnRopeBreakEventReceived;

        GameManager.onPaintingsAwakeMessage += OnPaintingsAwakeMessageEventReceived;
        ParentsAruguingEvent.onKeyMove += OnKeyMoveReceived;

        VCamManager.onFinalBossAppear += FinalBossAppearReceived;

        if (keyReference == null)
            Debug.LogWarning("Add Key Reference");

        if (brokenBoardSection == null)
            Debug.LogWarning("Add Borken Board Reference");

        if (fadeOutObj == null)
            Debug.LogWarning("Add Fade To Black Obj Reference");

        if (woodenPlank == null)
            Debug.LogWarning("Add Wooden Plank Reference");

        if (ropeBreakCol == null)
            Debug.LogWarning("Add Rope Break Collider Reference");

        if (paintingsEyes == null)
            Debug.LogWarning("Add Paintings Reference");
    }

    void OnDisable()
    {
        PlayerControls.onRopeBreak -= OnRopeBreakEventReceived;
        GameManager.onPaintingsAwakeMessage -= OnPaintingsAwakeMessageEventReceived;
        ParentsAruguingEvent.onKeyMove -= OnKeyMoveReceived;
        VCamManager.onFinalBossAppear -= FinalBossAppearReceived;
    }

    void OnDestroy()
    {
        ParentsAruguingEvent.onKeyMove -= OnKeyMoveReceived;
        PlayerControls.onRopeBreak -= OnRopeBreakEventReceived;
        GameManager.onPaintingsAwakeMessage -= OnPaintingsAwakeMessageEventReceived;
        VCamManager.onFinalBossAppear -= FinalBossAppearReceived;
    }


    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.G))
    //         FinalBossAppearReceived();
    // }

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
        keyReference.GetComponent<Rigidbody>().AddForce(Vector3.up * moveForce + Vector3.forward * moveForce);
        Debug.Log("Key Moved");
    }

    void OnEnableTranistionReceived()
    {
        fadeOutObj.SetActive(true);
        Debug.Log("Fade To Black Obj Active");
    }

    void FinalBossAppearReceived()
    {
        finalBoss.SetActive(true);
        // areanaLockObj.SetActive(true);
        trunk.GetComponent<Rigidbody>().AddForce(Vector3.right * trunkMoveForce);
    }
}
