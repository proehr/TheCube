using UnityEngine;

namespace Features.WorkerAI.Scripts
{
    [CreateAssetMenu(fileName = "CommandDefinition", menuName = "Command")]
    public class Command_SO : ScriptableObject
    {
        [Tooltip("How many workers are required to execute this command?")]
        [SerializeField] private int requiredWorkers;

        [Tooltip("How many seconds does it take to finish the command?")]
        [SerializeField] private int duration;

        [Tooltip("(Optional) Created prefab at the position of the issued command.")]
        [SerializeField] private GameObject commandPostPrefab;

        public int RequiredWorkers => requiredWorkers;
        public double Duration => duration * 1000;
        public GameObject CommandPostPrefab => commandPostPrefab;
    }
}
