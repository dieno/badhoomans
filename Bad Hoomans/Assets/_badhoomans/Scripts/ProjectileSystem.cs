using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileSystem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] public AudioManager audioManager = null;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectilesObject;
    [SerializeField] private Transform topLeftAnchor;
    [SerializeField] private Transform topRightAnchor;
    [SerializeField] private Transform houseAnchor;
    [SerializeField] private float force = 5f;
    [SerializeField] private int delay = 10;
    [SerializeField] private float timeToLive = 5f;

    [SerializeField] private RectTransform indicator;
    [SerializeField] private Text Countdown;

    private bool isRunning = false;

    private int currentTimer = 0;

    private Vector2 indicatorForward = Vector2.down;

    private Vector2 direction = Vector2.zero;
    private Vector2 startPos = Vector2.zero;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Countdown.text = currentTimer.ToString();

        //Debug.Log(currentTimer);
    }

    public void Begin()
    {
        //currentTimer = delay;
        isRunning = true;
        StartCoroutine(ProjectileRoutine());
    }

    public void End()
    {
        isRunning = false;
    }

    private void DetermineDirection()
    {
        float randomXPos = Random.Range(topLeftAnchor.position.x, topRightAnchor.position.x);
        float y = topLeftAnchor.position.y;

        startPos = new Vector2(randomXPos, y);
        direction = (new Vector2(houseAnchor.position.x, houseAnchor.position.y) - startPos).normalized;


        Quaternion rot = Quaternion.FromToRotation(indicatorForward, direction);
        indicator.rotation = rot * indicator.rotation;
        indicatorForward = rot * indicatorForward;

        Debug.DrawLine(startPos, startPos + (direction * 10f), Color.magenta, 1000f);
    }

    private void LaunchProjectile()
    {
        ////float randomXPos = Random.Range(topLeftAnchor.position.x, topRightAnchor.position.x);
        ////float y = topLeftAnchor.position.y;

        ////Vector2 startPos = new Vector2(randomXPos, y);
        ////Vector2 direction = (new Vector2(houseAnchor.position.x, houseAnchor.position.y) - startPos).normalized;

        GameObject projectile = Instantiate(projectilePrefab, startPos, Quaternion.identity, projectilesObject) as GameObject;

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
        projectile.GetComponent<Projectile>().gameManager = gameManager;
        projectile.GetComponent<Projectile>().timeToLive = timeToLive;
        projectile.GetComponent<Projectile>().audioManager = audioManager;
    }

    public IEnumerator BeginCountdown()
    {
        for(int i = 0; i < delay; i++)
        {
            currentTimer--;
            yield return new WaitForSeconds(1f);
        }
    }


    private IEnumerator ProjectileRoutine()
    {
        while(isRunning)
        {
            currentTimer = delay;
            DetermineDirection();
            yield return StartCoroutine(BeginCountdown());
            LaunchProjectile();
        }

    }
}
