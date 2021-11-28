using System;
using DataStructures.Event;
using UnityEngine;

namespace Features.Planet.Resources.Scripts
{
    [CreateAssetMenu(fileName = "newCubeExcavationFinishedActionEvent", menuName = "Utils/ActionEvents/CubeExcavationFinished")]
    public class CubeExcavationFinishedActionEvent : ActionEventWithParameter<Cube>
    {
    }
}
