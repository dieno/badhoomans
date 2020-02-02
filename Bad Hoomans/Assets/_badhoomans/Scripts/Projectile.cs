using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameManager gameManager = null;
    public float timeToLive = 5f;

    private bool canCauseDamage = true;

    public AudioSource audioSource;

    [SerializeField] AudioClip impactClip = null;
    [SerializeField] AudioClip deathClip = null;

    private bool canPlaySound = true;

    private bool playingDeathSound = false;

    public AudioManager audioManager = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("House"))
       {
            if(canCauseDamage)
            {
                canCauseDamage = false;

                if(gameManager.NextHitKills())
                {
                    audioManager.PlayOneShot(deathClip);

                    playingDeathSound = true;
                }

                gameManager.takeHit();
            }
       }

       if(canPlaySound && !playingDeathSound)
       {
            audioManager.PlayOneShot(impactClip);
            canPlaySound = false;
       }
    }

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        //audioSource.clip = impactClip;


        StartCoroutine(DestroyAtTime(timeToLive));
    }

    private IEnumerator DestroyAtTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
