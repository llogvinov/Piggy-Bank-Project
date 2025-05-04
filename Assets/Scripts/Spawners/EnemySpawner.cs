using System.Collections;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : PooledObjectSpawner
    {
        [Space]
        [SerializeField] private ObjectPool _explosionPool;
        [SerializeField] private ObjectPool _explosionMarkPool;

        protected override IEnumerator WaitToStartSpawning()
        {
            foreach (var pooledObject in _pool.AllInstances)
            {
                var enemy = (Enemy)pooledObject;
                enemy.Exploded += SpawnExplosion;
                enemy.ExplodedOnGround += SpawnExplosionMark;
            }

            return base.WaitToStartSpawning();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (var pooledObject in _pool.AllInstances)
            {
                var enemy = (Enemy)pooledObject;
                enemy.Exploded -= SpawnExplosion;
                enemy.ExplodedOnGround -= SpawnExplosionMark;
            }
        }

        public void SpawnExplosion(Vector3 position, float scale)
        {
            var explosion = _explosionPool.TryGetPooledObject();
            explosion.transform.position = position;
            explosion.transform.rotation = Quaternion.identity;
            explosion.transform.localScale *= scale;
        }

        public void SpawnExplosionMark(Vector3 position, float scale)
        {
            var explosionMark = _explosionMarkPool.TryGetPooledObject();
            explosionMark.transform.position = position;
            explosionMark.transform.rotation = Quaternion.identity;
            explosionMark.transform.localScale *= scale;
        }
    }
}