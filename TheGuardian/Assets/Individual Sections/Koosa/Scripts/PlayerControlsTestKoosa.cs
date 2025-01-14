﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerControlsTestKoosa : MonoBehaviour
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
    public LightMechanic lightMechanic;
    public bool running;

    [Header("Player Jump Variables")]
    public float jumpPower;

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
    public static event SendEvents onRopeBreakMessage;
    public static event SendEvents onObjectBendPlank;
    public static event SendEvents onKeyMove;
    #endregion

    #region  Object Interaction
    [Header("Object Interaction Variables")]
    public bool isPickingObject;
    public bool isPushingOrPulling;
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

    #region Cheats
    [Header("Cheats Section")]
    [SerializeField] private float flashSpeed = default;
    [SerializeField] private float defaultRunningSpeed = default;
    [SerializeField] private float superJump = default;
    [SerializeField] private float defaultJump = default;
    #endregion

    private float playerHeight;
    private float moveHorizontal;
    private float moveVertical;
    private float playerCenter;
    public bool crouching;
    void OnEnable()
    {
        charController = GetComponent<CharacterController>();

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (onKeyMove != null)
                onKeyMove();
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
                if ((moveVertical == 0 || moveVertical == 0 || moveHorizontal == 0 || moveHorizontal == 0) && !crouching && lightMechanic.lightOn)
                {
                    anim.SetBool("Light", true);
                }
                else
                    anim.SetBool("Light", false);

                if ((moveVertical == 0 || moveVertical == 0 || moveHorizontal == 0 || moveHorizontal == 0) && crouching && lightMechanic.lightOn)
                {
                    anim.SetBool("LightCrouch", true);
                }
                else
                    anim.SetBool("LightCrouch", false);

                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && !crouching && lightMechanic.lightOn)
                {
                    anim.SetBool("LightWalk", true);
                }
                else
                    anim.SetBool("LightWalk", false);



                //Animation Controls
                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && !crouching && !lightMechanic.lightOn)
                {
                    anim.SetBool("isWalking", true);
                    running = false;
                }
                else
                    anim.SetBool("isWalking", false);
                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && crouching && !lightMechanic.lightOn)
                    anim.SetBool("isCrouchWalking", true);
                else
                    anim.SetBool("isCrouchWalking", false);

                if ((moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0) && lightMechanic.lightOn && crouching)
                {
                    anim.SetBool("LightCrouchWalk", true);
                }
                else
                    anim.SetBool("LightCrouchWalk", false);

                //Applies Movement
                moveDirection = new Vector3(-moveVertical, 0.0f, moveHorizontal);

                //Applies Roatation relative to What Key is Pressed
                if (moveDirection != Vector3.zero && !isPushingOrPulling)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15f);




                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C) && lightMechanic.lightOn && (moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0))
                {
                    anim.SetBool("LightRun", true);

                }
                else
                    anim.SetBool("LightRun", false);





                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C) && (moveVertical > 0 || moveVertical < 0 || moveHorizontal > 0 || moveHorizontal < 0))
                {
                    moveDirection = moveDirection * runningSpeed;
                    anim.SetBool("isRunning", true);
                    running = true;
                    // Debug.Log("Running");
                }
                else if (Input.GetKey(KeyCode.C) && !isPushingOrPulling)
                {
                    running = false;
                    //Made Character Controller Collider Shrink Variables Dynamic
                    localHeight = playerHeight * crouchColShrinkValue;
                    localCenter = playerCenter / crouchColCenterValue;
                    anim.SetBool("isCrouching", true);
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
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isCrouching", false);
                    crouching = false;
                    // Debug.Log("Walking");
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
            {
                plyInteract.StartInteraction();
            }
        }

        if (other.tag == "Rope")
        {
            interactCol = other;
        }

        if (other.tag == "PushAndPull" && plyInteract == null)
        {
            plyInteract = GetComponent<ObjectPushAndPull>();
            plyInteract.interactCol = other;

            if (Input.GetKey(KeyCode.E))
            {
                plyInteract.StartInteraction();
                anim.Play("CouragePush");
            }
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
        if (plyInteract != null)
        {
            if (other.tag == "PickUp" && plyInteract.interactCol == other)
            {
                ResetInteraction();
            }

            if (other.tag == "PushAndPull" && plyInteract.interactCol == other)
            {
                ResetInteraction();
            }
        }

        if (other.tag == "Rope")
        {
            interactCol = null;
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

    void Jump()
    {
        if (jumpTime > jumpDelay && !lightMechanic.lightOn)
        {
            moveDirection.y = jumpPower;
            anim.Play("CourageJump");
            Debug.Log("Jump");
            jumpTime = 0f;
        }
        if (jumpTime > jumpDelay && lightMechanic.lightOn)
        {
            moveDirection.y = jumpPower;
            anim.Play("CourageJumpLight");
            Debug.Log("Jump");
            jumpTime = 0f;
        }
    }

    public void ResetInteraction()
    {
        plyInteract.interactCol = null;
        plyInteract = null;
    }
}