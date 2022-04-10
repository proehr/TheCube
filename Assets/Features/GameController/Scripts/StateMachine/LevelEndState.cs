using DataStructures.Event;
using DataStructures.Variables;
using Features.Commands.Scripts;
using Features.LandingPod.Scripts;
using Features.MovableCamera.Logic;
using Features.PlanetIntegrity.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelEndState : AbstractGameState
    {
        private readonly IntegrityBehaviour integrityBehaviour;
        private readonly BoolVariable commandsAllowed;
        private readonly WorkerCommandHandler workerCommandHandler;
        private readonly CommandInputHandler commandInputHandler;
        private readonly LandingPodManager landingPodManager;
        private readonly CameraController cameraController;

        public LevelEndState(ActionEvent onBeforeLevelEnd,
            ActionEvent onAfterLevelEnd,
            IntegrityBehaviour integrityBehaviour,
            CameraController cameraController,
            BoolVariable commandsAllowed,
            WorkerCommandHandler workerCommandHandler,
            CommandInputHandler commandInputHandler,
            LandingPodManager landingPodManager)
            : base(GameState.LEVEL_END, onBeforeLevelEnd, onAfterLevelEnd)
        {
            this.integrityBehaviour = integrityBehaviour;
            this.cameraController = cameraController;
            this.commandsAllowed = commandsAllowed;
            this.workerCommandHandler = workerCommandHandler;
            this.commandInputHandler = commandInputHandler;
            this.landingPodManager = landingPodManager;
        }

        protected override void Enter()
        {
            base.Enter();

            integrityBehaviour.enabled = false;
            commandsAllowed.Set(false);
            commandInputHandler.RemoveCurrentHighlight();
            workerCommandHandler.CancelAllCommands();
            landingPodManager.DisableWorkerSpawn();
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
