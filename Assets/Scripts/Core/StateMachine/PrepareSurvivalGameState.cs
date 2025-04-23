using System;
using Core.Factory;
using Data;
using PiggyBank;
using Timer;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class PrepareSurvivalGameState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly UILoading _uiLoading;

        private GameTimer _timer;
        private UIPause _uiPause;
        private UIHealth _uiHealth;

        public PrepareSurvivalGameState(GameStateMachine stateMachine, 
            IGameFactory gameFactory,
            UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiLoading = uiLoading;
        }

        public void Enter()
        {
            _timer = GameObject.FindObjectOfType<GameTimer>();
            _timer.SetTimer(GameConstants.SURVIVAL_MODE_TIMER);
            _timer.TimerCompleted += OnTimerCompleted;

            _uiPause = GameObject.FindObjectOfType<UIPause>();
            _uiPause.MenuButton.onClick.AddListener(GoToMenu);

            var player = _gameFactory.InstantiatePlayer();
            _uiHealth = GameObject.FindObjectOfType<UIHealth>();
            _uiHealth.Initialize(player);
            player.SkinCreator.SetFullSkin();

            var spawners = GameObject.FindObjectsOfType<ObjectSpawner>();
            foreach (var spawner in spawners)
            {
                spawner.StartSpawner(); 
            }

            Game.GameOver += OnGameOver;
        }

        private void OnTimerCompleted()
        {
            _timer.TimerCompleted -= OnTimerCompleted;
            Game.GameOver?.Invoke(GameOverCondition.Completed);
        }

        private void GoToMenu()
        {
            _uiPause.MenuButton.onClick.RemoveListener(GoToMenu);
            _uiPause.ResumeGame();
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);
        }

        private void OnGameOver(GameOverCondition condition)
        {
            if (_timer.IsRunning)
                _timer.PauseTimer();

            _stateMachine.Enter<SurvivalGameOverState, GameOverCondition>(condition);
        }

        public void Exit()
        {
            _uiLoading.Hide();
        }
    }
}