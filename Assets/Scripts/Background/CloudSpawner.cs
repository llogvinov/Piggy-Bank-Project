using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Background
{
    public class CloudSpawner : MonoBehaviour
    {
        public List<Cloud> Clouds = new List<Cloud>();
        [SerializeField] private bool isNotMenu;

        [Header("Time between spawning")]
        [SerializeField] private float minTimeBetweenSpawn;
        [SerializeField] private float maxTimeBetweenSpawn;

        [Header("Spawning bounds")]
        [SerializeField] private float bottomSpawnBound = 2;
        [SerializeField] private float topSpawnBound = 5;
        [SerializeField] private float leftSpawnBound = -2;
        [SerializeField] private float rightSpawnBound = 2;
        [Space]
        public float Speed;

        private IEnumerator Start()
        {
            if (isNotMenu)
                StartCloudSpawn();

            while (true)
            {
                Instantiate(Clouds[RandomIndex()], RandomPosition(), Quaternion.identity, transform);
                yield return new WaitForSeconds(RandomTime());
            }
        }

        private int RandomIndex() => 
            Random.Range(0, Clouds.Count);

        private Vector2 RandomPosition() => 
            new Vector2(
                transform.position.x,
                Random.Range(bottomSpawnBound,
                topSpawnBound));

        private float RandomTime() => 
            Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);

        private void StartCloudSpawn()
        {
            float yCentre = (topSpawnBound + bottomSpawnBound) / 2;
            float xCentre = (leftSpawnBound + rightSpawnBound) / 2;

            Instantiate(Clouds[0], StartCloudRandomPosition(topSpawnBound, yCentre, leftSpawnBound, xCentre), Quaternion.identity, transform);
            Instantiate(Clouds[1], StartCloudRandomPosition(topSpawnBound, yCentre, xCentre, rightSpawnBound), Quaternion.identity, transform);
            Instantiate(Clouds[2], StartCloudRandomPosition(yCentre, bottomSpawnBound, leftSpawnBound, xCentre), Quaternion.identity, transform);
            Instantiate(Clouds[3], StartCloudRandomPosition(yCentre, bottomSpawnBound, xCentre, rightSpawnBound), Quaternion.identity, transform);
        }

        private Vector2 StartCloudRandomPosition(float topBound, float bottomBound, float leftBound, float rightBound) => 
            new Vector2(
                Random.Range(leftBound, rightBound), 
                Random.Range(bottomBound, topBound));
    }
}