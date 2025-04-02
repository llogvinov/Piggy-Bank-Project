using Core.Factory;
using PiggyBank;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class GameOverState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;

        private UIGameOver _uiGameOver;

        public GameOverState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        { 
            Game.GameOver = null;

            _uiGameOver = GameObject.FindObjectOfType<UIGameOver>();
            _uiGameOver.Show();
            _uiGameOver.MenuButton.onClick.AddListener(LoadMenu);
            _uiGameOver.RestartButton.onClick.AddListener(RestartGame);
        }

        public void Exit()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);

            _uiGameOver.Hide();
            _uiGameOver.MenuButton.onClick.RemoveListener(LoadMenu);
            _uiGameOver.RestartButton.onClick.RemoveListener(RestartGame);
        }

        private void LoadMenu() 
            => _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);

        private void RestartGame() 
            => _stateMachine.Enter<LoadSceneState, string>(AssetPath.GameScene);
    }
}