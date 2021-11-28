using DataStructures.Event;
using UnityEngine;

namespace Features.Commands.Scripts.ActionEvents
{
    [CreateAssetMenu(fileName = "newCommandFinishedActionEvent", menuName = "Utils/ActionEvents/CommandFinished")]
    public class CommandFinishedActionEvent : ActionEventWithParameter<Command>
    {
    }
}
