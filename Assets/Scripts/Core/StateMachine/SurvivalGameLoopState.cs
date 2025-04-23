namespace Core.StateMachine
{
    public class SurvivalGameLoopState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        
        public SurvivalGameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}