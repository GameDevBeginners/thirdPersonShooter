using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_scp : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float playerSpeed = 10.0f;
    float dummy;
    [SerializeField]
    float gravity = 20.0f;
    [SerializeField]
    float jumpHeight = 5.0f;
    [SerializeField]
    float playerDownSpeed = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 inputVector = new Vector3(horizontalInput, 0, forwardInput);

        float turnAngle = Mathf.Atan2(horizontalInput, forwardInput) * Mathf.Rad2Deg;
        float finalAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, turnAngle+cam.transform.eulerAngles.y, ref dummy, 0.1f); 
        transform.rotation =  Quaternion.Euler(0, finalAngle, 0);
        Vector3 playerDirection =  Quaternion.Euler(0, finalAngle, 0) * Vector3.forward;

        if (inputVector.magnitude > 0)
        {
            controller.Move(playerDirection * playerSpeed * Time.deltaTime);
        }

        if(controller.isGrounded)
        {
            playerDownSpeed = 0;
            if(Input.GetKey(KeyCode.Space))
            {
                playerDownSpeed = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
        }
        else
        {
            playerDownSpeed -= gravity * Time.deltaTime;
        }
        controller.Move(new Vector3(0, playerDownSpeed * Time.deltaTime, 0));
    }
}
