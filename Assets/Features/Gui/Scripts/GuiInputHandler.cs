using DataStructures.Event;
using Features.GameController.Scripts;
using Features.GameController.Scripts.StateMachine;
using Features.LandingPod.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Gui.Scripts
{
    public class GuiInputHandler : MonoBehaviour
    {
        [SerializeField] private GameState_SO gameState;
        
        [Header("Enter Pause")]
        [SerializeField] private ActionEvent onPauseRequested;
        [SerializeField] private ActionEvent onStartGameplay;

#if UNITY_EDITOR
        [Header("Enter EndMenu (unity editor only)")]
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private LaunchCompletedActionEvent onLaunchCompleted;
#endif
        
        public void OnPauseGame(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            
            if (gameState.Get() is GameState.GAMEPLAY)
            {
                onPauseRequested.Raise();
            }
            else if (gameState.Get() is GameState.PAUSE_SCREEN)
            {
                onStartGameplay.Raise();
            }
        }
        
#if UNITY_EDITOR
        /// <summary>
        /// Implementation to quickly enter the end menu.
        /// </summary>
        /// <param name="context"></param>
        public void OnLaunchComplete(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            if (gameState.Get() is GameState.GAMEPLAY)
            {
                LaunchInformation launchInformation = new LaunchInformation();
                onLaunchTriggered.Raise(launchInformation);
                onLaunchCompleted.Raise(launchInformation);
            }
        }
#endif
    }
}
