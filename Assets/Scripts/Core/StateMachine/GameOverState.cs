using System;
using Core.Factory;
using Core.Services.Ad;
using Core.Services.PlayerData;
using PiggyBank;
using Spawners;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class GameOverState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Game _game;
        private readonly AllServices _services;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerDataService _playerDataService;

        private UIGameOver _uiGameOver;

        public GameOverState(GameStateMachine stateMachine, 
            Game game, 
            AllServices services)
        {
            _stateMachine = stateMachine;
            _game = game;
            _services = services;

            _gameFactory = _services.Single<IGameFactory>();
            _playerDataService = services.Single<IPlayerDataService>();
        }

        public void Enter()
        {
            Game.GameOver = null;

            _playerDataService.SetBestScore(_gameFactory.Player.CoinCollector.CoinsToAdd);
            _playerDataService.AddCoins(_gameFactory.Player.CoinCollector.CoinsToAdd);

            var uiPowerup = GameObject.FindObjectOfType<PowerupSpawner>();
            uiPowerup.DeactivateAllPowerups();

            _uiGameOver = GameObject.FindObjectOfType<UIGameOver>();
            _uiGameOver.ReviveButton.onClick.AddListener(ShowRewardedAd);
            _uiGameOver.MenuButton.onClick.AddListener(LoadMenu);
            _uiGameOver.RestartButton.onClick.AddListener(RestartGame);
            _uiGameOver.ToggleRewardButton(!_game.IsRevived);
            _uiGameOver.Show(!_game.IsRevived, 
                _playerDataService.GetBestScore(), 
                _gameFactory.Player.CoinCollector.CoinsToAdd);
        }

        public void Exit()
        {
            _uiGameOver.Hide();
            _uiGameOver.ReviveButton.onClick.RemoveListener(ShowRewardedAd);
            _uiGameOver.MenuButton.onClick.RemoveListener(LoadMenu);
            _uiGameOver.RestartButton.onClick.RemoveListener(RestartGame);
        }

        private void ShowRewardedAd()
        {
            _uiGameOver.ReviveButton.onClick.RemoveListener(ShowRewardedAd);
            _services.Single<IAdService>().ShowRewardedAd("Revive", RevivePlayer);
        }

        private void RevivePlayer()
        {
            _stateMachine.Enter<ReviveGameLoopState>();
        }

        private void LoadMenu()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);
        }

        private void RestartGame()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.GameScene);
        }
    }
}