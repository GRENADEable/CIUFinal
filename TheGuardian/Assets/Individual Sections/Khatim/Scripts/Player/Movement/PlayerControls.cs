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
    public float jumpDelay;

    [Header("Player Jump Variables")]
    public float jumpPower;

    [Header("Player Gravity Variables")]
    public float defaultGravity;
    public float gravityAfterRopeBreak;
    // public float pushPower;

    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    public float sprintClimbSpeed;
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

    #region  Object Interaction
    [Header("Object Interaction Variables")]
    public bool isPickingObject;
    public bool isPushingOrPulling;
    public float throwingForce;
    public Transform pivotDummy;
    [SerializeField]
    private Collider objectInteractReference;
    // public static event SendEvents onObjectDropEvent;
    #endregion

    #region Trigger Colliders
    [SerializeField]
    private Collider interactCol;
    [SerializeField]
    private Collider plankCol;
    #endregion

    private float gravity;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private PlayerInteraction plyInteract;
    private float jumpTime;
    private Animator anim;
    [SerializeField]
    private Rigidbody rg;
    [SerializeField]
    private Rigidbody rgCourageRightHand;

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

        // if (Input.GetKeyDown(KeyCode.E) && interactAction != null)// && !isPushingOrPulling && !isPickingObject && interactCol.tag == "PickUp" && charController.isGrounded )
        // {
        //     //isPickingObject = true;
        // }

        /*if (Input.GetKey(KeyCode.E) && charController.isGrounded && interactAction != null && !isPushingOrPulling && !isPickingObject && interactCol.tag == "PushAndPull")
        {
            isPushingOrPulling = true;
            ObjectPushandPull();
        }

        if (Input.GetKeyUp(KeyCode.E) && interactAction != null && (isPushingOrPulling || isPickingObject) && (interactCol.tag == "PushAndPull" || interactCol.tag == "PickUp"))
        {
            isPushingOrPulling = false;
            isPickingObject = false;
            ObjectDrop();
        }*/

        // if (Input.GetKey(KeyCode.Space) && interactAction != null && isPickingObject && !isPushingOrPulling)
        // {
        //     isPickingObject = false;
        //     ObjectThrow();
        // }

        if (Input.GetKey(KeyCode.E) && plankCol != null)
        {
            if (onObjectBendPlank != null)
            {
                onObjectBendPlank();
            }
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
                    //Made Character Controller Collider Shrink Variables Dynamic
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

                if (Input.GetKeyDown(KeyCode.Space) && !isPushingOrPulling && !isPickingObject)
                {
                    Jump();
                    // moveDirection.y = jumpPower;
                }

                //Player Crouching
                charController.height = Mathf.Lerp(charController.height, localHeight, 5 * Time.deltaTime);
                charController.center = new Vector3(0, Mathf.Lerp(charController.center.y, localCenter, 5 * Time.deltaTime), 0);
                Mathf.Clamp(charController.center.y, 0.05f, 0.1f);
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

        if (interactCol != null && Input.GetKey(KeyCode.E) && interactCol.tag == "Rope")
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
            //interactCol = other;
            objectInteractReference = other;
            plyInteract = GetComponent<ObjectPickup>();
        }

        if (other.tag == "Rope" && plyInteract == null)
        {
            plyInteract = GetComponent<RopeClimbing>();
            //interactCol = other;
        }

        if (other.tag == "PushAndPull")
        {
            // interactCol = other;
            plyInteract = GetComponent<ObjectPushAndPull>();
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
            // Debug.LogWarning("Rope Broken");
        }

        if (other.gameObject.tag == "End")
        {
            if (onChangeLevelToHallway != null)
                onChangeLevelToHallway();
        }

        if (other.tag == "BendPlank")
        {
            plankCol = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp" && objectInteractReference == other)
        {
            plyInteract = null;
            // interactCol = null;
        }

        if (other.tag == "Rope")
        {
            plyInteract = null;
            // interactCol = null;
        }

        if (other.tag == "PushAndPull")
        {
            plyInteract = null;
            // interactCol = null;
        }

        if (other.gameObject.tag == "Matchstick")
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

    }

    #region Object Interaction
    // void ObjectPickup()
    // {
    //     //Replaced it with trigger collider because the raycast was not accurate when the distance was increased or decreased.
    //     interactCol.gameObject.AddComponent(typeof(FixedJoint));
    //     interactCol.transform.position = pivotDummy.position;

    //     interactCol.gameObject.GetComponent<FixedJoint>().connectedBody = rgCourageRightHand;
    //     interactCol.GetComponent<Rigidbody>().useGravity = false;
    //     Debug.LogWarning("Object Picked Up");
    // }

    // void ObjectDrop()
    // {
    //     //To avoid the three lines of code to not run. I moved those three lines of code under the DropObject Class.
    //     // if (onObjectDropEvent != null)
    //     // {
    //     //     isPickingObject = false;
    //     //     onObjectDropEvent();
    //     // }

    //     interactCol.GetComponent<Rigidbody>().useGravity = true;
    //     Destroy(interactCol.gameObject.GetComponent<FixedJoint>());
    //     Debug.LogWarning("Object LetGo");
    // }

    // void ObjectThrow()
    // {
    //     Destroy(interactCol.gameObject.GetComponent<FixedJoint>());
    //     Rigidbody objectRg = interactCol.GetComponent<Rigidbody>();
    //     objectRg.AddForce(this.gameObject.transform.up * throwingForce + this.gameObject.transform.forward * throwingForce, ForceMode.Impulse);
    //     objectRg.useGravity = true;
    //     Debug.LogWarning("Object Thrown");
    // }

    // void ObjectPushandPull()
    // {
    //     interactCol.gameObject.AddComponent(typeof(FixedJoint));
    //     interactCol.gameObject.GetComponent<FixedJoint>().enableCollision = true;
    //     interactCol.gameObject.GetComponent<FixedJoint>().connectedBody = rg;
    //     interactCol.GetComponent<Rigidbody>().isKinematic = false;
    //     interactCol.GetComponent<Rigidbody>().useGravity = false;
    //     Debug.LogWarning("Object Attached");
    // }
    #endregion

    void Jump()
    {
        if (jumpTime > jumpDelay)
        {
            moveDirection.y = jumpPower;
            anim.Play("CourageJump");
            Debug.LogWarning("Jump");
            jumpTime = 0f;
        }
    }
}
