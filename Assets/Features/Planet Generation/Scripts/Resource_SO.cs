using DataStructures.Variables;
using Features.Commands.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.PlanetGeneration.Scripts
{
    [CreateAssetMenu(fileName = "ResourceType", menuName = "Resource", order = 0)]
    public class Resource_SO : ScriptableObject
    {
        [Tooltip("Count of cubes with this resource type in planet")]
        [SerializeField] private int count;
        [Tooltip("Prefab used to instantiate this resource")]
        [SerializeField] private GameObject resourcePrefab;
        [Tooltip("The inventory slot that is filled when a cube of this resource is excavated.")]
        [SerializeField] private IntVariable inventoryResource;
        [Tooltip("Added amount of this resource to the inventory during excavation.\n" +
                 "The concrete amount is randomized per command.")]
        [MinMaxSlider(0,20,true)]
        [SerializeField] private Vector2Int excavationAmountInterval;
        [Tooltip("The values used when workers are commanded to excavate a cube of this resource.")]
        [SerializeField] private Command_SO excavationCommandData;
        [Tooltip("The integrity multiplier per type of block")]
        [SerializeField] private float integrityBlockValue;

        public int Count => count;

        public GameObject ResourcePrefab => resourcePrefab;

        public IntVariable InventoryResource => inventoryResource;

        public Vector2Int ExcavationAmountInterval => excavationAmountInterval;

        public Command_SO ExcavationCommandData => excavationCommandData;

        public float IntegrityBlockValue => integrityBlockValue;
    }
}
