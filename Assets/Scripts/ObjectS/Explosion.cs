using Core;
using Core.Services.PlayerData;
using Spawners;
using UnityEngine;

public class Explosion : PooledObject
{
    private AudioSource _explosionAudio;

    private void Awake()
    {
        _explosionAudio = GetComponent<AudioSource>();
        _explosionAudio.volume = AllServices.Container.Single<IPlayerDataService>().GetSound() ? 1 : 0;
    }

    public void DestroyExplosion() => 
        Release();
}
