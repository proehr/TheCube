using DataStructures.Event;
using Features.Planet.Resources.Scripts;
using UnityEngine;

namespace Features.Commands.Scripts.ActionEvents
{
    [CreateAssetMenu(fileName = "newWorkerCommandActionEvent", menuName = "Utils/ActionEvents/WorkerCommand")]
    public class WorkerCommandActionEvent : ActionEventWithParameter<Cube>
    {
    }
}
