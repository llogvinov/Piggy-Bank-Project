using Core.Factory;
using Core.Services.PlayerData;
using Main.Background;
using PiggyBank;
using Spawners;
using UI;
using UnityEngine;

namespace Core.StateMachine
{
    public class PrepareGameState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Game _game;
        private readonly AllServices _services;
        private readonly UILoading _uiLoading;
        private readonly IGameFactory _gameFactory;

        private UIAddCoins _uiAddCoins;
        private UIPause _uiPause;
        private UIHealth _uiHealth;

        public PrepareGameState(GameStateMachine stateMachine,
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
            var playerDataService = _services.Single<IPlayerDataService>();
            playerDataService.IncrementNormalGamesPlayed();

            _game.IsRevived = false;
            
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

            _uiAddCoins = GameObject.FindObjectOfType<UIAddCoins>();
            _uiAddCoins.Initialize(_gameFactory.Player);

            var spawners = GameObject.FindObjectsOfType<PooledObjectSpawner>();
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
            AddCollectedCoins();
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.MenuScene);
        }

        private void AddCollectedCoins()
        {
            var playerDataService = _services.Single<IPlayerDataService>();
            var coinCollector = _gameFactory.Player.CoinCollector;
            playerDataService.AddCoins(coinCollector.CoinsToAdd);
        }

        private void OnGameOver(GameOverCondition condition)
        {
            Game.GameOver -= OnGameOver;
            _stateMachine.Enter<GameOverState>();
        }

        public void Exit()
        {
            _uiLoading.Hide();
        }
    }
}