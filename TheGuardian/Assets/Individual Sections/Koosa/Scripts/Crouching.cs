using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    //For Khatim
    //uses the character controller component attatched to its gameobject

    public float crouchSpeed;
    public float normalSpeed; // this is the walk speed in ur playercontrols script
    private CharacterController characterController;
    private Vector3 playerVector;
    private float playerHeight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerHeight = characterController.height;
        playerVector = transform.position;
    }

    void Update()
    {
        float localHeight = playerHeight;
        float localSpeed = normalSpeed;
        // Vector3 scale = new Vector3(1, characterController.height, 1);
        // transform.localScale = scale;

        if (/*if the player is on the floor subtitute booleon here from ur script*/ Input.GetKey(KeyCode.C))
        {
            localHeight = playerHeight * 0.5f;
            localSpeed = crouchSpeed;
        }

        float latestRecordedHeight = characterController.height;
        characterController.height = Mathf.Lerp(characterController.height, localHeight, 5 * Time.deltaTime);
        playerVector.y += (characterController.height - latestRecordedHeight) / 1.5f;
    }

    public void CrouchingCheck()
    {
        float localHeight = 0;
        float localSpeed = 0;

        if (/*if the player is on the floor subtitute booleon here from ur script*/ Input.GetKey(KeyCode.C))
        {
            CrouchingExecution(localSpeed, localHeight);
        }

    }

    public void CrouchingExecution(float speed, float height)
    {
        height = playerHeight * 0.5f;
        speed = crouchSpeed;
    }

    public void CharacterControllerBodyModifier()
    {
        float latestRecordedHeight = characterController.height;
        //characterController.height = Mathf.Lerp(characterController.height);
    }
}
