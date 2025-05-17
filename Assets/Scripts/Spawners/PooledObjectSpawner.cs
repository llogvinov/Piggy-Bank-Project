using System.Collections;
using Core;
using Core.Factory;
using Main;
using UnityEngine;

namespace Spawners
{
    public class PooledObjectSpawner : MonoBehaviour
    {
        [SerializeField] protected ObjectPool _pool;
        [SerializeField] private float spawnBounds = 3f;
        [Header("Time")]
        [SerializeField] private float startTimeSpawn;
        [SerializeField] private float minTimeSpawn;
        [SerializeField] private float maxTimeSpawn;
        [SerializeField] private float scaleTime = 15f;
        [SerializeField] private float gravityScale = 1.1f;
        [Header("Spawn Rules")]
        [SerializeField] private float minSpawnDistance = 0.8f;

        private Player player;
        private Transform playerTransform;
        public static float lastSpawnX = Mathf.Infinity;

        public virtual void StartSpawner()
        {
            Game.GameOver += OnGameOver;

            player = AllServices.Container.Single<IGameFactory>().Player;
            playerTransform = player.Movement.transform;

            StartCoroutine(WaitToStartSpawning());
        }

        protected virtual void OnDestroy()
        {
            Game.GameOver -= OnGameOver;
        }

        private void OnGameOver(GameOverCondition condition)
        {
            Game.GameOver -= OnGameOver;
            StopAllCoroutines();
        }

        protected virtual IEnumerator WaitToStartSpawning()
        {
            yield return new WaitForSeconds(startTimeSpawn);
            StartCoroutine(SpawnObject());
            StartCoroutine(ChangeGravityScale());
        }

        private IEnumerator SpawnObject()
        {
            while (true)
            {
                var pooledObject = _pool.TryGetPooledObject();
                pooledObject.transform.position = GetValidSpawnPosition();
                yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            }
        }

        protected Vector2 GetValidSpawnPosition()
        {
            int attempts = 5;
            float x = 0f;

            for (int i = 0; i < attempts; i++)
            {
                x = GenerateSpawnX();
                if (Mathf.Abs(x - lastSpawnX) >= minSpawnDistance)
                    break;
            }

            float y = transform.position.y;
            return new Vector2(x, y);
        }

        protected float GenerateSpawnX() => 
            Random.Range(-spawnBounds, spawnBounds);

        protected Vector2 RandomPosition() =>
            new Vector2(Random.Range(-spawnBounds, spawnBounds), transform.position.y);

        private IEnumerator ChangeGravityScale()
        {
            while (true)
            {
                yield return new WaitForSeconds(scaleTime);
                minTimeSpawn *= 0.95f;
                maxTimeSpawn *= 0.95f;
                foreach (var pooledObject in _pool.AllInstances)
                {
                    pooledObject.Rigidbody.gravityScale *= gravityScale;
                }
            }
        }
    }
}