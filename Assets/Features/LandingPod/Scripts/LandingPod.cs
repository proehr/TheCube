using Cinemachine;
using Features.Inventory.Scripts;
using Features.WorkerAI.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LandingPod : MonoBehaviour
    {
        [Header("Required References")]
        [SerializeField] private Inventory_SO inventory;

        [SerializeField] private WorkerService_SO workerService;
        
        [Header("References to camera in child ")]
        [SerializeField] private CinemachineVirtualCamera landingPodCam;

        public CinemachineVirtualCamera LandingPodCam => landingPodCam;

        [Header("Landing Pod Positioning")]
        [Tooltip("How high does the Landing Pod float above the planet?")]
        [SerializeField] private float yOffset = 4.0f;

        [Header("Spawning Workers")]
        [SerializeField] private WorkerBehavior spawnedWorkerPrefab;

        [Tooltip("How many resources does it cost to spawn 1 worker?")]
        [SerializeField] private int workerCost = 2;

        [Tooltip("Cooldown in seconds before spawning a new worker.")]
        [SerializeField] private float workerSpawnCooldown = 5.0f;

        [ShowInInspector] [ReadOnly] private float currentCooldown;
        private float lastWorkerSpawnTime;
        private bool spawnEnabled = true;
        private Vector3 spawnPosition;

        [Button(ButtonSizes.Medium)]
        private void SpawnWorker()
        {
            this.SpawnWorker(false);
        }

        // TODO can be refactored in one method or moved up to landingPodManager
        // TODO to either get the landing Position for Land() or init the pod for the first time
        
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
        }

        public Vector3 GetLandingPosition(GameObject surfaceCube)
        {
            var thisTransform = this.transform;
            var surfaceCubeTransform = surfaceCube.transform;
            var surfaceCubePosition = surfaceCubeTransform.position;
            var surfaceCubeScale = surfaceCubeTransform.localScale;
            // From the cube position, add:
            // - half the cube height
            // - half the landing pod height
            // - the specified float offset
            Vector3 landingPosition = surfaceCubePosition +
                                      surfaceCubeTransform.up * (surfaceCubeScale.y / 2 +
                                                                 thisTransform.localScale.y / 2 + yOffset);
            thisTransform.rotation = surfaceCubeTransform.rotation;

            spawnPosition = surfaceCubePosition + surfaceCubeTransform.up * (surfaceCubeScale.y / 2);

            return landingPosition;
        }

        private void Awake()
        {
            landingPodCam.LookAt = this.transform;
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
            if (!this.spawnEnabled)
            {
                Debug.LogWarning("Spawn is currently disabled.");
                return;
            }
            if (useResources && inventory.Resource.Get() < workerCost) return;

            workerService.InstantiateNewWorker(spawnedWorkerPrefab, spawnPosition, Quaternion.identity);
            if (useResources)
            {
                inventory.Resource.Add(-workerCost);
            }

            lastWorkerSpawnTime = Time.time;
        }

        public void EnableWorkerSpawn()
        {
            this.currentCooldown = 0;
            this.lastWorkerSpawnTime = 0;
            this.spawnEnabled = true;
        }

        public void DisableWorkerSpawn()
        {
            this.spawnEnabled = false;
        }
    }
}
