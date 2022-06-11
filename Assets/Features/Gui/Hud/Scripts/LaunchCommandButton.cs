using DataStructures.Variables;
using Features.LandingPod.Scripts;
using UnityEngine;

namespace Features.Gui.Hud.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LaunchCommandButton : MonoBehaviour
    {
        // TODO adjust visual & interactivity state accordingly
        [SerializeField] private BoolVariable commandsAllowed;
        [SerializeField] private IntVariable relicAmount;
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private MouseEventHandler mouseEventHandler;
        private CanvasGroup buttonCanvasGroup;


        private void Start()
        {
            this.buttonCanvasGroup = GetComponent<CanvasGroup>();
            AdjustVisibility();
        }

        public void OnRelicAmountChanged()
        {
            AdjustVisibility();
        }

        /**
         * The button is only visible if there's at least 1 relic in the inventory.
         */
        private void AdjustVisibility()
        {
            if (relicAmount.Get() <= 0)
            {
                buttonCanvasGroup.alpha = 0;
                buttonCanvasGroup.interactable = false;
                if (mouseEventHandler)
                {
                    mouseEventHandler.enabled = false;
                }
            }
            else
            {
                buttonCanvasGroup.alpha = 1;
                buttonCanvasGroup.interactable = true;
                if (mouseEventHandler)
                {
                    mouseEventHandler.enabled = true;
                }
            }
        }

        public void OnClick()
        {
            if (commandsAllowed.Not()) return;

            onLaunchTriggered.Raise(new LaunchInformation(true));
        }
    }
}
