using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] AnimationCurve animationCurve;

    [SerializeField] private float spawnBounds = 3.5f;

    [Header("Time")]
    [SerializeField] private float startTimeSpawn;
    [SerializeField] private float minTimeSpawn;
    [SerializeField] private float maxTimeSpawn;
    [SerializeField] private float scaleTime = 15f;
    [Space]
    [SerializeField] private GameObject[] objectPrefabs;

    [SerializeField] private float gravityScale = 1.2f;

    private GameManager gameManager;

    private int objectIndex;
    private float randomFloat;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            Rigidbody2D objectRb = objectPrefabs[i].GetComponent<Rigidbody2D>();
            objectRb.gravityScale = 0.7f;
        }

        StartCoroutine(WaitToStartSpawning());
    }

    private IEnumerator WaitToStartSpawning()
    {
        yield return new WaitForSeconds(startTimeSpawn);
        StartCoroutine(SpawnObject());
        StartCoroutine(ChangeGravityScale());
    }

    private IEnumerator SpawnObject()
    {
        while (!gameManager.IsGameOver)
        {
            objectIndex = objectPrefabs.Length > 1 ? RandomPrefab() : 0;
            
            Instantiate(objectPrefabs[objectIndex], RandomPosition(), objectPrefabs[objectIndex].transform.rotation, transform);

            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        }
    }

    private int RandomPrefab()
    {
        randomFloat = animationCurve.Evaluate(Random.value);
        
        if (randomFloat > 0.3)
            return 0;
        else
            return RandomPrefabPlus(objectPrefabs.Length); 
    }

    private int RandomPrefabPlus(int numberOfPrefabs)
    {
        return Random.Range(1, numberOfPrefabs);
    }

    protected Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-spawnBounds, spawnBounds), transform.position.y);
    }

    //Increase gravity scale for objects
    private IEnumerator ChangeGravityScale() 
    {
        while (true)
        {
            yield return new WaitForSeconds(scaleTime);

            for (int i = 0; i < objectPrefabs.Length; i++)
            {
                Rigidbody2D objectRb = objectPrefabs[i].GetComponent<Rigidbody2D>();
                objectRb.gravityScale *= gravityScale;
            }
        }
    }
}
