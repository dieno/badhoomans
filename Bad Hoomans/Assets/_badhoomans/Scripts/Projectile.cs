using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameManager gameManager = null;
    public float timeToLive = 5f;

    private bool canCauseDamage = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("House"))
       {
            if(canCauseDamage)
            {
                canCauseDamage = false;
                gameManager.takeHit();
            }
       }
    }


    private void Start()
    {
        StartCoroutine(DestroyAtTime(timeToLive));
    }


    private IEnumerator DestroyAtTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
