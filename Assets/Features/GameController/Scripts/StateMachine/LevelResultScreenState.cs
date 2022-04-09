using DataStructures.Event;
using Features.Gui.Scripts;
using Features.LandingPod.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelResultScreenState : AbstractGameState
    {
        private readonly LaunchInformation launchInformation;
        private readonly PlanetGenerator planetGenerator;

        public LevelResultScreenState(ActionEvent onBeforeLevelResultScreen,
            ActionEvent onAfterLevelResultScreen,
            PlanetGenerator planetGenerator,
            LaunchInformation launchInformation)
            : base(GameState.LEVEL_RESULT_SCREEN, onBeforeLevelResultScreen, onAfterLevelResultScreen)
        {
            this.planetGenerator = planetGenerator;
            this.launchInformation = launchInformation;
        }

        protected override void Enter()
        {
            base.Enter();

            planetGenerator.Destroy();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.START_SCREEN:
                case GameState.LEVEL_INIT:
                case GameState.GAME_EXITING:
                    return true;
                default:
                    return false;
            }
        }
    }
}
