using System.Collections;
using Core;
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

        public virtual void StartSpawner()
        {
            Game.GameOver += OnGameOver;

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
                pooledObject.transform.position = RandomPosition();
                yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            }
        }

        protected Vector2 RandomPosition() =>
            new Vector2(Random.Range(-spawnBounds, spawnBounds), transform.position.y);

        private IEnumerator ChangeGravityScale()
        {
            while (true)
            {
                yield return new WaitForSeconds(scaleTime);
                foreach (var pooledObject in _pool.AllInstances)
                {
                    pooledObject.Rigidbody.gravityScale *= gravityScale;
                }
            }
        }
    }
}