using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectilesObject;
    [SerializeField] private Transform topLeftAnchor;
    [SerializeField] private Transform topRightAnchor;
    [SerializeField] private Transform houseAnchor;
    [SerializeField] private float force = 5f;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ProjectileRoutine(50));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LaunchProjectile()
    {
        float randomXPos = Random.Range(topLeftAnchor.position.x, topRightAnchor.position.x);
        float y = topLeftAnchor.position.y;

        Vector2 startPos = new Vector2(randomXPos, y);
        Vector2 direction = (new Vector2(houseAnchor.position.x, houseAnchor.position.y) - startPos).normalized;

        GameObject projectile = Instantiate(projectilePrefab, startPos, Quaternion.identity, projectilesObject) as GameObject;

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);


        Debug.DrawLine(startPos, startPos + (direction * 5f), Color.magenta, 1000f);
    }

    private IEnumerator ProjectileRoutine(int n)
    {
        for(int i = 0; i < n; i++)
        {
            LaunchProjectile();
            yield return new WaitForSeconds(1f);
        }
        
    }
}
