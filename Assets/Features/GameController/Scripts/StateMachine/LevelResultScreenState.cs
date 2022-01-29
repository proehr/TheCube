using DataStructures.Event;
using Features.Gui.Scripts;
using Features.LandingPod.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelResultScreenState : AbstractGameState
    {
        private readonly GuiController guiController;
        private readonly LaunchInformation launchInformation;

        public LevelResultScreenState(
            ActionEvent onBeforeLevelResultScreen,
            ActionEvent onAfterLevelResultScreen,
            GuiController guiController,
            LaunchInformation launchInformation)
            : base(GameState.LEVEL_RESULT_SCREEN, onBeforeLevelResultScreen, onAfterLevelResultScreen)
        {
            this.guiController = guiController;
            this.launchInformation = launchInformation;
        }

        protected override void Enter()
        {
            base.Enter();

            guiController.ShowLevelResultScreen(launchInformation);
        }

        protected override void Exit()
        {
            guiController.HideLevelResultScreen();

            base.Exit();
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
