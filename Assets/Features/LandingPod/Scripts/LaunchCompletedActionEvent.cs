using DataStructures.Event;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    [CreateAssetMenu(fileName = "OnLaunchCompletedEvent", menuName = "Utils/ActionEvents/LaunchCompleted")]
    public class LaunchCompletedActionEvent : ActionEventWithParameter<LaunchInformation>
    {
    }
}
