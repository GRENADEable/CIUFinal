using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeModifier : MonoBehaviour
{
    public float maxAngleFromGround;
    public float AngleFromGround;

    public PlayerController playerController;

    protected internal Vector3 playerCalculatedForwardVector;
    private RaycastHit hitInfo;


    // Use this for initialization
    void Start ()
    {
       
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Physics.Raycast(transform.position, -Vector3.up,out hitInfo, 15);
        CalculateTheForwardVectorOfPlayer();
        CalculateAngleFromGroundToPlayerForward();

        if (AngleFromGround >= maxAngleFromGround)
        {
            playerController.canMove = false;
        }
        else
            playerController.canMove = true;

        Debuging();
	}

    public void CalculateTheForwardVectorOfPlayer()
    {
        
        if (playerController.NotGrounded())
        {
            playerCalculatedForwardVector = transform.forward;
        }
        else
        
            playerCalculatedForwardVector = Vector3.Cross(transform.right, hitInfo.normal);
    }

    public void CalculateAngleFromGroundToPlayerForward()
    {
        
        if (playerController.NotGrounded())
        {
            AngleFromGround = 90;
        }
        else
        
            AngleFromGround = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    public void Debuging()
    {
        Debug.DrawLine(transform.position, transform.position + playerCalculatedForwardVector * 15 * 2, Color.red);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 15, Color.blue);
        Debug.Log(playerController.NotGrounded());
        //  Debug.Log(hitInfo);
    }
}
