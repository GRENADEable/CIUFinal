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
    [Header("References Obejcts")]
    public GameObject levelTitleText;
    public GameObject pausePanel;
    private float gravity;
    private CharacterController charController;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private bool climb;

    void Start()
    {
        if (levelTitleText != null && pausePanel != null)
        {
            levelTitleText.SetActive(true);
            pausePanel.SetActive(false);
        }
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
    }
    void Update()
    {
        if (charController.isGrounded)
        {
            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
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
        }
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseorUnpause();
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

    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     Rigidbody rg = hit.collider.attachedRigidbody;

    //     if (rg == null || rg.isKinematic)
    //         return;

    //     if (hit.moveDirection.y < -0.3)
    //         return;

    //     Vector3 dir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    //     rg.velocity = dir * pushForce;
    // }


}
