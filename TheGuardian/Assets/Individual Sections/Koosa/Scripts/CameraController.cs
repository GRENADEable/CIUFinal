using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Public Variables
    public Transform lookAt;
    public Transform playerCameraTransform;
    public float zDistance;
    public float xDistance;
    public float yDistance;
    #endregion
    #region Private Variables
    // private Camera playerCamera;
    private float currentX = 0f;
    // private float sensitivityX = 4f;
    //private float sensitivityY = 1f;
    #endregion
    #region Unity Callbacks
    // Use this for initialization
    void Start()
    {
        playerCameraTransform = transform;
        // playerCamera = gameObject.GetComponent<Camera>();
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
    }
    private void LateUpdate()
    {
        Vector3 direction = new Vector3(-xDistance, -yDistance, -zDistance);
        Quaternion rotation = Quaternion.Euler(0, currentX, 0);
        playerCameraTransform.position = lookAt.position + rotation * direction;
        playerCameraTransform.LookAt(lookAt.position);
    }
}
#endregion