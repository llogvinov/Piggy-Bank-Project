using System;
using Core.StateMachine;
using UI;

namespace Core
{
    public enum GameOverCondition
    {
        Default = 0,
        Died,
        Completed,
    }

    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public GameStateMachine StateMachine => _stateMachine;

        public static Action<GameOverCondition> GameOver;
        public bool IsRevived;

        public Game(ICoroutineRunner coroutineRunner, UILoading uiLoading, GameSettings settings)
        {
            _stateMachine = new GameStateMachine(this,
                coroutineRunner,
                uiLoading, 
                new SceneLoader(coroutineRunner), 
                AllServices.Container,
                settings);
        }
    }

    public class GameSettings
    {
        public AllLocalizationData LocalizationData;
        public LocationShopDatabase LocationShopDatabase;
        public HatShopDatabase HatShopDatabase;
        public MaskShopDatabase MaskShopDatabase;
    }
}