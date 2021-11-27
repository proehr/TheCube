using UnityEngine;

namespace Features.Commands.Scripts
{
    [CreateAssetMenu(fileName = "CommandDefinition", menuName = "Command")]
    public class Command_SO : ScriptableObject
    {
        [Tooltip("How many workers are required to execute this command?")]
        [SerializeField] private int requiredWorkers = 4;

        [Tooltip("How close do workers have to be, before the commands starts?")]
        [SerializeField] private float maxDistanceToLocation = 7;

        [Tooltip("How many seconds does it take to finish the command?")]
        [SerializeField] private float duration = 15;

        [Tooltip("(Optional) Created prefab at the position of the issued command.")]
        [SerializeField] private GameObject commandPostPrefab;

        public int RequiredWorkers => requiredWorkers;
        public float Duration => duration;
        public GameObject CommandPostPrefab => commandPostPrefab;
        public float MaxDistanceToLocation => maxDistanceToLocation;
    }
}
