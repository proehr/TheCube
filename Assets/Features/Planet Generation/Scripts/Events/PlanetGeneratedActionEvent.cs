using System;
using UnityEngine;

namespace Features.Planet_Generation.Scripts.Events
{
    [CreateAssetMenu(fileName = "newPlanetGeneratedActionEvent", menuName = "Utils/ActionEvents/PlanetGenerated")]
    public class PlanetGeneratedActionEvent : ScriptableObject
    {
        Action<PlanetGenerator> listeners;

        public void Raise(PlanetGenerator caller)
        {
            this.listeners?.Invoke(caller);
        }

        public void RegisterListener(Action<PlanetGenerator> listener)
        {
            this.listeners += listener;
        }

        public void UnregisterListener(Action<PlanetGenerator> listener)
        {
            this.listeners -= listener;
        }
    }
}
