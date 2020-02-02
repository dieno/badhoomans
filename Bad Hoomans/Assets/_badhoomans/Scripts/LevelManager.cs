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
        //GenerateTestPyramid(worldPositionAnchor.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void GenerateTestPyramid(Vector3 position)
    {
        List<GameObject> bricks = new List<GameObject>();

        SpriteRenderer sr = brickPrefab.GetComponent<SpriteRenderer>();

        float brickWidth = sr.bounds.size.x;

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
    }
}
