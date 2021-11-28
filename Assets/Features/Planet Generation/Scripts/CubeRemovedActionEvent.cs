﻿using DataStructures.Event;
using Features.Planet.Resources.Scripts;
using UnityEngine;

namespace Features.Planet_Generation.Scripts
{
    [CreateAssetMenu(fileName = "OnCubeRemovedEvent", menuName = "Utils/ActionEvents/CubeRemoved")]
    public class CubeRemovedActionEvent : ActionEventWithParameter<Cube>
    {
    }
}
