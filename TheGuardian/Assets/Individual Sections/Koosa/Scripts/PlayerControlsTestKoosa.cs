using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsTestKoosa : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    public float crouchWalkSpeed;
    public float crouchRunSpeed;
    [Header("Player Jump Variables")]
    public float jumpPower;
    public float jumpDelay;
    [Header("Player Gravity Variables")]
    public float defaultGravity;

    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    public float sprintClimbSpeed;

    public delegate void SendEvents();
    public static event SendEvents onObjectDetatchEvent;


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
    private Vector3 playerVector;
    private float playerHeight;
    private float moveHorizontal;
    private float moveVertical;

    void OnEnable()
    {
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
        anim = GetComponent<Animator>();
        playerHeight = charController.height;
        playerVector = transform.position;
        jumpTime = jumpDelay;
    }

    void Update()
    {
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
            if (onObjectDetatchEvent != null) //Sends message to Gameobject with DropObject Script
                onObjectDetatchEvent();

            isInteracting = false;
        }

        if (!onRope)
        {
            float localHeight = playerHeight;
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
                if (moveDirection != Vector3.zero && !isInteracting)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15f);

                if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C))
                {
                    moveDirection = moveDirection * runningSpeed;
                    anim.SetBool("isRunning", true);
                    // Debug.LogWarning("Running");
                }
                else if (Input.GetKey(KeyCode.C) && !isInteracting)
                {
                    localHeight = playerHeight * 0.5f;
                    // anim.SetBool("isCrouching", true);
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
                    // Debug.LogWarning("Walking");
                }

                if (Input.GetKey(KeyCode.Space) && !isInteracting)
                {
                    Jump();
                }

                //Player Crouching
                float latestRecordedHeight = charController.height;
                charController.height = Mathf.Lerp(charController.height, localHeight, 5 * Time.deltaTime);
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
            anim.Play("CourageJump");
            Debug.LogWarning("Jump");
            jumpTime = 0f;
        }
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

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rope")
        {
            ropeCol = other;
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

	 //Function which checks what hit the Character Controller's Collider
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
		
    }
}
