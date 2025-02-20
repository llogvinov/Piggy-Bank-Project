using Core.Factory;
using Main.Background;
using Main.Player;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class PrepareGameState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly UILoading _uiLoading;

        public PrepareGameState(GameStateMachine stateMachine, IGameFactory gameFactory,
            UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiLoading = uiLoading;
        }

        public void Enter()
        {
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