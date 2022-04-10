using DataStructures.Event;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LaunchInformation
    {
        /**
         * true = the user collected the relic(s) and started the launch via a command --> up to the next planet
         * false = something happened and the landing pod had to flee the planet --> retry same planet
         */
        public bool userTriggered {get; private set; }
        // TODO add whatever infos are required, e.g. next planet etc

        public LaunchInformation(bool userTriggered) {
            this.userTriggered = userTriggered;
        }
    }

    [CreateAssetMenu(fileName = "OnLaunchTriggeredEvent", menuName = "Utils/Typed Action Events/LaunchTriggered")]
    public class LaunchTriggeredActionEvent : ActionEventWithParameter<LaunchInformation>
    {
    }
}
