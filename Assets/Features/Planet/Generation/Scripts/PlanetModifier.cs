using UnityEngine;

namespace Features.Planet_Generation.Scripts
{
    public abstract class PlanetModifier : ScriptableObject
    {
        public abstract void ModifyPlanet(Resource_SO[][][] resources);
    }
}