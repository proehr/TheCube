using Features.Inventory.Scripts;
using Features.WorkerAI.Scripts;
using Features.WorkerDTO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LandingPod : MonoBehaviour
    {
        [Header("Required References")] [SerializeField]
        private Inventory_SO inventory;

        [SerializeField] private WorkerBO_SO workerBO;
        private Transform workersParent;

        [Header("Landing Pod Positioning")]
        [Tooltip("How high does the Landing Pod float above the planet?")]
        [SerializeField]
        private float yOffset = 4.0f;

        [Header("Spawning Workers")] [SerializeField]
        private WorkerBehavior spawnedWorkerPrefab;

        [Tooltip("How many resources does it cost to spawn 1 worker?")] [SerializeField]
        private int workerCost = 2;

        [Tooltip("Cooldown in seconds before spawning a new worker.")] [SerializeField]
        private float workerSpawnCooldown = 5.0f;

        [ShowInInspector] [ReadOnly] private float currentCooldown;
        private float lastWorkerSpawnTime;

        private Vector3 spawnPosition;

        [Button(ButtonSizes.Medium)]
        private void SpawnWorker()
        {
            this.SpawnWorker(false);
        }

        public void Init(GameObject surfaceCube)
        {
            var thisTransform = this.transform;
            var surfaceCubeTransform = surfaceCube.transform;
            var surfaceCubePosition = surfaceCubeTransform.position;
            var surfaceCubeScale = surfaceCubeTransform.localScale;
            // From the cube position, add:
            // - half the cube height
            // - half the landing pod height
            // - the specified float offset
            thisTransform.position = surfaceCubePosition +
                                     surfaceCubeTransform.up * (surfaceCubeScale.y / 2 +
                                                                thisTransform.localScale.y / 2 + yOffset);
            thisTransform.rotation = surfaceCubeTransform.rotation;

            spawnPosition = surfaceCubePosition + surfaceCubeTransform.up * (surfaceCubeScale.y / 2);

            workersParent = GameObject.FindGameObjectWithTag("WorkersParent").transform;
            Debug.Assert(workersParent != null, "Missing a game object tagged 'WorkersParent' in the scene!");
        }

        private void Update()
        {
            if (lastWorkerSpawnTime > 0)
            {
                currentCooldown = workerSpawnCooldown - Time.time + lastWorkerSpawnTime;
            }

            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
                SpawnWorker(true);
            }
        }

        private void SpawnWorker(bool useResources)
        {
            if (useResources && inventory.Resource.Get() < workerCost)
            {
                return;
            }

            workerBO.InstantiateNewWorker(spawnedWorkerPrefab, spawnPosition, Quaternion.identity, workersParent);
            if (useResources)
            {
                inventory.Resource.Add(-workerCost);
            }

            lastWorkerSpawnTime = Time.time;
        }
    }
}
