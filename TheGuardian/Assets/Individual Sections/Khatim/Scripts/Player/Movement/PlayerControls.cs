﻿using System.Collections;
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
    public float interactionWalkSpeed;
    public float climbSpeed;
    public float crouchRunSpeed;
    public float maxClampValue;
    [Range(0f, 10.0f)]
    public float rotationSpeed;
    [Range(0f, 5f)]
    public float pushRotationSpeed;
    public float crouchColShrinkValue; //Initial Value is 0.5f
    public float crouchColCenterValue; //Initial Value is 2
    [Header("Player Audio")]
    public AudioSource jumpAud;
    public AudioSource ropeBreakAud;
    public AudioSource footStepAud;
    public float lowPitchRange; // 0.75F;
    public float highPitchRange; // 1.5F;

    [Header("Player Jump Variables")]
    public float jumpPower;
    private LightMechanic lightMechanic;
    public bool running;

    [Header("Player Gravity Variables")]
    public float defaultGravity;
    public float gravityAfterRopeBreak;
    #endregion

    #region Events
    public delegate void SendEvents();
    public static event SendEvents onChangeLevelToHallway;
    public static event SendEvents onChangeLevelText;
    public static event SendEvents onRopeBreak;
    public static event SendEvents onDeadPlayer;
    public static event SendEvents onHallwayFadeout;
    #endregion

    #region Player Movement
    [Header("Player Movement Variables")]
    public bool isPushingOrPulling;
    public bool isPickingObject;
    public bool onRope;
    public bool isCrouching;
    public bool isPickingUp;
    public float pushPower;
    #endregion

    #region Trigger Colliders
    private Collider interactCol;
    #endregion

    [SerializeField] private float gravity = default;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private PlayerInteraction plyInteract;
    private Animator courageAnim;

    #region Cheats
    [Header("Cheats Section :3")]
    [SerializeField] private float flashSpeed = default;
    [SerializeField] private float defaultRunningSpeed = default;
    #endregion

    private float playerHeight;
    private float moveHorizontal;
    private float moveVertical;
    private float playerCenter;

    void OnEnable()
    {
        charController = GetComponent<CharacterController>();
        lightMechanic = GetComponent<LightMechanic>();
        gravity = defaultGravity;
        courageAnim = GetComponent<Animator>();
        playerHeight = charController.height;
        playerCenter = charController.center.y;

        RatFSM.onPlayerDeath += OnPlayerDeathReceived_2;
        RatBlockerFSM.onPlayerDeath += OnPlayerDeathReceived_2;
        PaintingsAI.onPlayerDeath += OnPlayerDeathReceived;
        EventManager.onAtticFadeOut += OnPlayerFallReceived;
    }

    void OnDisable()
    {
        RatFSM.onPlayerDeath -= OnPlayerDeathReceived_2;
        RatBlockerFSM.onPlayerDeath -= OnPlayerDeathReceived_2;
        PaintingsAI.onPlayerDeath -= OnPlayerDeathReceived;
        EventManager.onAtticFadeOut -= OnPlayerFallReceived;
    }

    void OnDestroy()
    {
        RatFSM.onPlayerDeath -= OnPlayerDeathReceived_2;
        RatBlockerFSM.onPlayerDeath -= OnPlayerDeathReceived_2;
        PaintingsAI.onPlayerDeath -= OnPlayerDeathReceived;
        EventManager.onAtticFadeOut -= OnPlayerFallReceived;
    }

    void Update()
    {
        //Gets Player Inputs
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Interact") && plyInteract != null && !isCrouching)
            plyInteract.StartInteraction();
        else if (Input.GetButton("Interact") && plyInteract != null && !isCrouching)
            plyInteract.UpdateInteraction();
        if (Input.GetButtonUp("Interact") && plyInteract != null && !isCrouching)
            plyInteract.EndInteraction();

        if (!onRope)
        {
            //Storing Player's Character Controllers Height and Center
            float localHeight = playerHeight;
            float localCenter = playerCenter;

            //Checks if the player is on the Ground
            if (charController.isGrounded)
            {
                //Applies Movement
                moveDirection = new Vector3(-moveVertical, 0.0f, moveHorizontal);
                var multiplier = Mathf.Clamp01(moveDirection.magnitude) * Mathf.Lerp(walkingSpeed, runningSpeed, Input.GetAxis("Run"));
                moveDirection = moveDirection.normalized;

                //Add Input Floats to Blend Tee "speed" Parameters
                courageAnim.SetFloat("speed", multiplier / runningSpeed);

                //Applies Roatation relative to What Key is Pressed
                if (moveDirection != Vector3.zero && !isPushingOrPulling && !isPickingUp)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);

                if (isPushingOrPulling && moveDirection != Vector3.zero)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), pushRotationSpeed * Time.deltaTime);

                if (Input.GetButton("Run") && !isCrouching && !isPickingObject && !isPushingOrPulling)
                {
                    moveDirection *= multiplier;
                    // Debug.Log("Running");
                }
                else if (Input.GetButton("Crouch") && !isPushingOrPulling && !isPickingObject)
                {
                    //Made Character Controller Collider Shrink Variables Dynamic
                    //Player Crouching
                    localHeight = playerHeight * crouchColShrinkValue;
                    localCenter = playerCenter / crouchColCenterValue;
                    isCrouching = true;
                    moveDirection *= crouchWalkSpeed;
                    courageAnim.SetBool("isCrouching", true);
                    // Debug.Log("Crouch Walk");
                }
                else if (isPushingOrPulling || isPickingObject)
                {
                    moveDirection *= interactionWalkSpeed;
                    // Debug.Log("Interaction Walk");
                }
                else
                {
                    moveDirection *= multiplier;
                    isCrouching = false;
                    courageAnim.SetBool("isCrouching", false);
                    // Debug.Log("Walking");
                }

                if (Input.GetButtonDown("Jump") && !isPushingOrPulling && !isPickingObject)
                    Jump();

                charController.height = Mathf.Lerp(charController.height, localHeight, 5 * Time.deltaTime);
                charController.center = new Vector3(0, Mathf.Lerp(charController.center.y, localCenter, 5 * Time.deltaTime), 0);
                //Mathf.Clamp(charController.center.y, 0.05f, 0.1f);
            }
            else
                moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection = new Vector3(0.0f, moveVertical * climbSpeed, 0.0f);
            courageAnim.SetFloat("speed", moveVertical);
        }

        if (interactCol != null && Input.GetButton("Interact") && interactCol.tag == "Rope")
        {
            onRope = true;
            courageAnim.SetBool("isOnRope", true);
        }
        else
        {
            onRope = false;
            courageAnim.SetBool("isOnRope", false);
        }
        if (!isPickingUp)
            charController.Move(moveDirection * Time.deltaTime);

        if (Input.GetButton("Run"))
            running = true;
        if (!Input.GetButton("Run"))
            running = false;
    }

    #region Cheats :P
    public void FlashSpeedToggle(bool isFlash)
    {
        if (isFlash)
            runningSpeed = flashSpeed;
        else
            runningSpeed = defaultRunningSpeed;
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp" && plyInteract == null)
        {
            plyInteract = GetComponent<ObjectPickup>();
            plyInteract.interactCol = other;

            if (Input.GetButton("Interact"))
                plyInteract.StartInteraction();

            // Debug.Log("Key Reference Added");
        }

        if (other.tag == "PushAndPull" && plyInteract == null)
        {
            plyInteract = GetComponent<ObjectPushAndPull>();
            plyInteract.interactCol = other;

            if (Input.GetButton("Interact"))
                plyInteract.StartInteraction();

            // Debug.Log("Push and Pull Reference Added");
        }

        if (other.tag == "PickupCheese" && plyInteract == null)
        {
            plyInteract = GetComponent<ObjectCheesePickup>();
            plyInteract.interactCol = other;

            if (Input.GetButton("Interact"))
                plyInteract.StartInteraction();

            // Debug.Log("Cheese Reference Added");
        }

        if (other.tag == "Rope")
        {
            interactCol = other;
        }

        if (other.tag == "RopeBreak")
        {
            if (onRopeBreak != null)
                onRopeBreak();

            gravity = gravityAfterRopeBreak;
            // Debug.Log("Rope Broken");
        }

        if (other.gameObject.tag == "End")
        {
            if (onChangeLevelToHallway != null && onChangeLevelText != null)
            {
                onChangeLevelToHallway();
                onChangeLevelText();
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

            if (other.tag == "PickupCheese" && plyInteract.interactCol == other)
                ResetInteraction();

            // Debug.Log("Interaction Script Reference Removed");
        }

        if (other.tag == "Rope")
            interactCol = null;
    }

    void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpPower * Mathf.Abs(defaultGravity) * 2);
        moveDirection.y = jumpForce;
        jumpAud.Play();

        if (!lightMechanic.lightOn)
            courageAnim.SetTrigger("isJumping"); // Courage Jump Animation
        else
            courageAnim.SetTrigger("isJumpingWithTorch"); // Courage Jump Animation with Light
    }

    public void ResetInteraction()
    {
        plyInteract.interactCol = null;
        plyInteract = null;
    }

    void CanMove()
    {
        isPickingUp = false;
    }

    void PlayFootStep()
    {
        footStepAud.pitch = Random.Range(lowPitchRange, highPitchRange);
        footStepAud.Play();
        // Debug.Log("Footstep Audio Playing");
    }

    void OnPlayerDeathReceived()
    {
        courageAnim.SetTrigger("dead");
    }

    void OnPlayerDeathReceived_2()
    {
        courageAnim.SetTrigger("dead_2");
        isPickingUp = true;
    }

    void OnPlayerFallReceived()
    {
        courageAnim.SetTrigger("dead_2");
    }

    void ShowDeathUI()
    {
        if (onDeadPlayer != null)
            onDeadPlayer();
    }

    void FadeOut()
    {
        if (onHallwayFadeout != null)
            onHallwayFadeout();
    }
}