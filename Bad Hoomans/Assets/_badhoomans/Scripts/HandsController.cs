using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger");
        if (collision.CompareTag("Brick"))
        {
            Debug.Log("bruuuhk trigger");

            player.HandleBrickHandsCollision(collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //player.EndBrickHandsTrigger();
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log("trigger");
    //    if (collision.CompareTag("Brick"))
    //    {
    //        Debug.Log("bruuuhk - trigger");

    //        player.HandleBrickHandsCollision(collision.gameObject);
    //    }

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("col");

    //    if (collision.collider.CompareTag("Brick"))
    //    {
    //        Debug.Log("bruuuhk - collision");
    //    }
    //}
}
