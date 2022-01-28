using DataStructures.Event;
using Features.LandingPod.Scripts;
using Features.Planet_Generation.Scripts;
using Features.PlanetIntegrity.Scripts;
using Features.WorkerAI.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    public class LevelInitState : AbstractGameState
    {
        private readonly PlanetGenerator planetGenerator;
        private readonly LandingPodManager landingPodManager;
        private readonly IntegrityManager integrityManager;
        private readonly WorkerService_SO workerService;

        public LevelInitState(
            ActionEvent onBeforeLevelInit,
            ActionEvent onAfterLevelInit,
            PlanetGenerator planetGenerator,
            LandingPodManager landingPodManager,
            IntegrityManager integrityManager,
            WorkerService_SO workerService)
            : base(GameState.LEVEL_INIT, onBeforeLevelInit, onAfterLevelInit)
        {
            this.planetGenerator = planetGenerator;
            this.landingPodManager = landingPodManager;
            this.integrityManager = integrityManager;
            this.workerService = workerService;
        }

        protected override void Enter()
        {
            base.Enter();

            planetGenerator.Generate();
            // For now, the landing pod is always placed "on top" - later this could be input by the player
            landingPodManager.PlaceLandingPod(planetGenerator.GetSurface(Surface.POSITIVE_Y));
            integrityManager.Initialize();
            workerService.OnLevelStart();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.GAMEPLAY;
        }
    }
}
