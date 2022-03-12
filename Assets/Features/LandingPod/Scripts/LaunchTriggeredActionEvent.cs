using DataStructures.Event;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LaunchInformation
    {
        // TODO add whatever infos are required, e.g. next planet etc
    }

    [CreateAssetMenu(fileName = "OnLaunchTriggeredEvent", menuName = "Utils/Typed Action Events/LaunchTriggered")]
    public class LaunchTriggeredActionEvent : ActionEventWithParameter<LaunchInformation>
    {
    }
}
