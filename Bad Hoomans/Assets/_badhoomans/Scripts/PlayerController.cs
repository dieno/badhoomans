using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float yStartOffset = 5f;

    [SerializeField] private Transform handPivot;
    [SerializeField] private Transform hand;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravity = 0.1f;

    [SerializeField] private float handRadius = 1f;

    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded = false;
    private float groundY = 0f;
    private Vector2 currentCursorPosition = Vector2.zero;

    private bool isHovering = false;
    private bool isHolding = false;
    private GameObject objectUnderHand = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        groundY = transform.position.y;

        // start character above and let him fall in
        transform.position = new Vector3(transform.position.x, transform.position.x + yStartOffset, transform.position.z);

    }

    void Update() 
    {
        velocity.y -= gravity;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentCursorPosition = new Vector2(worldPoint.x, worldPoint.y);

        if (Input.GetButton("Jump") && isGrounded)
        {
            OnJumpStart();
        }

        if(Input.GetButtonUp("Jump") && !isGrounded)
        {
            OnJumpEnd();
        }

        velocity.x = Input.GetAxis("Horizontal") * speed;

        rb.MovePosition(transform.position + velocity * Time.deltaTime);

        if(transform.position.y < groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            isGrounded = true;
            velocity.y = 0f;
        }

        Vector2 direction = currentCursorPosition - new Vector2(transform.position.x, transform.position.y); //direction from Center to Cursor
        Vector2 normalizedDirection = direction.normalized;

        hand.position = new Vector2(transform.position.x, transform.position.y) + (normalizedDirection * handRadius);


        if(Input.GetButton("Fire1"))
        {
            if(isHovering)
            {
                isHolding = true;
            }

            //if (Input.GetButton("Fire1"))
            //{
            //    isHolding = true;
            //}
            //else
            //{
            //    isHolding = false;
            //    isHovering = false;
            //}
        }
        else
        {
            if (objectUnderHand != null)
            {
                objectUnderHand.layer = LayerMask.NameToLayer("Default");
               
            }
            isHolding = false;
        }

        if(isHolding)
        {
            if(objectUnderHand != null)
            {
                objectUnderHand.layer = LayerMask.NameToLayer("Ignore Player");
                objectUnderHand.GetComponent<Rigidbody2D>().MovePosition(hand.position);

                //objectUnderHand.transform.position = hand.position;
                //objectUnderHand = null;
            }

            //if (Input.GetButtonUp("Fire1"))
            //{
            //    isHolding = false;
            //    //objectUnderHand = null;
            //    //Debug.Log("isholding");
            //}
        }
    }

    private void OnJumpStart()
    {
        velocity.y = jumpForce;
        isGrounded = false;
    }

    private void OnJumpEnd()
    {
        if (velocity.y > 6.0f)
            velocity.y = 6.0f;
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentCursorPosition, 0.25f);

        //Gizmos.color = Color.magenta;
       // Gizmos.DrawLine(handPivot.position, handPivot.position + (handPivot.forward * 100f));
    }


    public void HandleBrickHandsCollision(GameObject brick)
    {
        if(isHolding)
        {
            return;
        }


        objectUnderHand = brick;
        isHovering = true;
    }

    public void BeginBrickHandsTrigger()
    {

    }    

    public void EndBrickHandsTrigger()
    {

    }
}
