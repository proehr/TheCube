using DataStructures.Event;
using Features.Planet.Generation.Scripts;
using Features.WorkerAI.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelResultScreenState : AbstractGameState
    {
        private readonly WorkerService_SO workerService;
        private readonly PlanetGenerator planetGenerator;

        public LevelResultScreenState(ActionEvent onBeforeLevelResultScreen,
            ActionEvent onAfterLevelResultScreen,
            WorkerService_SO workerService,
            PlanetGenerator planetGenerator)
            : base(GameState.LEVEL_RESULT_SCREEN, onBeforeLevelResultScreen, onAfterLevelResultScreen)
        {
            this.workerService = workerService;
            this.planetGenerator = planetGenerator;
        }

        protected override void Enter()
        {
            base.Enter();

            workerService.DestroyAllWorkers();

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
