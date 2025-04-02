using System;
using Core.Factory;
using Main.Background;
using PiggyBank;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class PrepareGameState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly UILoading _uiLoading;

        private UIPause _uiPause;
        private UIHealth _uiHealth;

        public PrepareGameState(GameStateMachine stateMachine, IGameFactory gameFactory,
            UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _uiLoading = uiLoading;
        }

        public void Enter()
        {
            _uiPause = GameObject.FindObjectOfType<UIPause>();
            _uiPause.MenuButton.onClick.AddListener(GoToMenu);

            var backgroundCreator = GameObject.FindObjectOfType<BackgroundCreator>();
            if (backgroundCreator != null)
            {
                backgroundCreator.SetLocation();
            }

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

        private void GoToMenu()
        {
            _uiPause.MenuButton.onClick.RemoveListener(GoToMenu);
            _uiPause.ResumeGame();
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);
        }

        private void OnGameOver()
        {
            _stateMachine.Enter<GameOverState>();
        }

        public void Exit()
        {
            _uiLoading.Hide();
        }
    }
}