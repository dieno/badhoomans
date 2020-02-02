using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameManager gameManager = null;
    [SerializeField] private float timeToLive = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("House"))
       {
            gameManager.takeHit();
       }
    }


    private void Start()
    {
        DestroyAtTime(timeToLive);
    }


    private IEnumerator DestroyAtTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
