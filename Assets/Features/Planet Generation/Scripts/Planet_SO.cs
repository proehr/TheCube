using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

namespace Features.Planet_Generation.Scripts
{
    [CreateAssetMenu(fileName = "PlanetConfiguration", menuName = "Planet", order = 0)]
    public class Planet_SO : ScriptableObject
    {
        [Tooltip("Number of cubes on one side of the planet")]
        [SerializeField] private int size;
        
        [Tooltip("Data for default resource in this planet. Amount will be ignored")]
        [SerializeField] private Resource_SO defaultResource;
        [Tooltip("Data for relics in this planet")]
        [SerializeField] private Resource_SO relic;
        [Tooltip("Data for resources in this planet")]
        [SerializeField] private List<Resource_SO> resources;
        [Tooltip("Data for all planet modifiers")]
        [SerializeField] private List<PlanetModifier> planetModifiers;
        
        [Tooltip("Seed used for randomization, no seed used if this is set to 0")]
        [SerializeField] private int seed;
        
        public int getSize => size;

        public Resource_SO getRelic => relic;
        
        public Resource_SO getDefaultResource => defaultResource;

        public List<Resource_SO> getResources => resources;

        public List<PlanetModifier> getPlanetModifiers => planetModifiers;

        public int getSeed => seed;
    }
}