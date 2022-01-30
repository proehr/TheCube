using DataStructures.Event;
using Features.LandingPod.Scripts;
using UnityEngine;

namespace Features.Gui.Scripts
{
    public class GuiController : MonoBehaviour
    {
        [SerializeField] private ActionEvent onStartRequested;

        public void ShowStartScreen()
        {
            // TODO Implement

            // This is just here to simulate the game state flow - this
            // event would be raised when the player clicks "Start Game"
            onStartRequested.Raise();
        }

        public void HideStartScreen()
        {
            // TODO Implement
        }

        public void ShowPauseScreen()
        {
            // TODO Implement
        }

        public void HidePauseScreen()
        {
            // TODO Implement
        }

        public void ShowLevelResultScreen(LaunchInformation launchInformation)
        {
            // TODO Implement

            // This is just here to simulate the game state flow - this
            // event would be raised when the player clicks "Next Level"
            onStartRequested.Raise();
        }

        public void HideLevelResultScreen()
        {
            // TODO Implement
        }
    }
}
