using DataStructures.Event;
using Features.Commands.Scripts;
using Features.MovableCamera.Logic;
using Features.WorkerAI.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelEndState : AbstractGameState
    {
        private readonly WorkerService_SO workerService;
        private readonly CameraController cameraController;
        private readonly WorkerCommandHandler workerCommandHandler;

        public LevelEndState(ActionEvent onBeforeLevelEnd,
            ActionEvent onAfterLevelEnd,
            WorkerService_SO workerService,
            CameraController cameraController,
            WorkerCommandHandler workerCommandHandler)
            : base(GameState.LEVEL_END, onBeforeLevelEnd, onAfterLevelEnd)
        {
            this.workerService = workerService;
            this.cameraController = cameraController;
            this.workerCommandHandler = workerCommandHandler;
        }

        protected override void Enter()
        {
            base.Enter();
            
            workerCommandHandler.CancelAllCommands();
            // TODO also think about re-enabling commands once LevelInits
            // TODO disable Commands and Command UI
            // TODO Disable Integrity Tracking and UI
            // workerCommandHandler.DisableNewCommands();
            workerService.DestroyAllWorkers();
            // TODO disable worker spanwing
            cameraController.OnResetCamera();
            // Disable Movable Camera
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.LAUNCHING;
        }
    }
}
