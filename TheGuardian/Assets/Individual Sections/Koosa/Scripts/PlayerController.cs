using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float crouchSpeed;
    public float sprintSpeed;
    public float walkSpeed;
    public float rotateSpeed;
    public float lockRotation = 0f;
    public float magnitudeToClamp;
    public float jumpStrenght;
    public float climbSpeed;

    public bool climb = false;
    public bool crouch = false;
    public bool sprinting = false;

    public Animator playerAnimator;

    private float moveInput;
    private float rotateInput;
    public float playerHeight;

    private Vector3 playerInput;
    private Vector3 defaultVector;

    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Collider playerCollider;
    [SerializeField]
    public Transform playerTransform;

    private RaycastHit hit;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerTransform = GetComponent<Transform>();
        playerHeight = this.gameObject.transform.localScale.y;
        defaultVector = transform.localScale;
    }

    void Update()
    {
        //playerHeight = this.gameObject.transform.localScale.y;


        PlayerInputs();
        transform.rotation = Quaternion.Euler(lockRotation, transform.rotation.eulerAngles.y, lockRotation);
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (Input.GetKeyDown(KeyCode.Space) && NotGrounded())
            Jump();
    }

    void FixedUpdate()
    {
        if(!climb)
        PlayerTurn();
        if (moveInput != 0 && /*NotGrounded() &&*/ !climb)
        {
            PlayerMove();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
            Debug.Log("running running ");
        }
        else
            moveSpeed = walkSpeed;
    }

    private void PlayerInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Horizontal");
    }

    private void PlayerTurn()
    {
        transform.Rotate(0, rotateInput * rotateSpeed, 0);
    }

    private void PlayerMove()
    {
        Vector3 directionOfMovement = transform.forward * moveInput * moveSpeed;
        Vector3 vectorOfMovement = Vector3.ClampMagnitude(directionOfMovement, magnitudeToClamp);
       // playerRb.velocity = vectorOfMovement;
        playerRb.AddRelativeForce(transform.forward * moveInput * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        playerRb.velocity = moveSpeed * playerRb.velocity.normalized;
    }

    private bool NotGrounded()
    {
        float DistanceFromGround;
        DistanceFromGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, DistanceFromGround);
    }

    private void Jump()
    {
        playerRb.AddForce(new Vector3(0f, jumpStrenght, 0f), ForceMode.Impulse);
        //playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public void OnCollisionStay(Collision collider)
    {
        if (climb)
            playerRb.velocity = Vector3.up * climbSpeed * moveInput;

        if (collider.gameObject.tag == "Rope" && Input.GetKeyDown(KeyCode.E) && !climb)
        {
            Debug.Log("climbing i should");
            climb = true;
        }
        else if(NotGrounded() && Input.GetKeyUp(KeyCode.E) && climb)
        {
            Debug.Log(" i should kinda land now");
            climb = false;
        }
    }


    public void Sprint()
    {
        moveSpeed = sprintSpeed;
        sprinting = true;
        //might add more stuff
    }
}
