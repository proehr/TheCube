using DataStructures.Event;
using DataStructures.Variables;
using Features.Commands.Scripts;
using Features.GameController.Scripts.StateMachine;
using Features.Gui.Scripts;
using Features.Inventory.Scripts;
using Features.LandingPod.Scripts;
using Features.MovableCamera.Logic;
using Features.Planet.Progression.Scripts;
using Features.PlanetIntegrity.Scripts;
using Features.SaveLoad.Scripts;
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
    ///         <b>When would you use the Game Controller vs event based inter-feature communication?</b><br />
    ///         The question has to be: Is the event part of the game loop? Is the order of execution
    ///         relevant of potential listeners? Does the event alter the game state?
    ///         If all of this is 'yes', this should be handled via the Game Controller. If one of those
    ///         is a 'no', you will need to find another way to handle this event.
    ///     </para>
    /// </remarks>
    internal class GameController : MonoBehaviour
    {
        [Header("Most important - the Game State")]
        [SerializeField] private GameState_SO gameStateData;

        [Header("Game Features & Components")]
        [SerializeField] private PlanetGenerator planetGenerator;
        [SerializeField] private PlanetProgression_SO planetProgressionData;
        [SerializeField] private LandingPodManager landingPodManager;
        [SerializeField] private IntegrityBehaviour integrityBehaviour;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private WorkerService_SO workerService;
        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private Inventory_SO inventory;
        [SerializeField] private SaveGameManager saveGameManager;
        [SerializeField] private BoolVariable commandsAllowed;
        [SerializeField] private WorkerCommandHandler workerCommandHandler;
        [SerializeField] private CommandInputHandler commandInputHandler;

        [Header("Inbound Game Events")]
        [SerializeField] private ActionEvent onShowStartScreen;
        [SerializeField] private ActionEvent onStartRequested;
        [SerializeField] private ActionEvent onUnloadLevel;
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private LaunchCompletedActionEvent onLaunchCompleted;
        [SerializeField] private ActionEvent onPauseRequested;
        [SerializeField] private ActionEvent onStartGameplay;
        [SerializeField] private ActionEvent onExitRequested;

        [Header("Outbound Game Events")]
        [SerializeField] private ActionEvent onBeforeStartScreen;
        [SerializeField] private ActionEvent onAfterStartScreen;

        [SerializeField] private ActionEvent onBeforeLevelInit;
        [SerializeField] private ActionEvent onAfterLevelInit;

        [SerializeField] private ActionEvent onBeforeGameplay;
        [SerializeField] private ActionEvent onAfterGameplay;

        [SerializeField] private ActionEvent onBeforePauseScreen;
        [SerializeField] private ActionEvent onAfterPauseScreen;

        [SerializeField] private ActionEvent onBeforeLevelEnd;
        [SerializeField] private ActionEvent onAfterLevelEnd;

        [SerializeField] private ActionEvent onBeforeLaunch;
        [SerializeField] private ActionEvent onAfterLaunch;

        [SerializeField] private ActionEvent onBeforeLevelResultScreen;
        [SerializeField] private ActionEvent onAfterLevelResultScreen;

        [SerializeField] private ActionEvent onBeforeGameExiting;
        [SerializeField] private ActionEvent onAfterGameExiting;

        private void Awake()
        {
            onShowStartScreen.RegisterListener(StartGameScreen);
            onStartRequested.RegisterListener(InitLevel);
            onUnloadLevel.RegisterListener(UnloadLevel);
            onLaunchTriggered.RegisterListener(EndLevel);
            onLaunchCompleted.RegisterListener(ShowLevelResultScreen);
            onPauseRequested.RegisterListener(Pause);
            onStartGameplay.RegisterListener(QueueGameplayState);
            onExitRequested.RegisterListener(Exit);
        }

        private void Start()
        {
            StartGameScreen();
        }

        private void Update()
        {
            gameStateData.Update();
        }

        private void StartGameScreen()
        {
            gameStateData.Set(
                new StartScreenState(
                    onBeforeStartScreen,
                    onAfterStartScreen,
                    canvasManager));
        }

        private void InitLevel()
        {
            gameStateData.Set(
                new LevelInitState(
                    onBeforeLevelInit,
                    onAfterLevelInit,
                    inventory,
                    planetGenerator,
                    planetProgressionData,
                    landingPodManager,
                    integrityBehaviour,
                    workerService,
                    commandsAllowed));

            // Level Init will wait with proceeding into the next state until the landing sequence is fully done

            QueueGameplayState();
        }

        private void QueueGameplayState()
        {
            gameStateData.Set(
                new GameplayState(
                    onBeforeGameplay,
                    onAfterGameplay));

            // And now we wait - until the planet disintegrates or the
            // relic was retrieved and we initiate the launch procedure.
        }

        private void Pause()
        {
            gameStateData.Set(
                new PauseScreenState(
                    onBeforePauseScreen,
                    onAfterPauseScreen));
        }
        
        private void EndLevel(LaunchInformation launchInformation)
        {
            UnloadLevel();
            
            LaunchPod(launchInformation);
        }

        private void UnloadLevel()
        {
            gameStateData.Set(
                new LevelEndState(
                    onBeforeLevelEnd,
                    onAfterLevelEnd,
                    integrityBehaviour,
                    cameraController,
                    commandsAllowed,
                    workerCommandHandler,
                    commandInputHandler,
                    landingPodManager));
        }

        private void LaunchPod(LaunchInformation launchInformation)
        {
            gameStateData.Set(
                new LaunchingState(
                    onBeforeLaunch,
                    onAfterLaunch,
                    landingPodManager,
                    launchInformation));

            // And now we wait until the launch is completed
        }

        private void ShowLevelResultScreen(LaunchInformation launchInformation)
        {
            gameStateData.Set(
                new LevelResultScreenState(
                    onBeforeLevelResultScreen,
                    onAfterLevelResultScreen,
                    workerService,
                    planetGenerator));
        }

        private void Exit()
        {
            gameStateData.Set(
                new GameExitingState(
                    onBeforeGameExiting,
                    onAfterGameExiting,
                    saveGameManager));
        }
    }
}
