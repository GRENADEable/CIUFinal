using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamManager : MonoBehaviour
{
    [Header("Main Camera Reference")]
    public GameObject mainVirutalCam;
    private CinemachineVirtualCamera vCam;

    [Header("Attic Camera References")]
    public GameObject atticFirstPuzzleCamPan;
    public GameObject atticSecondPuzzleVirtualCam;
    public GameObject atticThirdPuzzleVirtualCam;
    public GameObject atticThirdPuzzleCloseUpCam;
    public GameObject atticThirdPuzzleCrateCam;
    public GameObject atticThirdpuzzleRopeCam;

    [Header("Hallway Camera Referemces")]
    public GameObject hallwayThirdKeyCam;
    public GameObject airventSecondKeyCam;
    public float lensFoV;
    public float lerpTime;

    [Header("Nursery Camera References")]
    public GameObject bossCam;

    [Header("Virtual Camera Variables")]
    public float positiveScreenYCamOffset;
    public float negativeScreenYCamOffset;
    public float positiveScreenXCamOffset;
    public float negativeScreenXCamOffset;
    private float defaultXCamOffset = 0.5f;
    private float defaultYCamOffset = 0.5f;

    public delegate void SendEvents();
    public static event SendEvents onDollAIChange;

    void Awake()
    {
        mainVirutalCam = GameObject.FindGameObjectWithTag("MainVCam");
        vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (mainVirutalCam != null)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenY = positiveScreenYCamOffset;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenY = negativeScreenYCamOffset;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenX = positiveScreenXCamOffset;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenX = negativeScreenXCamOffset;
            }
            else
            {
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstPuzzleCamPan")
        {
            atticFirstPuzzleCamPan.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.tag == "SecondPuzzleCamPan")
        {
            atticSecondPuzzleVirtualCam.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.tag == "ThirdPuzzleCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.tag == "ThirdPuzzleCloseUpCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(false);
            atticThirdPuzzleCloseUpCam.SetActive(true);
        }

        if (other.tag == "ThirdPuzzleCratesCamPan")
        {
            atticThirdPuzzleCloseUpCam.SetActive(false);
            atticThirdPuzzleCrateCam.SetActive(true);
        }

        if (other.tag == "ThirdPuzzleRopeCamPan")
        {
            atticThirdpuzzleRopeCam.SetActive(true);
        }

        if (other.tag == "ThirdKeyCamPan")
        {
            hallwayThirdKeyCam.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.tag == "NurseryBossCamPan")
        {
            bossCam.SetActive(true);
            mainVirutalCam.SetActive(false);

            if (onDollAIChange != null)
                onDollAIChange();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "FirstPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            atticFirstPuzzleCamPan.SetActive(false);
        }

        if (other.tag == "SecondPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            atticSecondPuzzleVirtualCam.SetActive(false);
        }

        if (other.tag == "ThirdPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            atticThirdPuzzleVirtualCam.SetActive(false);
            atticThirdPuzzleCloseUpCam.SetActive(false);
            atticThirdpuzzleRopeCam.SetActive(false);
        }

        if (other.tag == "ThirdPuzzleCloseUpCamPan")
        {
            atticThirdPuzzleCloseUpCam.SetActive(false);
            atticThirdPuzzleVirtualCam.SetActive(true);
        }

        if (other.tag == "ThirdPuzzleCratesCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(true);
            atticThirdPuzzleCrateCam.SetActive(false);
        }

        if (other.tag == "ThirdPuzzleRopeCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(true);
            atticThirdpuzzleRopeCam.SetActive(false);
        }

        if (other.tag == "ThirdKeyCamPan")
        {
            mainVirutalCam.SetActive(true);
            hallwayThirdKeyCam.SetActive(false);
        }

        if (other.tag == "NurseryBossCamPan")
        {
            mainVirutalCam.SetActive(true);
            bossCam.SetActive(false);
        }
    }
}
