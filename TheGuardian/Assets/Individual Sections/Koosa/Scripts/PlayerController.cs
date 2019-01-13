using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float climbSpeed;
    public float rotateSpeed;

    public float lockRotation = 0f;
    public float magnitudeToClamp;

    public float jumpStrenght;


    public bool climb = false;
    public bool crouch = false;
    public bool sprinting = false;
    public bool canMove = true;

    public Animator playerAnimator;
    public SlopeModifier slopeModifier;

    private float moveInput;
    private float rotateInput;
    private float playerHeight;
    private float moveSpeed;
    private float inputDelay = 0.1f;

    private Vector3 playerInput;
    private Vector3 defaultVector;


    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private Collider playerCollider;
    [SerializeField]
    private Transform playerTransform;

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
        //transform.rotation = Quaternion.Euler(lockRotation, transform.rotation.eulerAngles.y, lockRotation);
       // playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (Input.GetKeyDown(KeyCode.Space) && NotGrounded())
            Jump();
    }

    void FixedUpdate()
    {
        PlayerInputs();

        if (!climb && Mathf.Abs(rotateInput) > inputDelay)
            PlayerTurn();
        if (moveInput != 0 && /*NotGrounded() &&*/ !climb && canMove && Mathf.Abs(moveInput) > inputDelay)
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
        //transform.Rotate(0, rotateInput * rotateSpeed, 0);
        transform.rotation *= Quaternion.AngleAxis(rotateInput * rotateSpeed * Time.deltaTime, Vector3.up);
    }

    private void PlayerMove()
    {
        Vector3 directionOfMovement = slopeModifier.playerCalculatedForwardVector * moveInput;
        Vector3 vectorOfMovement = Vector3.ClampMagnitude(directionOfMovement, magnitudeToClamp);
         //playerRb.velocity = directionOfMovement;
        playerRb.AddForce(vectorOfMovement, ForceMode.Impulse);
       // playerRb.MovePosition(transform.position + vectorOfMovement.normalized * moveSpeed * Time.deltaTime);
        // playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ ;
        // playerRb.velocity = moveSpeed * playerRb.velocity.normalized;
    }

    public bool NotGrounded()
    {
        float distanceFromGround;
        distanceFromGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up,distanceFromGround);
    }

    private void Jump()
    {
        playerRb.AddForce(new Vector3(0f, jumpStrenght, 0f), ForceMode.Impulse);
       // playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
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
        else if (NotGrounded() && Input.GetKeyUp(KeyCode.E) && climb)
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
