using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace Spawners
{
    public class ExplosionSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private AudioClip _explosionClip;

        private AudioSource _explosionAudio;

        private void Awake()
        {
            _explosionAudio = GetComponent<AudioSource>();
            _explosionAudio.volume = AllServices.Container.Single<IPlayerDataService>().GetSound() ? 1 : 0;
        }

        private void Start()
        {
            _pool.GetObject += PlayExplosionSound;
        }

        public void OnDestroy()
        {
            _pool.GetObject -= PlayExplosionSound;
        }

        private void PlayExplosionSound(PooledObject pooledObject) => 
            _explosionAudio.PlayOneShot(_explosionClip);
    }
}