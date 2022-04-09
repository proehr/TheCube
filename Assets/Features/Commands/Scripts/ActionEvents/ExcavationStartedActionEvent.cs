using DataStructures.Event;
using Features.Planet.Resources.Scripts;
using UnityEngine;

namespace Features.Commands.Scripts.ActionEvents
{
    [CreateAssetMenu(fileName = "newExcavationStartedActionEvent", menuName = "Utils/Typed Action Events/ExcavationStarted")]
    public class ExcavationStartedActionEvent : ActionEventWithParameter<Cube>
    {
    }
}