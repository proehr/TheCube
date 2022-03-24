using DataStructures.Event;
using Features.Inventory.Scripts;
using Features.LandingPod.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LaunchingState : AbstractGameState
    {
        private readonly Inventory_SO inventory;
        private readonly LandingPodManager landingPodManager;
        private readonly LaunchInformation launchInformation;

        public LaunchingState(ActionEvent onBeforeLaunch,
            ActionEvent onAfterLaunch,
            Inventory_SO inventory,
            LandingPodManager landingPodManager,
            LaunchInformation launchInformation)
            : base(GameState.LAUNCHING, onBeforeLaunch, onAfterLaunch)
        {
            this.inventory = inventory;
            this.landingPodManager = landingPodManager;
            this.launchInformation = launchInformation;
        }

        protected override void Enter()
        {
            base.Enter();

            landingPodManager.Launch(launchInformation);
            
            inventory.Reset();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.LEVEL_RESULT_SCREEN;
        }
    }
}
