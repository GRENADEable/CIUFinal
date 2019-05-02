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
    [Range(0f, 1.0f)]
    public float rotationSpeed;
    public float crouchColShrinkValue; //Initial Value is 0.5f
    public float crouchColCenterValue; //Initial Value is 2

    [Header("Player Jump Variables")]
    public float jumpPower;
    private float jumpTime;
    public float jumpDelay;
    private LightMechanic lightMechanic;

    [Header("Player Gravity Variables")]
    public float defaultGravity;
    public float gravityAfterRopeBreak;

    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    public float sprintClimbSpeed;
    #endregion

    #region Events
    public delegate void SendEvents();
    public static event SendEvents onChangeLevelToHallway;
    public static event SendEvents onChangeLevelText;
    public static event SendEvents onPlayHallwayOST;
    public static event SendEvents onRopeBreakMessage;
    // public static event SendEvents onObjectDetatchEvent;
    public static event SendEvents onKeyMove;
    #endregion

    #region  Object Interaction
    [Header("Object Interaction Variables")]
    // public bool isPickingObject;
    public bool isPushingOrPulling;
    public float pushPower;
    #endregion

    #region Trigger Colliders
    [SerializeField]
    private Collider interactCol;
    #endregion

    private float gravity;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private PlayerInteraction plyInteract;
    private Animator courageAnim;

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
    public bool crouching;

    void OnEnable()
    {
        charController = GetComponent<CharacterController>();
        lightMechanic = GetComponent<LightMechanic>();

        ObjectPushAndPull.constraints += PushAndPullConstraintsEventReceived;
        ObjectPushAndPull.noConstraints += PushAndPullNoConstraintsEventReceived;

        gravity = defaultGravity;
        courageAnim = GetComponent<Animator>();
        playerHeight = charController.height;
        playerCenter = charController.center.y;
        jumpTime = jumpDelay;
    }

    void OnDisable()
    {
        ObjectPushAndPull.constraints -= PushAndPullConstraintsEventReceived;
        ObjectPushAndPull.noConstraints -= PushAndPullNoConstraintsEventReceived;
    }

    void OnDestroy()
    {
        ObjectPushAndPull.constraints -= PushAndPullConstraintsEventReceived;
        ObjectPushAndPull.noConstraints -= PushAndPullNoConstraintsEventReceived;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && plyInteract != null)
        {
            plyInteract.StartInteraction();
        }
        else if (Input.GetKey(KeyCode.E) && plyInteract != null)
        {
            plyInteract.UpdateInteraction();
        }
        if (Input.GetKeyUp(KeyCode.E) && plyInteract != null)
        {
            plyInteract.EndInteraction();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (onKeyMove != null)
                onKeyMove();
        }

        if (!onRope)
        {
            //Storing Player's Character Controllers Height and Center
            float localHeight = playerHeight;
            float localCenter = playerCenter;

            //Gets Player Inputs
            moveVertical = Input.GetAxisRaw("Vertical");
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            jumpTime += Time.deltaTime;

            //Checks if the player is on the Ground
            if (charController.isGrounded)
            {
                //Player Light Idle Animation
                if ((moveVertical == 0 || moveVertical == 0 || moveHorizontal == 0 || moveHorizontal == 0) && !crouching && lightMechanic.lightOn)
                    courageAnim.SetBool("Light", true);
                else
                    courageAnim.SetBool("Light", false);

                //Player Light Crouch Idle Animation
                if ((moveVertical == 0 || moveVertical == 0 || moveHorizontal == 0 || moveHorizontal == 0) && crouching && lightMechanic.lightOn)
                    courageAnim.SetBool("LightCrouch", true);
                else
                    courageAnim.SetBool("LightCrouch", false);

                //Player Walking Animation
                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && !crouching && !lightMechanic.lightOn)
                    courageAnim.SetBool("isWalking", true);
                else
                    courageAnim.SetBool("isWalking", false);

                //Player Crouch Walk Animation
                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && crouching && !lightMechanic.lightOn)
                    courageAnim.SetBool("isCrouchWalking", true);
                else
                    courageAnim.SetBool("isCrouchWalking", false);

                //Player Light Crouch Walk Animation
                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && lightMechanic.lightOn && crouching)
                    courageAnim.SetBool("LightCrouchWalk", true);
                else
                    courageAnim.SetBool("LightCrouchWalk", false);

                //Player Light Run  Animation
                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && !crouching && lightMechanic.lightOn)
                    courageAnim.SetBool("LightWalk", true);
                else
                    courageAnim.SetBool("LightWalk", false);

                //Player Light Run Animation
                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C) && lightMechanic.lightOn)
                    courageAnim.SetBool("LightRun", true);
                else
                    courageAnim.SetBool("LightRun", false);

                //Applies Movement
                moveDirection = new Vector3(-moveVertical, 0.0f, moveHorizontal);

                //Applies Roatation relative to What Key is Pressed
                if (moveDirection != Vector3.zero && !isPushingOrPulling)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);

                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C))
                {
                    moveDirection = moveDirection * runningSpeed;
                    courageAnim.SetBool("isRunning", true);
                    // Debug.Log("Running");
                }
                else if (Input.GetKey(KeyCode.C) && !isPushingOrPulling)
                {
                    //Made Character Controller Collider Shrink Variables Dynamic
                    localHeight = playerHeight * crouchColShrinkValue;
                    localCenter = playerCenter / crouchColCenterValue;
                    courageAnim.SetBool("isCrouching", true);
                    crouching = true;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        moveDirection = moveDirection * crouchRunSpeed;
                        // Debug.Log("Crouch Run");
                    }
                    else
                    {
                        moveDirection = moveDirection * crouchWalkSpeed;
                        // Debug.Log("Crouch Walk");
                    }
                }
                else
                {
                    moveDirection = moveDirection * walkingSpeed;
                    courageAnim.SetBool("isRunning", false);
                    courageAnim.SetBool("isCrouching", false);
                    crouching = false;
                    // Debug.Log("Walking");
                }

                if (Input.GetButton("Jump") && !isPushingOrPulling)
                {
                    Jump();
                    // moveDirection.y = jumpPower;
                    // anim.Play("CourageJump");
                }

                //Player Crouching
                charController.height = Mathf.Lerp(charController.height, localHeight, 5 * Time.deltaTime);
                charController.center = new Vector3(0, Mathf.Lerp(charController.center.y, localCenter, 5 * Time.deltaTime), 0);
                Mathf.Clamp(charController.center.y, 0.05f, 0.1f);
            }
            else
                moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            //Rope Climbing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = new Vector3(0.0f, Input.GetAxis("Vertical") * sprintClimbSpeed, 0.0f);
                // Debug.Log("Sprint Climb?");
            }
            else
            {
                moveDirection = new Vector3(0.0f, Input.GetAxis("Vertical") * climbSpeed, 0.0f);
                // Debug.Log("Climbing");
            }
        }

        if (interactCol != null && Input.GetKey(KeyCode.E))
            onRope = true;

        else
            onRope = false;

        charController.Move(moveDirection * Time.deltaTime);

        if (onRope && Input.GetKeyUp(KeyCode.E) && !charController.isGrounded)
            onRope = false;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp" && plyInteract == null)
        {
            plyInteract = GetComponent<ObjectPickup>();
            plyInteract.interactCol = other;

            if (Input.GetKey(KeyCode.E))
                plyInteract.StartInteraction();

            Debug.Log("Pickup Reference Added");
        }

        if (other.tag == "PushAndPull" && plyInteract == null)
        {
            plyInteract = GetComponent<ObjectPushAndPull>();
            plyInteract.interactCol = other;

            if (Input.GetKey(KeyCode.E))
                plyInteract.StartInteraction();

            Debug.Log("Push and Pull Reference Added");
        }

        if (other.tag == "Rope")
        {
            interactCol = other;
        }

        if (other.gameObject.tag == "Matchstick")
        {
            interactCol = other;
        }

        if (other.tag == "RopeBreak")
        {
            if (onRopeBreakMessage != null)
                onRopeBreakMessage();

            gravity = gravityAfterRopeBreak;
            // Debug.Log("Rope Broken");
        }

        if (other.gameObject.tag == "End")
        {
            if (onChangeLevelToHallway != null && onChangeLevelText != null && onPlayHallwayOST != null)
            {
                onChangeLevelToHallway();
                onChangeLevelText();
                onPlayHallwayOST();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (plyInteract != null)
        {
            if (other.tag == "PickUp" && plyInteract.interactCol == other)
                ResetInteraction();

            if (other.tag == "PushAndPull" && plyInteract.interactCol == other)
                ResetInteraction();

            Debug.Log("Interaction Script Reference Removed");
        }

        if (other.tag == "Rope")
            interactCol = null;

        if (other.gameObject.tag == "Matchstick")
            interactCol = null;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "HallwayEndDoor" && Input.GetKey(KeyCode.E))
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if (body == null || body.isKinematic)
                return;

            if (hit.moveDirection.y < -0.3f)
                return;

            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushDir * pushPower;
            // Debug.Log("Pushing Door");
        }
    }

    void Jump()
    {
        if (jumpTime > jumpDelay && !lightMechanic.lightOn)
        {
            moveDirection.y = jumpPower;
            courageAnim.Play("CourageJump");
            // Debug.Log("Jump");
            jumpTime = 0f;
        }

        if (jumpTime > jumpDelay && lightMechanic.lightOn)
        {
            moveDirection.y = jumpPower;
            courageAnim.Play("CourageJumpLight");
            // Debug.Log("Jump");
            jumpTime = 0f;
        }
    }

    public void ResetInteraction()
    {
        plyInteract.interactCol = null;
        plyInteract = null;
    }

    void PushAndPullConstraintsEventReceived()
    {
        isPushingOrPulling = true;
    }

    void PushAndPullNoConstraintsEventReceived()
    {
        isPushingOrPulling = false;
    }
}