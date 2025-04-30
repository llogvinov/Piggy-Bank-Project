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
        private readonly Game _game;
        private readonly AllServices _services;
        private readonly UILoading _uiLoading;
        private readonly IGameFactory _gameFactory;

        private GameTimer _timer;
        private UIPause _uiPause;
        private UIHealth _uiHealth;

        public PrepareSurvivalGameState(GameStateMachine stateMachine,
            Game game,
            AllServices services,
            UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _game = game;
            _services = services;
            _uiLoading = uiLoading;
            _gameFactory = _services.Single<IGameFactory>();
        }

        public void Enter()
        {
            _game.IsRevived = false;

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
            Game.GameOver -= OnGameOver;
            
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