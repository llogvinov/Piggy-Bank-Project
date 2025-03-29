using Core.Factory;
using Main.Background;
using Main;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class PrepareGameState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly UILoading _uiLoading;

        private UIHealth _uiHealth;
        private UIHealth UIHealth => _uiHealth ??= GameObject.FindObjectOfType<UIHealth>();

        public PrepareGameState(GameStateMachine stateMachine, IGameFactory gameFactory,
            UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiLoading = uiLoading;
        }

        public void Enter()
        {
            var backgroundCreator = GameObject.FindObjectOfType<BackgroundCreator>();
            if (backgroundCreator != null)
            {
                backgroundCreator.SetLocation();
            }

            var player = _gameFactory.InstantiatePlayer();
            UIHealth.Initialize(player);
            player.SkinCreator.SetFullSkin();

            Game.GameOver += OnGameOver;
        }

        public void Exit()
        {
            _uiLoading.Hide();
        }

        private void OnGameOver()
        {
            _stateMachine.Enter<GameOverState>();
        }
    }
}