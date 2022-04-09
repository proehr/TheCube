using System;
using Features.LandingPod.Scripts;
using UnityEngine;

namespace Features.Gui.Hud.Scripts
{
    public class LevelResultBehaviour : MonoBehaviour
    {
        [SerializeField] private LaunchCompletedActionEvent onLaunchCompleted;
        
        private void Awake()
        {
            onLaunchCompleted.RegisterListener(ResultEvaluation);
        }

        private void ResultEvaluation(LaunchInformation launchInformation)
        {
            //TODO: implement result based on launchInformation
        }
    }
}
