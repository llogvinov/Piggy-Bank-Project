using Core.Factory;
using Spawners;
using Timer;
using UnityEngine;

namespace Core.StateMachine
{
    public class ReviveSurvivalGameLoopState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Game _game;
        private readonly AllServices _services;
        private readonly IGameFactory _gameFactory;

        private GameTimer _timer;

        public ReviveSurvivalGameLoopState(GameStateMachine stateMachine,
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
            player.ToggleMovement(true);
            player.ToggleHealth(true);
            player.Health.AddHeart();
            GameObject.Destroy(_gameFactory.Player.Cracks.CrackedPlayer.gameObject);
            player.Health.gameObject.SetActive(true);

            _timer = GameObject.FindObjectOfType<GameTimer>();
            _timer.UnPauseTimer();

            var spawners = GameObject.FindObjectsOfType<PooledObjectSpawner>();
            foreach (var spawner in spawners)
            {
                spawner.StartSpawner();
            }
        }

        private void OnGameOver(GameOverCondition condition)
        {
            Game.GameOver -= OnGameOver;

            if (_timer.IsRunning)
                _timer.PauseTimer();

            _stateMachine.Enter<SurvivalGameOverState, GameOverCondition>(condition);
        }

        public void Exit()
        {

        }
    }
}