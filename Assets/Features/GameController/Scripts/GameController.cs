using System;
using DataStructures.Event;
using Features.GameController.Scripts.StateMachine;
using Features.Inventory.Scripts;
using Features.LandingPod.Scripts;
using Features.Planet_Generation.Scripts;
using Features.PlanetIntegrity.Scripts;
using Features.WorkerAI.Scripts;
using UnityEngine;
namespace Features.GameController.Scripts
{
    /// <summary>
    ///     <para>
    ///         There shall be no references to this class outside of the Features.GameController namespace!
    ///         Do not call the GameController, the GameController calls you(r feature).
    ///     </para>
    ///     <para>
    ///         The Features.GameController namespace provides a collection of GameEvents that it listens to get notified about
    ///         various things happening in the game and progressing the game state.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>When would you use the Game Controller vs event based inter-feature communication?</b><br/>
    ///         The question has to be: Is the event part of the game loop? Is the order of execution
    ///         relevant of potential listeners? Does the event alter the game state?
    ///         If all of this is 'yes', this should be handled via the Game Controller. If one of those
    ///         is a 'no', you will need to find another way to handle this event.
    ///     </para>
    /// </remarks>
    public class GameController : MonoBehaviour
    {
        [Header("Most important - the Game State")]
        [SerializeField] private GameState_SO gameStateData;

        [Header("Game Features & Components")]
        [SerializeField] private PlanetGenerator planetGenerator;
        [SerializeField] private LandingPodManager landingPodManager;
        [SerializeField] private Integrity integrity;
        [SerializeField] private WorkerService_SO workerService;
        [SerializeField] private Inventory_SO inventory;

        [Header("Game Events")]
        // TODO prolly needs its own class
        [SerializeField] private ActionEvent onStartRequested;
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        // TODO prolly needs its own class
        [SerializeField] private ActionEvent onPauseRequested;
        // TODO prolly needs its own class
        [SerializeField] private ActionEvent onExitRequested;

        private void Awake()
        {
            onStartRequested.RegisterListener(InitLevel);
            onLaunchTriggered.RegisterListener(EndLevel);
            onPauseRequested.RegisterListener(Pause);
            onExitRequested.RegisterListener(Exit);

            gameStateData.Set(new StartScreenState());
        }

        private void Update()
        {
            gameStateData.Update();
        }

        private void InitLevel()
        {
            // TODO refactor to move all this method(s) into the states itself - so the state does stuff and protects the state transitions
            if (gameStateData.GetId() != AbstractGameState.GameState.START_SCREEN &&
                gameStateData.GetId() != AbstractGameState.GameState.LEVEL_END)
            {
                throw new InvalidOperationException("Invalid Game State transition");
            }

            gameStateData.Set(new LevelInitState());

            planetGenerator.Generate();
            // For now, the landing pod is always placed "on top" - later this could be input by the player
            landingPodManager.PlaceLandingPod(planetGenerator.GetSurface(Surface.POSITIVE_Y));
            integrity.Initialize();
            workerService.OnLevelStart();

            StartGameplay();
        }

        private void StartGameplay()
        {
            if (gameStateData.GetId() != AbstractGameState.GameState.LEVEL_INIT &&
                gameStateData.GetId() != AbstractGameState.GameState.PAUSE_SCREEN)
            {
                throw new InvalidOperationException("Invalid Game State transition");
            }

            gameStateData.Set(new GameplayState());

            // And now we wait - until the planet disintegrates or the
            // relic was retrieved and we initiate the launch procedure.
        }

        private void Pause()
        {
            if (gameStateData.GetId() != AbstractGameState.GameState.GAMEPLAY)
            {
                throw new InvalidOperationException("Invalid Game State transition");
            }

            gameStateData.Set(new PauseScreenState());

            // TODO Tell UI to Show Pause Screen OR make UI listen to the game state and show the according screen
        }

        private void EndLevel(LaunchInformation launchInformation)
        {
            if (gameStateData.GetId() != AbstractGameState.GameState.GAMEPLAY)
            {
                throw new InvalidOperationException("Invalid Game State transition");
            }

            gameStateData.Set(new LevelEndState());

            landingPodManager.Launch(launchInformation);
            workerService.OnLevelEnd();
            inventory.Reset();
            // TODO Tell UI to Show Level Result Screen OR make UI listen to the game state and show the according screen

            gameStateData.Set(new LevelResultScreenState());
        }

        private void Exit()
        {
            if (gameStateData.GetId() != AbstractGameState.GameState.START_SCREEN &&
                gameStateData.GetId() != AbstractGameState.GameState.PAUSE_SCREEN &&
                gameStateData.GetId() != AbstractGameState.GameState.LEVEL_RESULT_SCREEN)
            {
                throw new InvalidOperationException("Invalid Game State transition");
            }

            gameStateData.Set(new GameExitingState());

            // TODO Save?

            Application.Quit();
        }
    }
}
