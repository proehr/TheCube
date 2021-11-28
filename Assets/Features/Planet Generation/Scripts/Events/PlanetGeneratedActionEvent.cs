using DataStructures.Event;
using UnityEngine;

namespace Features.Planet_Generation.Scripts.Events
{
    [CreateAssetMenu(fileName = "newPlanetGeneratedActionEvent", menuName = "Utils/ActionEvents/PlanetGenerated")]
    public class PlanetGeneratedActionEvent : ActionEventWithParameter<PlanetGenerator>
    {
    }
}
