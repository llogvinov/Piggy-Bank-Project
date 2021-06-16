using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] AnimationCurve animationCurve;

    [SerializeField] protected float spawnBounds = 3.5f;

    [Header("Time")]
    [SerializeField] protected float startTimeSpawn;
    [SerializeField] protected float minTimeSpawn;
    [SerializeField] protected float maxTimeSpawn;
    [SerializeField] private float scaleTime = 15f;

    [SerializeField] private GameObject[] objectPrefabs;

    [SerializeField] private float gravityScale = 1.2f;

    private GameManager gameManager;

    private int objectIndex;
    private float randomFloat;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
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
        while (!gameManager.isGameOver)
        {
            if (objectPrefabs.Length > 1)
            {
                objectIndex = RandomPrefab();
            }
            else
            {
                objectIndex = 0;
            }
            
            Instantiate(objectPrefabs[objectIndex], RandomPosition(), objectPrefabs[objectIndex].transform.rotation, transform);

            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        }
    }

    private int RandomPrefab()
    {
        randomFloat = animationCurve.Evaluate(Random.value);
        if (randomFloat > 0.3) 
        { 
            return 0; 
        }
        else 
        { 
            return RandomPrefabPlus(objectPrefabs.Length); 
        }
    }

    private int RandomPrefabPlus(int numOfPrefabs)
    {
        return Random.Range(1, numOfPrefabs);
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
