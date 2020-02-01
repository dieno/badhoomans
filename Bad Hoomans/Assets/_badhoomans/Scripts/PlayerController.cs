using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;

    [SerializeField] private float gravity = 0.1f;
    [SerializeField] private float drag = 0.5f;

    private float velocityY;

    private Vector3 velocity = Vector3.zero;

    private bool isGrounded = false;

    private float groundY = 0f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        groundY = transform.position.y;

        transform.position = new Vector3(transform.position.x, transform.position.x + 25f, transform.position.z);

        rb = GetComponent<Rigidbody2D>();

        //isGrounded = true;

        //if(groundY == transform.position.y)
        //{
        //    isGrounded = true;
        //}

        //isGrounded();
    }

    //private bool check()
    //{
    //    return (groundY == transform.position.y);
    //}

    // Update is called once per frame
    void Update() 
    {


        //if(!isGrounded)
        velocity.y -= gravity;

        //if (velocity.y > 0f)
        //{
        //    velocity.y += gravity;
        //}
        //else
        //{
        //    velocity.y = 0f;
        //}

        //if (velocity.x > 0f)
        //{
            velocity.x *= drag;
        //}
        //else
        //{
        //    velocity.x = 0f;
        //}


        //Vector3 movement = new Vector3
        //{
        //    x = Input.GetAxis("Horizontal"),
        //    y = 0f,
        //    z = 0f,
        //}.normalized;

        ////movement *= speed * Time.deltaTime;

        ////transform.Translate(movement);


        if (Input.GetButton("Jump") && isGrounded)
        {
            OnJump();
        }

        velocity.x = Input.GetAxis("Horizontal") * speed;




        //velocityY += gravity * Time.deltaTime;


        //transform.position += velocity * Time.deltaTime;
        rb.MovePosition(transform.position + velocity * Time.deltaTime);

        


        if(transform.position.y < groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            isGrounded = true;
            velocity.y = 0f;
        }


        //Debug.Log(velocity);
    }

    void OnJump()
    {
        velocity.y = jumpForce;
        isGrounded = false;
    }
}
