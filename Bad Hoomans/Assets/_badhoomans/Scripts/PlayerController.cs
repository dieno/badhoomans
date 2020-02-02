using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float yStartOffset = 5f;

    //[SerializeField] private Transform handPivot;
    [SerializeField] private Transform hand;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravity = 0.1f;

    [SerializeField] private float handRadius = 1f;

    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private float maxJumpLength = 10f;

    [SerializeField] private SpriteRenderer handSprite = null;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded = false;
    private float groundY = 0f;
    private Vector2 currentCursorPosition = Vector2.zero;

    private bool isHovering = false;
    private bool isHolding = false;
    private GameObject objectUnderHand = null;

    private float currentJumpLength = 0f;

    private bool isJumpCanceled = false;
    private bool canBeginJump = false;


    private bool jumpButtonReset = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        groundY = transform.position.y;

        // start character above and let him fall in
        transform.position = new Vector3(transform.position.x, transform.position.x + yStartOffset, transform.position.z);
    }

    void Update() 
    {
        velocity.y -= gravity;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentCursorPosition = new Vector2(worldPoint.x, worldPoint.y);

        if (Input.GetButton("Jump"))
        {
            if(!isJumpCanceled && jumpButtonReset)
            {
                OnJumpStart();
            }

            if (!isGrounded)
            {
                currentJumpLength -= Time.deltaTime;
                //Debug.Log(currentJumpLength);
            }
        }

        if (Input.GetButtonUp("Jump") && !isGrounded)
        {
            if (!isJumpCanceled)
            {
                OnJumpEnd();
            }
        }


        velocity.x = Input.GetAxis("Horizontal") * speed;

        rb.MovePosition(transform.position + velocity * Time.deltaTime);

        if(transform.position.y < groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            isGrounded = true;
            velocity.y = 0f;
            currentJumpLength = 0f;
            isJumpCanceled = false;
            canBeginJump = true;
        }


        if (transform.position.y > maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            velocity.y = 0f;
        }

        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            jumpButtonReset = true;
        }


        Vector2 centerPosition = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(currentCursorPosition, centerPosition);

        if (distance > handRadius)
        {
            Vector2 fromOriginToObject = currentCursorPosition - centerPosition;
            fromOriginToObject *= handRadius / distance;
            hand.position = centerPosition + fromOriginToObject;
        }
        else
        {
            hand.position = currentCursorPosition;
        }

        if(Input.GetButton("Fire1"))
        {
            if(isHovering)
            {
                isHolding = true;
            }
        }
        else
        {
            if(isHolding)
            {
                if (objectUnderHand != null)
                {
                    //objectUnderHand.layer = LayerMask.NameToLayer("Default");
                    objectUnderHand.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    objectUnderHand = null;

                }
                isHolding = false;
                isHovering = false;
            }
        }

        if(isHolding)
        {
            if(objectUnderHand != null)
            {
                Vector2 currentPos = new Vector2(objectUnderHand.transform.position.x, objectUnderHand.transform.position.y);

                if(Vector2.Distance(currentPos, transform.position) > (handRadius + 1f))
                {
                    if (objectUnderHand != null)
                    {
                        //objectUnderHand.layer = LayerMask.NameToLayer("Default");
                        objectUnderHand.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        objectUnderHand = null;

                    }
                    isHolding = false;
                    isHovering = false;
                }
                else
                {
                    //objectUnderHand.layer = LayerMask.NameToLayer("Ignore Player");
                    objectUnderHand.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    objectUnderHand.GetComponent<Rigidbody2D>().MovePosition(hand.position);
                    objectUnderHand.transform.rotation = Quaternion.identity;
                    objectUnderHand.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                }
            }
        }

        if(!isGrounded && currentJumpLength <= 0f)
        {
            isJumpCanceled = true;
            jumpButtonReset = false;
        }


        Vector3 YAxisLineStart = transform.position + (Vector3.up * 2f);
        Vector3 YAxisLineEnd = transform.position - (Vector3.up * 2f);

        float facingDirection = (YAxisLineEnd.x - YAxisLineStart.x) * (currentCursorPosition.y - YAxisLineStart.y) - (YAxisLineEnd.y - YAxisLineStart.y) * (currentCursorPosition.x - YAxisLineStart.x);

        if(facingDirection >= 0)
        {
            sr.flipX = true;
            handSprite.flipX = false;
        }
        else
        {
            sr.flipX = false;
            handSprite.flipX = true;
        }
        

        
    }

    private void OnJumpStart()
    {
        velocity.y = jumpForce;
        isGrounded = false;

        if(canBeginJump)
        {
            currentJumpLength = maxJumpLength;
            canBeginJump = false;
        }
       
    }

    private void OnJumpEnd()
    {
        velocity.y = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentCursorPosition, 0.25f);
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
}
