using DataStructures.Event;
using Features.Gui.Scripts;
using Features.LandingPod.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    public class LevelResultScreenState : AbstractGameState
    {
        private readonly GuiManager guiManager;
        private readonly LaunchInformation launchInformation;

        public LevelResultScreenState(
            ActionEvent onBeforeLevelResultScreen,
            ActionEvent onAfterLevelResultScreen,
            GuiManager guiManager,
            LaunchInformation launchInformation)
            : base(GameState.LEVEL_RESULT_SCREEN, onBeforeLevelResultScreen, onAfterLevelResultScreen)
        {
            this.guiManager = guiManager;
            this.launchInformation = launchInformation;
        }

        protected override void Enter()
        {
            base.Enter();

            guiManager.ShowLevelResultScreen(launchInformation);
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.LEVEL_INIT:
                case GameState.GAME_EXITING:
                    return true;
                default:
                    return false;
            }
        }
    }
}
