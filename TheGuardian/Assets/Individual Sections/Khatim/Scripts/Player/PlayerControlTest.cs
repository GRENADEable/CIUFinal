using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public bool onLadder;
    public float walkingSpeed;
    public float runningSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float climbSpeed;
    public float sprintClimbSpeed;
    // public float raycastDistance;
    // public Vector3 raycastHeight;
    // public float pushForce;
    public float defaultGravity;
    public float interactionDistance;
    private float gravity;
    private CharacterController charController;
    [Header("Virtual Camera Reference")]
    public GameObject mainVirutalCam;
    public GameObject firstPuzzleCamPan;
    public GameObject secondPuzzleVirtualCam;
    public GameObject thirdPuzzleVirtualCam;
    private Vector3 moveDirection = Vector3.zero;

    // [SerializeField]
    // private bool isInteracting;

    void Start()
    {
        if (mainVirutalCam != null && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null && thirdPuzzleVirtualCam != null)
        {
            mainVirutalCam.SetActive(true);
            firstPuzzleCamPan.SetActive(false);
            secondPuzzleVirtualCam.SetActive(false);
            thirdPuzzleVirtualCam.SetActive(false);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
    }
    void Update()
    {
        if (!onLadder)
        {
            if (charController.isGrounded)
            {
                //Gets Player Inputs
                moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
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

        // if (Input.GetKeyDown(KeyCode.Escape))
        //     PauseorUnpause();

        // RaycastHit hit;
        // if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance) && Input.GetKeyDown(KeyCode.E) && hit.collider.tag == "Interact" && !isInteracting)
        // {
        //     isInteracting = true;
        //     hit.collider.gameObject.AddComponent(typeof(FixedJoint));
        //     hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = true;
        //     hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
        //     hit.rigidbody.useGravity = false;
        //     Debug.LogWarning("Object Attached");
        // }

        // else if (Input.GetKeyUp(KeyCode.E) && isInteracting)
        // {
        //     //Sets bool to false, removes fixed joint from the other gameobject with ours, turns on  gravity of the other object and destroys the fixed joint component.
        //     isInteracting = false;
        //     hit.collider.gameObject.GetComponent<FixedJoint>().enableCollision = false;
        //     hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody = null;
        //     hit.rigidbody.useGravity = true;
        //     Destroy(hit.collider.gameObject.GetComponent<FixedJoint>());
        //     Debug.LogWarning("Object Detached");
        // }
    }

    //Function which checks what hit the Character Controller's Collider
    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    // }

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
