using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Transform worldPositionAnchor;
    [SerializeField] private int pyramidHeight;
    [SerializeField] private float gapSize;
    [SerializeField] private Transform bricksParentObject;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTestPyramid(worldPositionAnchor.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateTestPyramid(Vector3 position)
    {
        List<GameObject> bricks = new List<GameObject>();

        SpriteRenderer sr = brickPrefab.GetComponent<SpriteRenderer>();

        //halfWidth = sr.bounds.size.x / 2;
        //halfHeight = sr.bounds.size.y / 2;

        float brickWidth = sr.bounds.size.x;


        //Vector2 currentPosition = worldPositionAnchor.position;
        Vector2 offset = new Vector2(brickWidth / 2f, brickWidth / 2f);

        int test = pyramidHeight;

        for (int i = 0; i < pyramidHeight; i++)
        {
            for (int j = 0; j < test; ++j)
            {

                Vector2 currentPosition = new Vector2(worldPositionAnchor.position.x + (j * (brickWidth + gapSize)), worldPositionAnchor.position.y + (i * (brickWidth + gapSize)));

                currentPosition.x += (brickWidth / 2f) * i;

                Instantiate(brickPrefab, currentPosition, Quaternion.identity, bricksParentObject);
            }
            test--;
        }

        //for (int i = pyramidHeight; i >= 0; i--)
        //{
        //    for (int j = 0; j <= i; ++j)
        //    {
                

        //        Vector2 currentPosition = new Vector2(worldPositionAnchor.position.x + (j * (brickWidth + gapSize)), worldPositionAnchor.position.y + (i * (brickWidth + gapSize)));

        //        currentPosition += offset;

        //        Debug.Log(i + ", " + j + " | " + currentPosition);

               

        //        if(i == pyramidHeight && j == 0)
        //        {
        //            Instantiate(brickPrefab, currentPosition, Quaternion.identity);
        //        }
        //    }
        //}
    }
}
