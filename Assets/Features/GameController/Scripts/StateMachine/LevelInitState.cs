using DataStructures.Event;
using DataStructures.Variables;
using Features.Inventory.Scripts;
using Features.LandingPod.Scripts;
using Features.Planet.Progression.Scripts;
using Features.Planet_Generation.Scripts;
using Features.PlanetIntegrity.Scripts;
using Features.WorkerAI.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelInitState : AbstractGameState
    {
        private readonly Inventory_SO inventory;
        private readonly PlanetGenerator planetGenerator;
        private readonly PlanetProgression_SO planetProgressionData;
        private readonly LandingPodManager landingPodManager;
        private readonly IntegrityBehaviour integrityBehaviour;
        private readonly WorkerService_SO workerService;
        private readonly BoolVariable commandsAllowed;

        public LevelInitState(
            ActionEvent onBeforeLevelInit,
            ActionEvent onAfterLevelInit,
            Inventory_SO inventory,
            PlanetGenerator planetGenerator,
            PlanetProgression_SO planetProgressionData,
            LandingPodManager landingPodManager,
            IntegrityBehaviour integrityBehaviour,
            WorkerService_SO workerService,
            BoolVariable commandsAllowed)
            : base(GameState.LEVEL_INIT, onBeforeLevelInit, onAfterLevelInit)
        {
            this.inventory = inventory;
            this.planetGenerator = planetGenerator;
            this.planetProgressionData = planetProgressionData;
            this.landingPodManager = landingPodManager;
            this.integrityBehaviour = integrityBehaviour;
            this.workerService = workerService;
            this.commandsAllowed = commandsAllowed;
        }

        protected override void Enter()
        {
            base.Enter();

            planetGenerator.SetPlanetData(planetProgressionData.GetNextPlanetData());
            inventory.Reset();
            planetGenerator.Generate();
            // For now, the landing pod is always placed "on top" - later this could be input by the player
            landingPodManager.PlaceLandingPod(planetGenerator.GetSurface(Surface.POSITIVE_Y));
            integrityBehaviour.enabled = true;
            integrityBehaviour.InitializeIntegrity();
            workerService.OnLevelStart();
            commandsAllowed.Set(true);
        }

        protected override void Update()
        {
            // Wait for the landing pod to land before proceeding
            if (!landingPodManager.isLanded) return;

            base.Update();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.GAMEPLAY;
        }
    }
}
