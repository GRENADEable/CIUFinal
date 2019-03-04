using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamManager : MonoBehaviour
{
    [Header("Virtual Camera Reference")]
    public GameObject mainVirutalCam;
    public GameObject firstPuzzleCamPan;
    public GameObject secondPuzzleVirtualCam;
    public GameObject thirdPuzzleVirtualCam;
    public GameObject thirdPuzzleCrateCam;
    public GameObject thidpuzzleRopeCam;
    [Header("Virtual Camera Variables")]
    public float positiveScreenYCamOffset;
    public float negativeScreenYCamOffset;
    public float positiveScreenXCamOffset;
    public float negativeScreenXCamOffset;
    private float defaultXCamOffset = 0.5f;
    private float defaultYCamOffset = 0.5f;

    void Start()
    {
        if (mainVirutalCam != null && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null
         && thirdPuzzleVirtualCam != null && thirdPuzzleCrateCam != null && thidpuzzleRopeCam != null)
        {
            mainVirutalCam.SetActive(true);
            firstPuzzleCamPan.SetActive(false);
            secondPuzzleVirtualCam.SetActive(false);
            thirdPuzzleVirtualCam.SetActive(false);
            thirdPuzzleCrateCam.SetActive(false);
            thidpuzzleRopeCam.SetActive(false);
        }
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
            firstPuzzleCamPan.SetActive(true);
        }

        if (other.gameObject.tag == "SecondPuzzleCamPan")
        {
            secondPuzzleVirtualCam.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdPuzzleCamPan")
        {
            thirdPuzzleVirtualCam.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdPuzzleCratesCamPan")
        {
            thirdPuzzleCrateCam.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdPuzzleRopeCamPan")
        {
            thidpuzzleRopeCam.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FirstPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            firstPuzzleCamPan.SetActive(false);
        }

        if (other.gameObject.tag == "SecondPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            secondPuzzleVirtualCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleCamPan")
        {
            mainVirutalCam.SetActive(true);
            thirdPuzzleVirtualCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleCratesCamPan")
        {
            mainVirutalCam.SetActive(true);
            thirdPuzzleCrateCam.SetActive(false);
        }

        if (other.gameObject.tag == "ThirdPuzzleRopeCamPan")
        {
            thirdPuzzleVirtualCam.SetActive(true);
            thidpuzzleRopeCam.SetActive(false);
        }
    }
}
