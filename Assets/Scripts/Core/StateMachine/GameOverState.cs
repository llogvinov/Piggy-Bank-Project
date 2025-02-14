using Core.Factory;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class GameOverState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;

        public GameOverState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            // _uiManager = uiManager;
            
            // _uiManager.UIGameOver.Show();
            // _uiManager.UIGameOver.MenuButton.onClick.AddListener(LoadMenu);
            // _uiManager.UIGameOver.RestartButton.onClick.AddListener(RestartGame);
        }

        public void Exit()
        {
            // GameObject.Destroy(_gameFactory.Frog);
            // GameObject.Destroy(_gameFactory.Girl);

            // foreach (var enemySpawner in _gameFactory.EnemySpawners) 
            //     GameObject.Destroy(enemySpawner);

            // _uiManager.UIGameOver.MenuButton.onClick.RemoveListener(LoadMenu);
            // _uiManager.UIGameOver.RestartButton.onClick.RemoveListener(RestartGame);
            // _uiManager.UIGameOver.Hide();
        }

        // private void LoadMenu() 
        //     => _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);

        // private void RestartGame() 
        //     => _stateMachine.Enter<LoadSceneState, string>(AssetPath.GameScene);
    }
}