using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControlTest : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float walkingSpeed;
    public float runningSpeed;
    public float rotateSpeed;
    public float jumpPower;
    [Header("Rope Variables")]
    public bool onRope;
    public float climbSpeed;
    public float pushPower;
    public float sprintClimbSpeed;
    public float defaultGravity;
    [Header("References Obejcts")]
    public GameObject trapDoor;
    private Collider col;
    private float gravity;
    private CharacterController charController;
    [Header("Virtual Camera Reference")]
    public GameObject mainVirutalCam;
    public GameObject firstPuzzleCamPan;
    public GameObject secondPuzzleVirtualCam;
    public GameObject thirdPuzzleVirtualCam;
    [Header("Virtual Camera Variables")]
    public float foV;
    public Vector3 verticalCamOffset;
    public Vector3 horizontalCamOffset;
    public float floatT;
    private Vector3 moveDirection = Vector3.zero;
    [Header("Cheats Section :3")]
    [SerializeField]
    private float runningCheat;
    [SerializeField]
    private float defaultRunningSpeed;
    [SerializeField]
    private Vector3 defaultObjOffset;
    void Start()
    {
        if (mainVirutalCam != null && firstPuzzleCamPan != null && secondPuzzleVirtualCam != null && thirdPuzzleVirtualCam != null
            && trapDoor != null)
        {
            mainVirutalCam.SetActive(true);
            firstPuzzleCamPan.SetActive(false);
            secondPuzzleVirtualCam.SetActive(false);
            thirdPuzzleVirtualCam.SetActive(false);
            trapDoor.SetActive(true);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
    }
    void Update()
    {
        #region Cheats
        if (Input.GetKey(KeyCode.G))
            runningSpeed = runningCheat;

        if (Input.GetKey(KeyCode.H))
            runningSpeed = defaultRunningSpeed;

        if (Input.GetKeyDown(KeyCode.J))
        {
            mainVirutalCam.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = foV;
        }
        #endregion

        if (!onRope)
        {
            // transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
            float moveHorizontal = Input.GetAxis("Vertical");
            float moveVertical = Input.GetAxis("Horizontal");
            // float moveCamVertical = Input.GetAxis("MoveCamVertical");

            if (charController.isGrounded)
            {
                //Gets Player Inputs
                // moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));


                //Applies Movement
                // moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = new Vector3(-moveHorizontal, 0.0f, moveVertical);

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

                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpPower;
                    // Debug.LogWarning("Jump");
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                    var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                    camOffset.m_TrackedObjectOffset = verticalCamOffset;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                    var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                    camOffset.m_TrackedObjectOffset = -verticalCamOffset;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                    var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                    camOffset.m_TrackedObjectOffset = horizontalCamOffset;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                    var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                    camOffset.m_TrackedObjectOffset = -horizontalCamOffset;
                }
                else
                {
                    CinemachineVirtualCamera vCam = mainVirutalCam.GetComponent<CinemachineVirtualCamera>();
                    var camOffset = vCam.GetCinemachineComponent<CinemachineComposer>();
                    camOffset.m_TrackedObjectOffset = defaultObjOffset;
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

        if (col != null && Input.GetKey(KeyCode.E))
            onRope = true;

        else
            onRope = false;

        charController.Move(moveDirection * Time.deltaTime);

        if (onRope && Input.GetKeyUp(KeyCode.E) && !charController.isGrounded)
            onRope = false;

        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     var camOffset = mainVirutalCam.GetComponent<CinemachineCameraOffset>();
        //     camOffset.m_Offset = trackedObjOffset;
        // }
        // else
        // {
        //     var camOffset = mainVirutalCam.GetComponent<CinemachineCameraOffset>();
        //     camOffset.m_Offset = Vector3.zero;
        // }
    }

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

        if (other.tag == "Rope")
        {
            col = other;
        }

        if (other.tag == "RopeBreak" && trapDoor != null)
        {
            Destroy(other.GetComponent<HingeJoint>());
            Destroy(other.GetComponent<Collider>());
            trapDoor.SetActive(false);
            // other.gameObject.SetActive(false);
            Debug.LogWarning("Rope Broken");
        }

        if (other.gameObject.tag == "End")
        {
            this.gameObject.SetActive(false);
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

        if (other.tag == "Rope")
        {
            col = null;
        }
    }
}
