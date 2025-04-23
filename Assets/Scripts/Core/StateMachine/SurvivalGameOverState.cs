using System;
using Core.Factory;
using Core.Services.PlayerData;
using Data;
using PiggyBank;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class SurvivalGameOverState : IPayloadState<GameOverCondition>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerDataService _playerDataService;

        private UISurvivalGameOver _uiSurvivalGameOver;
        private UIGameComplete _uiGameComplete;

        private GameOverCondition _gameOverCondition;

        public SurvivalGameOverState(GameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;

            _gameFactory = _services.Single<IGameFactory>();
            _playerDataService = services.Single<IPlayerDataService>();
        }

        public void Enter(GameOverCondition gameOverCondition)
        {
            _gameOverCondition = gameOverCondition;
            Game.GameOver = null;

            switch (_gameOverCondition)
            {
                case GameOverCondition.Died:
                    OnDied();
                    break;
                case GameOverCondition.Completed:
                    OnCompleted();
                    break;
                default:
                    throw new Exception($"Invalid game over condition {_gameOverCondition}");
            }
        }

        private void OnDied()
        {
            _uiSurvivalGameOver = GameObject.FindObjectOfType<UISurvivalGameOver>();
            _uiSurvivalGameOver.Show();
            _uiSurvivalGameOver.MenuButton.onClick.AddListener(LoadMenu);
            _uiSurvivalGameOver.RestartButton.onClick.AddListener(RestartGame);
        }

        private void OnCompleted()
        {
            _playerDataService.AddCoins(GameConstants.SURVIVAL_MODE_REWARD);

            _uiGameComplete = GameObject.FindObjectOfType<UIGameComplete>();
            _uiGameComplete.Show(GameConstants.SURVIVAL_MODE_REWARD,
                _playerDataService.GetCoins());
            _uiGameComplete.MenuButton.onClick.AddListener(LoadMenu);
            _uiGameComplete.RestartButton.onClick.AddListener(RestartGame);
        }

        public void Exit()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);

            switch (_gameOverCondition)
            {
                case GameOverCondition.Died:
                    _uiSurvivalGameOver.Hide();
                    _uiSurvivalGameOver.MenuButton.onClick.RemoveListener(LoadMenu);
                    _uiSurvivalGameOver.RestartButton.onClick.RemoveListener(RestartGame);
                    break;
                case GameOverCondition.Completed:
                    _uiGameComplete.Hide();
                    _uiGameComplete.MenuButton.onClick.RemoveListener(LoadMenu);
                    _uiGameComplete.RestartButton.onClick.RemoveListener(RestartGame);
                    break;
                default:
                    throw new Exception($"Invalid game over condition {_gameOverCondition}");
            }
        }

        private void LoadMenu()
            => _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);

        private void RestartGame()
            => _stateMachine.Enter<LoadSceneState, string>(AssetPath.SurvivalGameScene);
    }
}