using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool onLadder;
    [Header("Player Movement Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float climbSpeed;
    public float sprintClimbSpeed;
    public float defaultGravity;
    public float pushForce;
    [Header("Virtual Camera Reference")]
    public GameObject mainVirutalCam;
    public GameObject firstPuzzleCamPan;
    public GameObject secondPuzzleVirtualCam;
    public GameObject thirdPuzzleVirtualCam;
    [Header("References Obejcts")]
    public GameObject levelTitleText;
    public GameObject pausePanel;
    private float gravity;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private Animator anim;

    void Start()
    {
        if (levelTitleText != null && pausePanel != null && mainVirutalCam != null && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null && thirdPuzzleVirtualCam != null)
        {
            levelTitleText.SetActive(true);
            pausePanel.SetActive(false);
            mainVirutalCam.SetActive(true);
            firstPuzzleCamPan.SetActive(false);
            secondPuzzleVirtualCam.SetActive(false);
            thirdPuzzleVirtualCam.SetActive(false);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Checks if the player is on the Ground
        if (!onLadder)
        {
            if (charController.isGrounded)
            {
                //Gets Player Inputs
                moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
                if (Input.GetAxis("Vertical") > 0)
                    anim.SetBool("isWalking", true);
                else
                    anim.SetBool("isWalking", false);

                if (Input.GetAxis("Vertical") < 0)
                    anim.SetBool("isWalkingBack", true);
                else
                    anim.SetBool("isWalkingBack", false);

                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

                //Applies Movement
                moveDirection = transform.TransformDirection(moveDirection);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection = moveDirection * runningSpeed;
                    // Debug.LogWarning("Running");
                }

                else
                {
                    moveDirection = moveDirection * walkingSpeed;
                    // Debug.LogWarning("Walking");
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpPower;
                    // Debug.LogWarning("Jump");
                }
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = new Vector3(0.0f, Input.GetAxis("Vertical") * sprintClimbSpeed, 0.0f);
                Debug.LogWarning("Sprint Climb?");
            }
            else
            {
                moveDirection = new Vector3(0.0f, Input.GetAxis("Vertical") * climbSpeed, 0.0f);
                Debug.LogWarning("Climbing");
            }

        }
        charController.Move(moveDirection * Time.deltaTime);

        if (onLadder && Input.GetKeyDown(KeyCode.E) && !charController.isGrounded)
            onLadder = false;
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

    //Function which checks what hit the Character Controller's Collider
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Ropes" && Input.GetKey(KeyCode.E))
            onLadder = true;
        else
            onLadder = false;
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
    }
}
