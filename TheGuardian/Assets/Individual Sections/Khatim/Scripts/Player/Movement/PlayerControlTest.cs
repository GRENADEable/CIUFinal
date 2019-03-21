using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    [Header("Player Jump Variables")]
    public float jumpPower;
    public float jumpDelay;
    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    public float pushPower;
    public float sprintClimbSpeed;
    public float defaultGravity;
    [Header("References Obejcts")]
    public GameObject trapDoor;
    [Header("Player Push and Pull Variables")]
    public float interactionDistance;
    public float interactionDistanceHeight;

    public delegate void SendEvents();
    public static event SendEvents onObjectDetatchEvent;
    public static event SendEvents onObjectShakePlank;
    public static event SendEvents onObjectStillPlank;

    [SerializeField]
    private Collider ropeCol;
    [SerializeField]
    private Collider interactCol;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private bool isInteracting;
    private float gravity;
    private float jumpTime;
    private CharacterController charController;
    private Vector3 moveDirection = Vector3.zero;
    [Header("Cheats Section :3")]
    [SerializeField]
    private float flashSpeed;
    [SerializeField]
    private float defaultRunningSpeed;
    [SerializeField]
    private float superJump;
    private GameObject cheatPanel;
    [SerializeField]
    private float defaultJump;

    void Awake()
    {
        cheatPanel = GameObject.FindGameObjectWithTag("CheatPanel");

        if (cheatPanel != null)
        {
            cheatPanel.SetActive(false);
        }
    }

    void Start()
    {
        if (trapDoor != null)
        {
            trapDoor.SetActive(true);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
        jumpTime = jumpDelay;
    }
    void Update()
    {
        if (cheatPanel != null)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                CheatPanelToggle();
        }

        if (!onRope)
        {
            // transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
            float moveHorizontal = Input.GetAxis("Vertical");
            float moveVertical = Input.GetAxis("Horizontal");
            jumpTime += Time.deltaTime;
            // float moveCamVertical = Input.GetAxis("MoveCamVertical");

            // RaycastHit hit;
            // // Debug.DrawRay(transform.position + Vector3.up * interactionDistanceHeight, transform.TransformDirection(Vector3.forward) * interactionDistance, Color.blue);
            // bool interaction = Physics.Raycast(transform.position + Vector3.up * interactionDistanceHeight, transform.TransformDirection(Vector3.forward) * interactionDistance, out hit);

            // if (interaction && Input.GetKey(KeyCode.E) && hit.collider.tag == "Interact" && !isInteracting)
            // {
            //     // Sets bool to true, adds fixed joint component and links fixed joint from other gameobject to ours and turns off gravity.
            //     hit.collider.gameObject.AddComponent(typeof(FixedJoint));
            //     hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
            //     hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
            //     hit.rigidbody.isKinematic = false;
            //     hit.rigidbody.useGravity = false;
            //     isInteracting = true;
            //     Debug.LogWarning("Object Attached");
            // }

            // if (Input.GetKeyUp(KeyCode.E) && isInteracting && hit.collider.tag == "Interact")
            // {
            //     // Sets bool to false, removes fixed joint from the other gameobject with ours, turns on  gravity of the other object and destroys the fixed joint component.
            //     Destroy(hit.collider.gameObject.GetComponent<FixedJoint>());
            //     hit.rigidbody.useGravity = true;
            //     hit.rigidbody.isKinematic = true;
            //     isInteracting = false;
            //     Debug.LogWarning("Object Detached");
            // }

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

            if (charController.isGrounded)
            {
                //Gets Player Inputs
                // moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));


                //Applies Movement
                // moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = new Vector3(-moveHorizontal, 0.0f, moveVertical);

                if (moveDirection != Vector3.zero && !isInteracting)
                    transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15f);

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

                if (Input.GetKey(KeyCode.Space) && !isInteracting)
                {
                    Jump();
                    // Debug.LogWarning("Jump");
                }
            }
            else
            {
                moveDirection.y -= defaultGravity * Time.deltaTime;
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

        if (ropeCol != null && Input.GetKey(KeyCode.E))
            onRope = true;
        else
            onRope = false;

        charController.Move(moveDirection * Time.deltaTime);

        if (onRope && Input.GetKeyUp(KeyCode.E) && !charController.isGrounded)
            onRope = false;
    }

    public void CheatPanelToggle()
    {
        cheatPanel.SetActive(!cheatPanel.activeSelf);
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

    // Function which checks what hit the Character Controller's Collider
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

        // if (hit.collider.tag == "Rope" && Input.GetKey(KeyCode.E))
        //     onLadder = true;
        // else
        //     onLadder = false;

        if (hit.gameObject.tag == "BendPlank")
        {
            if (onObjectShakePlank != null)
            {
                onObjectShakePlank();
            }
        }
        else
        {
            if (onObjectStillPlank != null)
            {
                onObjectStillPlank();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rope")
        {
            ropeCol = other;
        }

        if (other.tag == "RopeBreak" && trapDoor != null)
        {
            Destroy(other.GetComponent<HingeJoint>());
            Destroy(other.GetComponent<Collider>());
            trapDoor.SetActive(false);
            // other.gameObject.SetActive(false);
            Debug.LogWarning("Rope Broken");
        }

        if (other.tag == "End")
        {
            this.gameObject.SetActive(false);
        }

        if (other.tag == "Interact")
        {
            interactCol = other;
        }

        if (other.tag == "BendPlank")
        {
            col = other;
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

        if (other.tag == "BendPlank")
        {
            col = null;
        }
    }
}
