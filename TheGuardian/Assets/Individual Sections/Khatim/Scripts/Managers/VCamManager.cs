using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamManager : MonoBehaviour
{
    [Header("Main Camera Reference")]
    public GameObject mainVirutalCam;
    [Header("Attic Camera References")]
    public GameObject atticFirstPuzzleCamPan;
    public GameObject atticSecondPuzzleVirtualCam;
    public GameObject atticThirdPuzzleVirtualCam;
    public GameObject atticThirdPuzzleCloseUpCam;
    public GameObject atticThirdPuzzleCrateCam;
    public GameObject atticThirdpuzzleRopeCam;
    [Header("Hallway Camera Referemces")]
    public GameObject hallwayUnderFloorWalkwayCam;
    [Header("Virtual Camera Variables")]
    public float positiveScreenYCamOffset;
    public float negativeScreenYCamOffset;
    public float positiveScreenXCamOffset;
    public float negativeScreenXCamOffset;
    private float defaultXCamOffset = 0.5f;
    private float defaultYCamOffset = 0.5f;

    void Awake()
    {
        // if (mainVirutalCam != null && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null
        //  && thirdPuzzleVirtualCam != null && thirdPuzzleCrateCam != null && thirdpuzzleRopeCam != null)
        // {
        //     mainVirutalCam.SetActive(true);
        //     firstPuzzleCamPan.SetActive(false);
        //     secondPuzzleVirtualCam.SetActive(false);
        //     thirdPuzzleVirtualCam.SetActive(false);
        //     thirdPuzzleCrateCam.SetActive(false);
        //     thirdpuzzleRopeCam.SetActive(false);
        // }

        mainVirutalCam = GameObject.FindGameObjectWithTag("MainVCam");
    }

    void Update()
    {
        if (mainVirutalCam != null)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenY = positiveScreenYCamOffset;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenY = negativeScreenYCamOffset;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenX = positiveScreenXCamOffset;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
                camOffset.m_ScreenX = negativeScreenXCamOffset;
            }
            else
            {
                CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                camOffset.m_ScreenY = defaultYCamOffset;
                camOffset.m_ScreenX = defaultXCamOffset;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FirstPuzzleCamPan")
        {
            atticFirstPuzzleCamPan.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.gameObject.tag == "SecondPuzzleCamPan")
        {
            atticSecondPuzzleVirtualCam.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(true);
            mainVirutalCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleCloseUpCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(false);
            atticThirdPuzzleCloseUpCam.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdPuzzleCratesCamPan")
        {
            atticThirdPuzzleCloseUpCam.SetActive(false);
            atticThirdPuzzleCrateCam.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdPuzzleRopeCamPan")
        {
            // atticThirdPuzzleCrateCam.SetActive(false);
            atticThirdpuzzleRopeCam.SetActive(true);
        }

        if (other.gameObject.tag == "UnderFloorWalkwayCamPan")
        {
            hallwayUnderFloorWalkwayCam.SetActive(true);
            mainVirutalCam.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FirstPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            atticFirstPuzzleCamPan.SetActive(false);
        }

        if (other.gameObject.tag == "SecondPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            atticSecondPuzzleVirtualCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            atticThirdPuzzleVirtualCam.SetActive(false);
            atticThirdPuzzleCloseUpCam.SetActive(false);
            atticThirdpuzzleRopeCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleCloseUpCamPan")
        {
            atticThirdPuzzleCloseUpCam.SetActive(false);
            atticThirdPuzzleVirtualCam.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdPuzzleCratesCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(true);
            atticThirdPuzzleCrateCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleRopeCamPan")
        {
            atticThirdPuzzleVirtualCam.SetActive(true);
            atticThirdpuzzleRopeCam.SetActive(false);
        }

        if (other.gameObject.tag == "UnderFloorWalkwayCamPan")
        {
            mainVirutalCam.SetActive(true);
            hallwayUnderFloorWalkwayCam.SetActive(false);
        }
    }
}
