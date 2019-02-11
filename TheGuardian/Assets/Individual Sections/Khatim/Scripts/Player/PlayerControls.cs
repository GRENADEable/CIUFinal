using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public bool onLadder;
    public float walkingSpeed;
    public float runningSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float climbSpeed;
    public float sprintClimbSpeed;
    public float defaultGravity;
    public float pushPower;
    [Header("Virtual Camera Reference")]
    public GameObject mainVirutalCam;
    public GameObject firstPuzzleCamPan;
    public GameObject secondPuzzleVirtualCam;
    public GameObject thirdPuzzleVirtualCam;
    public GameObject thirdPuzzleCrateCam;
    [Header("References Obejcts")]
    public GameObject levelTitleText;
    public GameObject pausePanel;
    private float gravity;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private Animator anim;
    [Header("Cheats Section :3")]
    [SerializeField]
    private float runningCheat;
    [SerializeField]
    private float defaultRunningSpeed;

    void Start()
    {
        if (levelTitleText != null && pausePanel != null && mainVirutalCam != null
         && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null
         && thirdPuzzleVirtualCam != null && thirdPuzzleCrateCam != null)
        {
            levelTitleText.SetActive(true);
            pausePanel.SetActive(false);
            mainVirutalCam.SetActive(true);
            firstPuzzleCamPan.SetActive(false);
            secondPuzzleVirtualCam.SetActive(false);
            thirdPuzzleVirtualCam.SetActive(false);
            thirdPuzzleCrateCam.SetActive(false);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        #region Cheats
        if (Input.GetKey(KeyCode.G))
            runningSpeed = runningCheat;

        if (Input.GetKey(KeyCode.H))
            runningSpeed = defaultRunningSpeed;
        #endregion

        //Checks if the player is on the Ground
        if (!onLadder)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

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
        Rigidbody body = hit.collider.attachedRigidbody;

        // Return null if no Rigidbody
        if (body == null || body.isKinematic)
            return;

        // Return null as we don't want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction, we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the push on the object
        body.velocity = pushDir * pushPower;

        if (hit.collider.tag == "Rope" && Input.GetKey(KeyCode.E))
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

        if (other.gameObject.tag == "ThirdPuzzleCratesCamPan")
        {
            thirdPuzzleCrateCam.SetActive(true);
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
    }
}
