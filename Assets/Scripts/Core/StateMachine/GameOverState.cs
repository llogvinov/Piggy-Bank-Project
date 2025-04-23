using Core.Factory;
using Core.Services.PlayerData;
using PiggyBank;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class GameOverState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerDataService _playerDataService;

        private UIGameOver _uiGameOver;

        public GameOverState(GameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;

            _gameFactory = _services.Single<IGameFactory>();
            _playerDataService = services.Single<IPlayerDataService>();
        }

        public void Enter()
        {
            Game.GameOver = null;

            _playerDataService.SetNewRecord(_gameFactory.Player.CoinCollector.CoinsToAdd);
            _playerDataService.AddCoins(_gameFactory.Player.CoinCollector.CoinsToAdd);

            _uiGameOver = GameObject.FindObjectOfType<UIGameOver>();
            _uiGameOver.Show(_playerDataService.GetPlayerRecord(), 
                _gameFactory.Player.CoinCollector.CoinsToAdd, 
                _playerDataService.GetCoins());
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