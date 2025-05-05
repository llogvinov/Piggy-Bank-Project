using Main;
using UnityEngine;

namespace Core.Factory
{
    public class GameFactory : IGameFactory
    {
        public Player Player { get; private set; }

        public Player InstantiatePlayer()
        {
            var loaded = Resources.Load<Player>("Prefabs/Player");
            if (loaded == null) 
                Debug.LogError($"{typeof(Player)} not found in resources");
            
            Player = GameObject.Instantiate(loaded);
            Debug.Log("player " + Player.gameObject.name);
            return Player;
        }
    }
}