﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject brokenBoardSection;
    public GameObject woodenPlank;
    public GameObject fadeOutObj;
    public Collider bossCamCol;

    public GameObject[] paintingsEyes;

    public Collider ropeBreakCol;
    public AudioSource ropeBreakAud;
    public float moveForce;
    public GameObject keyReference;
    public GameObject trunk;
    // public GameObject areanaLockObj;
    public float trunkMoveForce;
    public GameObject finalBoss;

    public delegate void SendEvents();
    public static event SendEvents onAtticFadeOut;

    void OnEnable()
    {
        PlayerControls.onRopeBreak += OnRopeBreakEventReceived;

        GameManager.onPaintingsAwakeMessage += OnPaintingsAwakeMessageEventReceived;
        ParentsAruguingEvent.onKeyMove += OnKeyMoveReceived;

        VCamManager.onFinalBossAppear += FinalBossAppearReceived;
        BehaviourTree.onEnableFinalCam += OnFinalCamReceived;

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
        BehaviourTree.onEnableFinalCam -= OnFinalCamReceived;
    }

    void OnDestroy()
    {
        ParentsAruguingEvent.onKeyMove -= OnKeyMoveReceived;
        PlayerControls.onRopeBreak -= OnRopeBreakEventReceived;

        GameManager.onPaintingsAwakeMessage -= OnPaintingsAwakeMessageEventReceived;

        VCamManager.onFinalBossAppear -= FinalBossAppearReceived;
        BehaviourTree.onEnableFinalCam -= OnFinalCamReceived;
    }

    void OnRopeBreakEventReceived()
    {
        if (ropeBreakCol != null)
        {
            Destroy(ropeBreakCol.GetComponent<HingeJoint>());
            Destroy(ropeBreakCol.GetComponent<Collider>());
            brokenBoardSection.SetActive(false);
            woodenPlank.SetActive(false);
            ropeBreakAud.Play();
            if (onAtticFadeOut != null)
                onAtticFadeOut();

            // Debug.Log("Rope Broken");
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

    void FinalBossAppearReceived()
    {
        finalBoss.SetActive(true);
        // areanaLockObj.SetActive(true);
        trunk.GetComponent<Rigidbody>().AddForce(Vector3.right * trunkMoveForce);
    }

    void OnFinalCamReceived()
    {
        bossCamCol.enabled = false;
        BehaviourTree.onEnableFinalCam -= OnFinalCamReceived;
    }
}