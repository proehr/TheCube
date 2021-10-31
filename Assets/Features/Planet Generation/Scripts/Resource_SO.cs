using System;
using UnityEngine;

namespace Features.Planet_Generation.Scripts
{
    [CreateAssetMenu(fileName = "ResourceType", menuName = "Resource", order = 0)]
    public class Resource_SO : ScriptableObject
    {
        [Tooltip("Amount of cubes with this resource type in planet")]
        [SerializeField] private int amount;
        [Tooltip("Prefab used to instantiate this resource")]
        [SerializeField] private GameObject resourcePrefab;

        public int Amount => amount;

        public GameObject ResourcePrefab => resourcePrefab;
    }
}