using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 0.5f;

    public Transform groundCheck;
    public float groundDistande = 0.4f;
    public LayerMask groundMask;
   

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistande, groundMask);

        //prevent from multiple jumps
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //move character
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Increase movement speed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = 30f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 20f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

 
}
