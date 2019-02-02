using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool onLadder;
    public float walkingSpeed;
    public float runningSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float climbSpeed;
    public float sprintClimbSpeed;
    public float defaultGravity;
    public float pushForce;

    private float gravity;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController charController;
    private Animator anim;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        gravity = defaultGravity;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Checks if the player is on the Ground
        if (!onLadder)
        {
            if (charController.isGrounded)
            {
                //Gets Player Inputs
                moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
                if (Input.GetAxis("Vertical") > 0)
                    anim.SetBool("isWalking", true);
                else
                    anim.SetBool("isWalking", false);

                if (Input.GetAxis("Vertical") < 0)
                    anim.SetBool("isWalkingBack", true);
                else
                    anim.SetBool("isWalkingBack", false);

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
    }

    //Function which checks what hit the Character Controller's Collider
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Rigidbody rg = hit.collider.attachedRigidbody;

        // if (rg == null || rg.isKinematic)
        //     return;

        // if (hit.moveDirection.y < -0.3)
        //     return;

        // Vector3 dir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        // rg.velocity = dir * pushForce;

        if (hit.collider.tag == "Ropes" && Input.GetKey(KeyCode.E))
            onLadder = true;
        else
            onLadder = false;
    }
}
