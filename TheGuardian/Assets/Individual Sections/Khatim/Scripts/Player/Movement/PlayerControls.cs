using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerControls : MonoBehaviour
{
    #region  Player Variables
    [Header("Player Movement Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    public float crouchWalkSpeed;
    public float crouchRunSpeed;
    public float crouchColShrinkValue; //Initial Value is 0.5f
    public float crouchColCenterValue; //Initial Value is 2
    [Header("Player Jump Variables")]
    public float jumpPower;
    public float jumpDelay;
    [Header("Player Gravity Variables")]
    public float defaultGravity;
    public float gravityAfterRopeBreak;
    // public float pushPower;
    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    public float sprintClimbSpeed;
    public bool isPickingObject;
    #endregion

    #region Events
    public delegate void SendEvents();
    public static event SendEvents onChangeLevelToHallway;
    public static event SendEvents onRopeBreakMessage;
    public static event SendEvents onObjectDetatchEvent;
    public static event SendEvents onObjectShakePlank;
    public static event SendEvents onObjectStillPlank;
    public static event SendEvents onObjectBendPlank;
    #endregion

    public delegate void Interact();
    Interact interactAction;

    #region Trigger Colliders
    [SerializeField]
    private Collider ropeCol;
    [SerializeField]
    private Collider interactCol;
    [SerializeField]
    private Collider plankCol;
    #endregion

    [SerializeField]
    private bool isPushingOrPulling;
    private float gravity;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private float jumpTime;
    private Animator anim;
    [SerializeField]
    private Rigidbody rg;

    #region Cheats
    [Header("Cheats Section :3")]
    [SerializeField]
    private float flashSpeed;
    [SerializeField]
    private float defaultRunningSpeed;
    [SerializeField]
    private float superJump;
    [SerializeField]
    private float defaultJump;
    #endregion

    private float playerHeight;
    private float moveHorizontal;
    private float moveVertical;
    private float playerCenter;

    void OnEnable()
    {
        charController = GetComponent<CharacterController>();
        rg = GetComponent<Rigidbody>();
        gravity = defaultGravity;
        anim = GetComponent<Animator>();
        playerHeight = charController.height;
        playerCenter = charController.center.y;
        jumpTime = jumpDelay;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && interactAction != null)
        {
            interactAction();
        }

        if (Input.GetKey(KeyCode.E) && interactCol != null && !isPushingOrPulling && charController.isGrounded)
        {
            interactCol.gameObject.AddComponent(typeof(FixedJoint));
            interactCol.gameObject.GetComponent<FixedJoint>().enableCollision = true;
            interactCol.gameObject.GetComponent<FixedJoint>().connectedBody = rg;
            interactCol.GetComponent<Rigidbody>().isKinematic = false;
            interactCol.GetComponent<Rigidbody>().useGravity = false;
            isPushingOrPulling = true;
            Debug.LogWarning("Object Attached");
        }

        if ((Input.GetKeyUp(KeyCode.E) && isPushingOrPulling) || (interactCol == null && isPushingOrPulling) || (!charController.isGrounded && interactCol == null && isPushingOrPulling))
        {
            if (onObjectDetatchEvent != null) //Sends message to Gameobject with Push and Pull Object Script
                onObjectDetatchEvent();

            isPushingOrPulling = false;
        }

        if (Input.GetKey(KeyCode.E) && plankCol != null)
        {
            if (onObjectBendPlank != null)
            {
                onObjectBendPlank();
            }
        }

        if (!onRope)
        {
            float localHeight = playerHeight;
            float localCenter = playerCenter;
            //Gets Player Inputs
            moveVertical = Input.GetAxisRaw("Vertical");
            moveHorizontal = Input.GetAxisRaw("Horizontal");
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
                if (moveDirection != Vector3.zero && !isPushingOrPulling)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15f);

                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C))
                {
                    moveDirection = moveDirection * runningSpeed;
                    anim.SetBool("isRunning", true);
                    // Debug.LogWarning("Running");
                }
                else if (Input.GetKey(KeyCode.C) && !isPushingOrPulling)
                {
                    localHeight = playerHeight * crouchColShrinkValue;
                    localCenter = playerCenter / crouchColCenterValue;
                    anim.SetBool("isCrouching", true);
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
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isCrouching", false);
                    // Debug.LogWarning("Walking");
                }

                if (Input.GetKey(KeyCode.Space) && !isPushingOrPulling && !isPickingObject)
                {
                    Jump();
                }

                //Player Crouching
                charController.height = Mathf.Lerp(charController.height, localHeight, 5 * Time.deltaTime);
                //Vector3 p = Vector3.zero;
                charController.center = new Vector3(0, Mathf.Lerp(charController.center.y, localCenter, 5 * Time.deltaTime), 0);
                Mathf.Clamp(charController.center.y, 0.05f, 0.1f);
                // charController.center = p;
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

    void Pickup()
    {
        Debug.LogWarning("Object Picked Up");
    }

    void Drop()
    {
        Debug.LogWarning("Object Dropped");
    }

    void Jump()
    {
        if (jumpTime > jumpDelay)
        {
            moveDirection.y = jumpPower;
            // anim.Play("CourageJump");
            Debug.LogWarning("Jump");
            jumpTime = 0f;
        }
    }

    // public void CrouchingCheck()
    // {
    //     float localHeight = 0;
    //     float localSpeed = 0;

    //     if (Input.GetKey(KeyCode.C))
    //     {
    //         CrouchingExecution(localSpeed, localHeight);
    //     }
    // }

    // public void CrouchingExecution(float speed, float height)
    // {
    //     height = playerHeight * 0.5f;
    //     speed = crouchWalkSpeed;
    // }

    // public void CharacterControllerBodyModifier()
    // {
    //     float latestRecordedHeight = charController.height;
    // }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            interactAction = GetComponent<ObjectThrowing>().PickUpFunctionality;
            GetComponent<ObjectThrowing>().pickupCol = other;
        }

        if (other.tag == "Rope")
        {
            ropeCol = other;
        }

        if (other.tag == "RopeBreak")
        {
            if (onRopeBreakMessage != null)
                onRopeBreakMessage();

            gravity = gravityAfterRopeBreak;
            // Debug.LogWarning("Rope Broken");
        }

        if (other.gameObject.tag == "End")
        {
            // SceneManage.instance.HallwayLevel();
            if (onChangeLevelToHallway != null)
                onChangeLevelToHallway();
        }

        if (other.tag == "Rope")
        {
            ropeCol = other;
        }

        if (other.tag == "Interact")
        {
            interactCol = other;
        }

        if (other.tag == "BendPlank")
        {
            plankCol = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        {
            interactAction -= GetComponent<ObjectThrowing>().PickUpFunctionality;
            GetComponent<ObjectThrowing>().pickupCol = null;
        }

        if (other.tag == "Rope")
        {
            ropeCol = null;
        }

        if (other.tag == "Interact")
        {
            interactCol = null;
        }

        if (other.tag == "BendPlank")
        {
            plankCol = null;
        }
    }

    //Function which checks what hit the Character Controller's Collider
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if (hit.gameObject.tag == "BendPlank" &&)
        // {
        //     if (onObjectShakePlank != null)
        //     {
        //         onObjectShakePlank();
        //     }
        // }

        // if (hit.gameObject.tag == "BendPlank" && Input.GetKeyDown(KeyCode.E))
        // {
        //     if (onObjectBendPlank != null)
        //     {
        //         onObjectBendPlank();
        //         Destroy(hit)
        //     }
        // }
    }
}
