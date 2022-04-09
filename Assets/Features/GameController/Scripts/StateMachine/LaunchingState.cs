using DataStructures.Event;
using Features.Inventory.Scripts;
using Features.LandingPod.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LaunchingState : AbstractGameState
    {
        private readonly LandingPodManager landingPodManager;
        private readonly LaunchInformation launchInformation;

        public LaunchingState(ActionEvent onBeforeLaunch,
            ActionEvent onAfterLaunch,
            LandingPodManager landingPodManager,
            LaunchInformation launchInformation)
            : base(GameState.LAUNCHING, onBeforeLaunch, onAfterLaunch)
        {
            
            this.landingPodManager = landingPodManager;
            this.launchInformation = launchInformation;
        }

        protected override void Enter()
        {
            base.Enter();

            landingPodManager.Launch(launchInformation);
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.LEVEL_RESULT_SCREEN;
        }
    }
}
