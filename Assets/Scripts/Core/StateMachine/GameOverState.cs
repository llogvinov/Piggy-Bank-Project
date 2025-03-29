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
        private UIGameOver UIGameOver => _uiGameOver ??= GameObject.FindObjectOfType<UIGameOver>();

        public GameOverState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {          
            UIGameOver.Show();
            UIGameOver.MenuButton.onClick.AddListener(LoadMenu);
            UIGameOver.RestartButton.onClick.AddListener(RestartGame);
        }

        public void Exit()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);

            UIGameOver.Hide();
            UIGameOver.MenuButton.onClick.RemoveListener(LoadMenu);
            UIGameOver.RestartButton.onClick.RemoveListener(RestartGame);
        }

        private void LoadMenu() 
            => _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);

        private void RestartGame() 
            => _stateMachine.Enter<LoadSceneState, string>(AssetPath.GameScene);
    }
}