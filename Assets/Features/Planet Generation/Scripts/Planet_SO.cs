using System.Collections.Generic;
using UnityEngine;

namespace Features.PlanetGeneration.Scripts
{
    [CreateAssetMenu(fileName = "PlanetConfiguration", menuName = "Planet", order = 0)]
    public class Planet_SO : ScriptableObject
    {
        [Tooltip("Number of cubes on one side of the planet")]
        [SerializeField] private int size;
        
        [Tooltip("Data for default resource in this planet. Count will be ignored")]
        [SerializeField] private Resource_SO defaultResource;
        [Tooltip("Data for relics in this planet")]
        [SerializeField] private Resource_SO relic;
        [Tooltip("Minimum distance of relic to surface, can not be bigger than half planet size")]
        [Min(0)][SerializeField] private int relicDistanceToSurface;
        [Tooltip("Data for resources in this planet")]
        [SerializeField] private List<Resource_SO> resources;
        [Tooltip("Data for all planet modifiers")]
        [SerializeField] private List<PlanetModifier> planetModifiers;
        
        [Tooltip("Seed used for randomization, no seed used if this is set to 0")]
        [SerializeField] private int seed;
        
        public int Size => size;

        public Resource_SO Relic => relic;
        
        public int RelicDistanceToSurface => relicDistanceToSurface;
        
        public Resource_SO DefaultResource => defaultResource;

        public List<Resource_SO> Resources => resources;

        public List<PlanetModifier> PlanetModifiers => planetModifiers;

        public int Seed => seed;
    }
}