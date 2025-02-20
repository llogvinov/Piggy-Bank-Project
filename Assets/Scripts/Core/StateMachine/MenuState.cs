using UI;

namespace Core.StateMachine
{
    public class MenuState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UILoading _uiLoading;

        public MenuState(GameStateMachine stateMachine, UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _uiLoading = uiLoading;
        }

        public void Enter()
        {
            _uiLoading.Hide();

            UISelectMode.NormalModeSelected += OnNormalModeSelected;
            UISelectMode.SurvivalModeSelected += OnSurvivalModeSelected;
        }

        public void Exit()
        {
            UISelectMode.NormalModeSelected -= OnNormalModeSelected;
            UISelectMode.SurvivalModeSelected -= OnSurvivalModeSelected;
        }

        private void OnNormalModeSelected()
        {
            
        }

        private void OnSurvivalModeSelected()
        {
            
        }

        private void LoadGame()
        {
            _stateMachine.Enter<LoadSceneState, string>("Game");
        }
    }
}