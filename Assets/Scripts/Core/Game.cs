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
        private readonly GameSettings _settings;

        public GameStateMachine StateMachine => _stateMachine;
        public GameSettings Settings => _settings;

        public static Action<GameOverCondition> GameOver;
        public bool IsRevived;

        public Game(ICoroutineRunner coroutineRunner, UILoading uiLoading, GameSettings settings)
        {
            _settings = settings;
            _stateMachine = new GameStateMachine(this,
                coroutineRunner,
                uiLoading, 
                new SceneLoader(coroutineRunner), 
                AllServices.Container,
                settings);
        }
    }
}