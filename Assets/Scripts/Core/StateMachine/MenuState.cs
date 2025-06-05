using Core.Services.PlayerData;
using PiggyBank;
using UI;
using YG;

namespace Core.StateMachine
{
    public class MenuState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly UILoading _uiLoading;

        public MenuState(GameStateMachine stateMachine, AllServices services, UILoading uiLoading)
        {
            _stateMachine = stateMachine;
            _services = services;
            _uiLoading = uiLoading;
        }

        public void Enter()
        {
            _uiLoading.Hide();

            UISelectMode.NormalModeSelected += OnNormalModeSelected;
            UISelectMode.SurvivalModeSelected += OnSurvivalModeSelected;

            ReviewGame();
        }

        public void Exit()
        {
            UISelectMode.NormalModeSelected -= OnNormalModeSelected;
            UISelectMode.SurvivalModeSelected -= OnSurvivalModeSelected;
        }

        private void OnNormalModeSelected()
        {
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.GameScene);
        }

        private void OnSurvivalModeSelected()
        {
            _stateMachine.Enter<LoadSceneState, string>(AssetPath.SurvivalGameScene);
        }

        private void ReviewGame()
        {
            var playerDataService = _services.Single<IPlayerDataService>();

            if (YG2.reviewCanShow && !playerDataService.PlayerData.ReviewShown && EnoughGamesPlayed(playerDataService))
            {
                playerDataService.PlayerData.ReviewShown = true;
                playerDataService.Save(playerDataService.PlayerData);
                YG2.ReviewShow();
            }
        }

        private bool EnoughGamesPlayed(IPlayerDataService playerDataService)
        {
            var normalPlayed = playerDataService.GetNormalGamesPlayed();
            var survivalPlayed = playerDataService.GetSurvivalGamesPlayed();

            return normalPlayed > 0 &&
                survivalPlayed > 0 &&
                normalPlayed + survivalPlayed > 2;
        }
    }
}