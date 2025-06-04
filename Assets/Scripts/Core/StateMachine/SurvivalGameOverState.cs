using System;
using System.Collections;
using Core.Factory;
using Core.Services.Ad;
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
        private readonly Game _game;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerDataService _playerDataService;

        private UISurvivalGameOver _uiSurvivalGameOver;
        private UIGameComplete _uiGameComplete;

        private GameOverCondition _gameOverCondition;

        public SurvivalGameOverState(GameStateMachine stateMachine,
            Game game,
            AllServices services,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _game = game;
            _services = services;
            _coroutineRunner = coroutineRunner;

            _gameFactory = _services.Single<IGameFactory>();
            _playerDataService = services.Single<IPlayerDataService>();
        }

        public void Enter(GameOverCondition gameOverCondition)
        {
            _gameOverCondition = gameOverCondition;
            Game.GameOver = null;

            var player = _gameFactory.Player;
            player.ToggleMovement(false);
            player.ToggleHealth(false);
            _coroutineRunner.StartCoroutine(Delayed());

            IEnumerator Delayed()
            {
                yield return new WaitForSeconds(0.5f);

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
        }

        private void OnDied()
        {
            _uiSurvivalGameOver = GameObject.FindObjectOfType<UISurvivalGameOver>();
            _uiSurvivalGameOver.ReviveButton.onClick.AddListener(ShowRewardedAd);
            _uiSurvivalGameOver.MenuButton.onClick.AddListener(LoadMenu);
            _uiSurvivalGameOver.RestartButton.onClick.AddListener(RestartGame);
            _uiSurvivalGameOver.Show(!_game.IsRevived);
        }

        private void OnCompleted()
        {
            _playerDataService.AddCoins(GameConstants.SURVIVAL_MODE_REWARD);

            _uiGameComplete = GameObject.FindObjectOfType<UIGameComplete>();
            _uiGameComplete.MenuButton.onClick.AddListener(LoadMenu);
            _uiGameComplete.RestartButton.onClick.AddListener(RestartGame);
            _uiGameComplete.ToggleRewardButton(!_game.IsRevived);
            _uiGameComplete.Show(GameConstants.SURVIVAL_MODE_REWARD);
        }

        public void Exit()
        {
            switch (_gameOverCondition)
            {
                case GameOverCondition.Died:
                    _uiSurvivalGameOver.Hide();
                    _uiSurvivalGameOver.ReviveButton.onClick.RemoveListener(ShowRewardedAd);
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

        private void ShowRewardedAd()
        {
            _uiSurvivalGameOver.ReviveButton.onClick.RemoveListener(ShowRewardedAd);
            _services.Single<IAdService>().ShowRewardedAd("Revive", RevivePlayer);
        }

        private void RevivePlayer()
        {
            _stateMachine.Enter<ReviveSurvivalGameLoopState>();
        }

        private void LoadMenu()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);
        }

        private void RestartGame()
        {
            GameObject.Destroy(_gameFactory.Player.gameObject);
            _services.Single<IAdService>().ShowInterstitialAd();
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.SurvivalGameScene);
        }
    }
}