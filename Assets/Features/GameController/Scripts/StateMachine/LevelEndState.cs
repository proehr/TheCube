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
            // TODO Disable Integrity UI
            // workerCommandHandler.DisableNewCommands();
            workerService.DestroyAllWorkers();
            // TODO disable worker spanwing
            cameraController.ResetCamera();
            // Disable Movable Camera
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            switch (nextState.id)
            {
                case GameState.LAUNCHING:
                case GameState.START_SCREEN:
                    return true;
                default:
                    return false;
            }
        }
    }
}
