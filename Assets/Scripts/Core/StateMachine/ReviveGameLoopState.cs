using Core.Factory;
using Spawners;
using UnityEngine;

namespace Core.StateMachine
{
    public class ReviveGameLoopState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Game _game;
        private readonly AllServices _services;
        private readonly IGameFactory _gameFactory;

        public ReviveGameLoopState(GameStateMachine stateMachine, 
            Game game, 
            AllServices services)
        {
            _stateMachine = stateMachine;
            _game = game;
            _services = services;
            _gameFactory = _services.Single<IGameFactory>();
        }

        public void Enter()
        {
            _game.IsRevived = true;
            Game.GameOver += OnGameOver;
            
            var player = _gameFactory.Player;
            player.Health.AddHeart(); 
            GameObject.Destroy(_gameFactory.Player.Cracks.CrackedPlayer.gameObject);
            player.Health.gameObject.SetActive(true);

            var spawners = GameObject.FindObjectsOfType<PooledObjectSpawner>();
            foreach (var spawner in spawners)
            {
                spawner.StartSpawner();
            }
        }

        private void OnGameOver(GameOverCondition condition)
        {
            Game.GameOver -= OnGameOver;
            _stateMachine.Enter<GameOverState>();
        }

        public void Exit()
        {

        }
    }
}