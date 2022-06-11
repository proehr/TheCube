using System;
using Features.LandingPod.Scripts;
using UnityEngine;

namespace Features.Gui.Hud.Scripts
{
    public class LevelResultBehaviour : MonoBehaviour
    {
        [SerializeField] private LaunchCompletedActionEvent onLaunchCompleted;
        [SerializeField] private GameObject screenContentWin;
        [SerializeField] private GameObject screenContentLose;
        
        private void Awake()
        {
            onLaunchCompleted.RegisterListener(ResultEvaluation);
        }

        private void ResultEvaluation(LaunchInformation launchInformation)
        {
            var levelWon = launchInformation is { userTriggered: true };
            if (screenContentWin)
            {
                this.screenContentWin.SetActive(levelWon);
            }
            if (screenContentLose)
            {
                this.screenContentLose.SetActive(!levelWon);
            }
        }
    }
}
