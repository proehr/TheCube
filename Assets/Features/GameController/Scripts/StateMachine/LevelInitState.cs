﻿using DataStructures.Event;
using Features.Inventory.Scripts;
using Features.Integrity.Scripts;
using Features.LandingPod.Scripts;
using Features.Planet_Generation.Scripts;
using Features.WorkerAI.Scripts;

namespace Features.GameController.Scripts.StateMachine
{
    internal class LevelInitState : AbstractGameState
    {
        private readonly Inventory_SO inventory;
        private readonly PlanetGenerator planetGenerator;
        private readonly LandingPodManager landingPodManager;
        private readonly IntegrityBehaviour integrityBehaviour;
        private readonly WorkerService_SO workerService;

        public LevelInitState(
            ActionEvent onBeforeLevelInit,
            ActionEvent onAfterLevelInit,
            Inventory_SO inventory,
            PlanetGenerator planetGenerator,
            LandingPodManager landingPodManager,
            IntegrityBehaviour integrityBehaviour,
            WorkerService_SO workerService)
            : base(GameState.LEVEL_INIT, onBeforeLevelInit, onAfterLevelInit)
        {
            this.inventory = inventory;
            this.planetGenerator = planetGenerator;
            this.landingPodManager = landingPodManager;
            this.integrityBehaviour = integrityBehaviour;
            this.workerService = workerService;
        }

        protected override void Enter()
        {
            base.Enter();

            inventory.Reset();
            planetGenerator.Generate();
            // For now, the landing pod is always placed "on top" - later this could be input by the player
            landingPodManager.PlaceLandingPod(planetGenerator.GetSurface(Surface.POSITIVE_Y));
            integrityBehaviour.InitializeIntegrity();
            workerService.OnLevelStart();
        }

        protected override bool ValidateNextState(AbstractGameState nextState)
        {
            return nextState == GameState.GAMEPLAY;
        }
    }
}
