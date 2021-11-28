using DataStructures.Event;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LaunchInformation
    {
        // TODO add whatever infos are required, e.g. next planet etc
    }

    [CreateAssetMenu(fileName = "OnLaunchTriggeredEvent", menuName = "Utils/ActionEvents/LaunchTriggered")]
    public class LaunchTriggeredActionEvent : ActionEventWithParameter<LaunchInformation>
    {
    }
}
