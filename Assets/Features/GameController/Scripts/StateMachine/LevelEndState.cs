using DataStructures.Event;
using Features.Inventory.Scripts;
using Features.LandingPod.Scripts;
using Features.WorkerAI.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelEndState : AbstractGameState
    {
        private readonly LandingPodManager landingPodManager;
        private readonly LaunchInformation launchInformation;
        private readonly WorkerService_SO workerService;
        private readonly Inventory_SO inventory;

        public LevelEndState(ActionEvent onBeforeLevelEnd,
            ActionEvent onAfterLevelEnd,
            LandingPodManager landingPodManager,
            LaunchInformation launchInformation,
            WorkerService_SO workerService,
            Inventory_SO inventory)
            : base(GameState.LEVEL_END, onBeforeLevelEnd, onAfterLevelEnd)
        {
            this.landingPodManager = landingPodManager;
            this.launchInformation = launchInformation;
            this.workerService = workerService;
            this.inventory = inventory;
        }

        protected override void Enter()
        {
            base.Enter();

            landingPodManager.Launch(launchInformation);
            workerService.OnLevelEnd();
            inventory.Reset();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.LEVEL_RESULT_SCREEN;
        }
    }
}
