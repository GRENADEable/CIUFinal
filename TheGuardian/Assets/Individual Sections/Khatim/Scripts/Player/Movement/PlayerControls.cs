using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerControls : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    public float crouchWalkSpeed;
    public float crouchRunSpeed;
    public float rotateSpeed;
    [Header("Player Jump Variables")]
    public float jumpPower;
    public float jumpDelay;
    [Header("Player Gravity Variables")]
    public float defaultGravity;
    public float gravityAfterRopeBreak;
    [Header("Player Push and Pull Variables")]
    public float interactionDistance;
    public float interactionDistanceHeight;
    public float pushPower;
    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    // public float distanceFromRope;
    public float raycastHeight;
    public float sprintClimbSpeed;
    [Header("References Obejcts")]
    public GameObject levelTitleText;
    public GameObject pausePanel;
    public GameObject cheatPanel;
    public GameObject brokenBoardSection;
    public GameObject woodenPlank;

    public delegate void Grab();
    public static event Grab onObjectDetatchEvent;

    [SerializeField]
    private Collider ropeCol;
    [SerializeField]
    private Collider interactCol;
    private bool isInteracting;
    private float gravity;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private float jumpTime;
    private Animator anim;
    [Header("Cheats Section :3")]
    [SerializeField]
    private float flashSpeed;
    [SerializeField]
    private float defaultRunningSpeed;
    [SerializeField]
    private float superJump;
    [SerializeField]
    private float defaultJump;
    private Vector3 playerVector;
    private float playerHeight;
    private float moveHorizontal;
    private float moveVertical;

    void Awake()
    {
        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        cheatPanel = GameObject.FindGameObjectWithTag("CheatPanel");

        if (pausePanel != null && pausePanel != null)
        {
            pausePanel.SetActive(false);
            cheatPanel.SetActive(false);
        }
    }
    void Start()
    {
        // if (levelTitleText != null && pausePanel != null && trapDoor != null
        //  && mainVirutalCam != null && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null
        //  && thirdPuzzleVirtualCam != null && thirdPuzzleCrateCam != null && thidpuzzleRopeCam != null
        //  cheatPanel != null)
        // {
        //     levelTitleText.SetActive(true);
        //     trapDoor.SetActive(true);
        //     pausePanel.SetActive(false);
        //     mainVirutalCam.SetActive(true);
        //     cheatPanel.SetActive(false);
        //     firstPuzzleCamPan.SetActive(false);
        //     secondPuzzleVirtualCam.SetActive(false);
        //     thirdPuzzleVirtualCam.SetActive(false);
        //     thirdPuzzleCrateCam.SetActive(false);
        //     thidpuzzleRopeCam.SetActive(false);
        // }
        if (levelTitleText != null && woodenPlank != null)
        {
            levelTitleText.SetActive(true);
            woodenPlank.SetActive(true);
        }

        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
        anim = GetComponent<Animator>();
        playerHeight = charController.height;
        playerVector = transform.position;
        jumpTime = jumpDelay;
    }

    void Update()
    {
        if (cheatPanel != null && pausePanel != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseorUnpause();

            if (Input.GetKeyDown(KeyCode.Tab))
                CheatPanelToggle();
        }

        if (Input.GetKey(KeyCode.E) && interactCol != null && !isInteracting)
        {
            interactCol.gameObject.AddComponent(typeof(FixedJoint));
            interactCol.gameObject.GetComponent<FixedJoint>().enableCollision = true;
            interactCol.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            interactCol.GetComponent<Rigidbody>().isKinematic = false;
            interactCol.GetComponent<Rigidbody>().useGravity = false;
            isInteracting = true;
            Debug.LogWarning("Object Attached");
        }

        if ((Input.GetKeyUp(KeyCode.E) && isInteracting) || (interactCol == null && isInteracting))
        {
            if (onObjectDetatchEvent != null)
                onObjectDetatchEvent();

            isInteracting = false;
        }

        if (!onRope)
        {
            float localHeight = playerHeight;
            //Gets Player Inputs
            moveVertical = Input.GetAxis("Vertical");
            moveHorizontal = Input.GetAxis("Horizontal");
            jumpTime += Time.deltaTime;

            //Checks if the player is on the Ground
            if (charController.isGrounded)
            {
                //Animation Controls
                if (moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0)
                    anim.SetBool("isWalking", true);
                else
                    anim.SetBool("isWalking", false);

                //Applies Movement
                moveDirection = new Vector3(-moveVertical, 0.0f, moveHorizontal);

                //Applies Roatation relative to What Key is Pressed
                if (moveDirection != Vector3.zero && !isInteracting)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15f);

                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C))
                {
                    moveDirection = moveDirection * runningSpeed;
                    // Debug.LogWarning("Running");
                }
                else if (Input.GetKey(KeyCode.C) && !isInteracting)
                {
                    localHeight = playerHeight * 0.5f;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        moveDirection = moveDirection * crouchRunSpeed;
                        // Debug.LogWarning("Crouch Run");
                    }
                    else
                    {
                        moveDirection = moveDirection * crouchWalkSpeed;
                        // Debug.LogWarning("Crouch Walk");
                    }
                }
                else
                {
                    moveDirection = moveDirection * walkingSpeed;
                    // Debug.LogWarning("Walking");
                }

                if (Input.GetKey(KeyCode.Space) && !isInteracting)
                {
                    Jump();
                }

                //Player Crouching
                float latestRecordedHeight = charController.height;
                charController.height = Mathf.Lerp(charController.height, localHeight, 5 * Time.deltaTime);
                playerVector.y += (charController.height - latestRecordedHeight) / 1.5f;
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }
        else
        {
            //Rope Climbing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = new Vector3(0.0f, Input.GetAxis("Vertical") * sprintClimbSpeed, 0.0f);
                // Debug.LogWarning("Sprint Climb?");
            }
            else
            {
                moveDirection = new Vector3(0.0f, Input.GetAxis("Vertical") * climbSpeed, 0.0f);
                // Debug.LogWarning("Climbing");
            }
        }

        if (ropeCol != null && Input.GetKey(KeyCode.E))
            onRope = true;

        else
            onRope = false;

        charController.Move(moveDirection * Time.deltaTime);

        if (onRope && Input.GetKeyUp(KeyCode.E) && !charController.isGrounded)
            onRope = false;
    }

    void Jump()
    {
        if (jumpTime > jumpDelay)
        {
            moveDirection.y = jumpPower;
            // Debug.LogWarning("Jump");
            jumpTime = 0f;
        }
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

    public void CheatPanelToggle()
    {
        cheatPanel.SetActive(!cheatPanel.activeSelf);
    }

    public void CrouchingCheck()
    {
        float localHeight = 0;
        float localSpeed = 0;

        if (Input.GetKey(KeyCode.C))
        {
            CrouchingExecution(localSpeed, localHeight);
        }
    }

    public void CrouchingExecution(float speed, float height)
    {
        height = playerHeight * 0.5f;
        speed = crouchWalkSpeed;
    }

    public void CharacterControllerBodyModifier()
    {
        float latestRecordedHeight = charController.height;
    }

    #region Cheats :P
    public void SuperJumpToggle(bool isSuperJump)
    {
        if (isSuperJump)
        {
            jumpPower = superJump;
        }
        else if (!isSuperJump)
        {
            jumpPower = defaultJump;
        }
    }

    public void FlashSpeedToggle(bool isFlash)
    {
        if (isFlash)
        {
            runningSpeed = flashSpeed;
        }
        else if (!isFlash)
        {
            runningSpeed = defaultRunningSpeed;
        }
    }
    #endregion

    //Function which checks what hit the Character Controller's Collider
    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     Rigidbody body = hit.collider.attachedRigidbody;

    //     // Return null if no Rigidbody
    //     if (body == null || body.isKinematic)
    //         return;

    //     // Return null as we don't want to push objects below us
    //     if (hit.moveDirection.y < -0.3f)
    //         return;

    //     // Calculate push direction from move direction, we only push objects to the sides never up and down
    //     Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

    //     // Apply the push on the object
    //     body.velocity = pushDir * pushPower;
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rope")
        {
            ropeCol = other;
        }

        if (other.tag == "RopeBreak" && brokenBoardSection != null)
        {
            Destroy(other.GetComponent<HingeJoint>());
            Destroy(other.GetComponent<Collider>());
            brokenBoardSection.SetActive(false);
            woodenPlank.SetActive(false);
            // other.gameObject.SetActive(false);
            gravity = gravityAfterRopeBreak;
            Debug.LogWarning("Rope Broken");
        }

        if (other.gameObject.tag == "End")
        {
            SceneManage.instance.HallwayLevel();
        }

        if (other.tag == "Rope")
        {
            ropeCol = other;
        }

        if (other.tag == "Interact")
        {
            interactCol = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rope")
        {
            ropeCol = null;
        }

        if (other.tag == "Interact")
        {
            interactCol = null;
        }
    }
}
