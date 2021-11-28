using DataStructures.Event;
using UnityEngine;

namespace Features.Planet_Generation.Scripts
{
    [CreateAssetMenu(fileName = "OnCubeRemovedEvent", menuName = "Utils/ActionEvents/CubeRemoved")]
    public class CubeRemovedActionEvent : ActionEventWithParameter<Resource_SO>
    {
    }
}
