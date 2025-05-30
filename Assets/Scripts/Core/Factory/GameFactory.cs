using Main;
using UnityEngine;

namespace Core.Factory
{
    public class GameFactory : IGameFactory
    {
        private MusicManager _musicManager;

        public Player Player { get; private set; }
        public MusicManager MusicManager => _musicManager ?? (_musicManager = CreateVOManager());

        public Player InstantiatePlayer()
        {
            var loaded = Resources.Load<Player>("Prefabs/Player");
            if (loaded == null)
                Debug.LogError($"{typeof(Player)} not found in resources");

            Player = GameObject.Instantiate(loaded);
            Debug.Log("player " + Player.gameObject.name);
            return Player;
        }

        public MusicManager CreateVOManager()
        {
            _musicManager = InstantiatePrefab<MusicManager>("Prefabs/Music Manager");
            return _musicManager;
        }
        
        private T InstantiatePrefab<T>(string path) where T : Component
        {
            var prefab = LoadFromResources<T>(path);
            if (prefab == null)
            {
                Debug.LogError($"Prefab of type {typeof(T)} not found at Resources path: {path}");
                return null;
            }
            return Object.Instantiate(prefab);
        }

        private T LoadFromResources<T>(string path) where T : Component =>
            Resources.Load<T>(path);
    }
}