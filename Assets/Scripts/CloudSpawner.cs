using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] public List<Cloud> clouds = new List<Cloud>();
    [SerializeField] private bool isNormalMode;

    [SerializeField] private float minTimeBetweenSpawn;
    [SerializeField] private float maxTimeBetweenSpawn;

    [SerializeField] private float bottomSpawnBound = 2;
    [SerializeField] private float topSpawnBound = 5;

    [SerializeField] private float leftSpawnBound = -2;
    [SerializeField] private float rightSpawnBound = 2;

    public float speed; //using it in a script Cloud.cs

    private IEnumerator Start()
    {
        if (isNormalMode)
        {
            StartCloudSpawn();
        }

        while (true)
        {
            //Instantiate new cloud as child of CloudSpawner
            Instantiate(clouds[RandomIndex()], RandomPosition(), Quaternion.identity, transform);

            yield return new WaitForSeconds(RandomTime());
        }
    }

    private Vector2 RandomPosition()
    {
        return new Vector2(transform.position.x, Random.Range(bottomSpawnBound, topSpawnBound));
    }

    private float RandomTime()
    {
        return Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    private int RandomIndex()
    {
        return Random.Range(0, clouds.Count);
    }

    //Set clouds on screen at start
    private void StartCloudSpawn()
    {
        float yCentre = (topSpawnBound + bottomSpawnBound) / 2;
        float xCentre = (leftSpawnBound + rightSpawnBound) / 2;

        Instantiate(clouds[0], StartCloudRandomPosition(topSpawnBound, yCentre, leftSpawnBound, xCentre), Quaternion.identity, transform);
        Instantiate(clouds[1], StartCloudRandomPosition(topSpawnBound, yCentre, xCentre, rightSpawnBound), Quaternion.identity, transform);
        Instantiate(clouds[2], StartCloudRandomPosition(yCentre, bottomSpawnBound, leftSpawnBound, xCentre), Quaternion.identity, transform);
        Instantiate(clouds[3], StartCloudRandomPosition(yCentre, bottomSpawnBound, xCentre, rightSpawnBound), Quaternion.identity, transform);
    }

    private Vector2 StartCloudRandomPosition(float topBound, float bottomBound, float rightBound, float leftBound)
    {
        return new Vector2(Random.Range(leftBound, rightBound), Random.Range(bottomBound, topBound));
    }
}
