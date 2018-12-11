using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float lockRotation = 0f;
    public float magnitudeToClamp;
    public float jumpStrenght;
    public float climbSpeed;

    public bool climb = false;

    public Animator playerAnimator;

    private float moveInput;
    private float rotateInput;

    private Vector3 playerInput;

    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    public Collider playerCollider;

    private RaycastHit hit;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        PlayerInputs();
        transform.rotation = Quaternion.Euler(lockRotation, transform.rotation.eulerAngles.y, lockRotation);
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (Input.GetKeyDown(KeyCode.Space) && NotGrounded())
            Jump();
    }

    void FixedUpdate()
    {
        PlayerTurn();
        if (moveInput != 0 && NotGrounded() && !climb)
        {
            PlayerMove();
        }
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
        playerRb.velocity = vectorOfMovement;
        //playerRb.AddForce(transform.forward * moveInput * moveSpeed, ForceMode.Impulse);
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    private bool NotGrounded()
    {
        float DistanceFromGround;
        DistanceFromGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, DistanceFromGround + 0.1f);
    }

    private void Jump()
    {
        playerRb.AddForce(new Vector3(0f, jumpStrenght, 0f), ForceMode.Impulse);
        //playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public void OnTriggerStay(Collider collider)
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
}
