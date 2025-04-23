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

        public Game(ICoroutineRunner coroutineRunner, UILoading uiLoading)
        {
            _stateMachine = new GameStateMachine(this, 
                uiLoading, 
                new SceneLoader(coroutineRunner), 
                AllServices.Container);
        }

    }
}