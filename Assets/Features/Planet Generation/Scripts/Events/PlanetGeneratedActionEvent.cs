using DataStructures.Event;
using UnityEngine;

namespace Features.PlanetGeneration.Scripts.Events
{
    [CreateAssetMenu(fileName = "newPlanetGeneratedActionEvent", menuName = "Utils/ActionEvents/PlanetGenerated")]
    public class PlanetGeneratedActionEvent : ActionEventWithParameter<PlanetGenerator>
    {
    }
}
