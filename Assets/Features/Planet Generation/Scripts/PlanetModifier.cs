using UnityEngine;

namespace Features.PlanetGeneration.Scripts
{
    public abstract class PlanetModifier : ScriptableObject
    {
        public abstract void ModifyPlanet(Resource_SO[][][] resources);
    }
}