using DataStructures.Event;
using Features.Planet.Resources.Scripts;
using UnityEngine;

namespace Features.Commands.Scripts.ActionEvents
{
    [CreateAssetMenu(fileName = "newWorkerCommandActionEvent", menuName = "Utils/Typed Action Events/WorkerCommand")]
    public class WorkerCommandActionEvent : ActionEventWithParameter<Cube>
    {
    }
}
