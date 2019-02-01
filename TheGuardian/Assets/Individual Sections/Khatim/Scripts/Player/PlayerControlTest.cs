using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    public float walkingSpeed;
    public float runningSpeed;
    public float rotateSpeed;
    public float raycastDistance;
    public float climbSpeed;
    public float jumpStrength;
    public Vector3 raycastHeight;
    public float pushForce;
    public float defaultGravity;
    public GameObject trapDoor;
    public float interactionDistance;
    [Header("References Obejcts")]
    public GameObject levelTitleText;
    public GameObject pausePanel;
    public GameObject toBeContinuedPanel;
    private float gravity;
    private CharacterController charController;
    [Header("Virtual Camera Reference")]
    public GameObject virtualCam1;
    public GameObject virtualCam2;
    public GameObject virtualCam3;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private bool climb;
    [SerializeField]
    private bool isInteracting;

    void Start()
    {
        if (levelTitleText != null && pausePanel != null && trapDoor != null && toBeContinuedPanel != null && virtualCam1 != null && virtualCam2 != null && virtualCam3 != null)
        {
            levelTitleText.SetActive(true);
            pausePanel.SetActive(false);
            trapDoor.SetActive(true);
            toBeContinuedPanel.SetActive(false);
            virtualCam1.SetActive(true);
            virtualCam2.SetActive(false);
            virtualCam3.SetActive(false);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
    }
    void Update()
    {
        //Checks if the player is on the Ground
        if (charController.isGrounded)
        {
            //Gets Player Input
            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));

            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

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

            if (Input.GetKeyDown(KeyCode.Space) && !climb)
            {
                moveDirection.y = jumpStrength;
            }

            Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseorUnpause();

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

    //Function which checks what hit the Character Controller's Collider
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rg = hit.collider.attachedRigidbody;

        if (rg == null || rg.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3)
            return;

        Vector3 dir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rg.velocity = dir * pushForce;

        if (hit.collider.tag == "Rope")
        {
            trapDoor.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondLevelCameraPan")
        {
            virtualCam1.SetActive(false);
            virtualCam3.SetActive(false);
            virtualCam2.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdLevelCameraPan")
        {
            virtualCam1.SetActive(false);
            virtualCam2.SetActive(false);
            virtualCam3.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SecondLevelCameraPan")
        {
            virtualCam3.SetActive(false);
            virtualCam2.SetActive(false);
            virtualCam1.SetActive(true);
        }

        if (other.gameObject.tag == "ThirdLevelCameraPan")
        {
            virtualCam3.SetActive(false);
            virtualCam2.SetActive(false);
            virtualCam1.SetActive(true);
        }
    }
}
