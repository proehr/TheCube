using DataStructures.Event;
using Features.Gui.Scripts;
using Features.LandingPod.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelResultScreenState : AbstractGameState
    {
        private readonly GuiController guiController;
        private readonly LaunchInformation launchInformation;
        private readonly PlanetGenerator planetGenerator;

        public LevelResultScreenState(ActionEvent onBeforeLevelResultScreen,
            ActionEvent onAfterLevelResultScreen,
            GuiController guiController,
            PlanetGenerator planetGenerator,
            LaunchInformation launchInformation)
            : base(GameState.LEVEL_RESULT_SCREEN, onBeforeLevelResultScreen, onAfterLevelResultScreen)
        {
            this.guiController = guiController;
            this.planetGenerator = planetGenerator;
            this.launchInformation = launchInformation;
        }

        protected override void Enter()
        {
            base.Enter();

            planetGenerator.Destroy();
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
