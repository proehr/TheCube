using DataStructures.Variables;
using Features.LandingPod.Scripts;
using Features.Planet.Resources.Scripts;
using Features.Planet_Generation.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Integrity.Scripts
{
    public class IntegrityBehaviour : MonoBehaviour
    {
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;
        [SerializeField] private PlanetCubes_SO planetCubes;

        [SerializeField] private FloatVariable integrityThreshold;
        [SerializeField] private FloatVariable currentIntegrity;
        [SerializeField, ReadOnly] private float maxStability;
        [SerializeField, ReadOnly] private float currentStability;

        private bool isInitialized;
        private float maxDistanceFromCenter;
    
        private void Awake()
        {
            onCubeRemoved.RegisterListener(UpdateCurrentStability);
        }

        private void UpdateCurrentStability(Cube cube)
        {
            currentStability -= cube.resourceData.IntegrityCubeValue * Mathf.Pow(
                2f - planetCubes.GetCubeLayerDistanceFromCenter(cube.planetPosition) /
                maxDistanceFromCenter, 2);
        }

        public void InitializeIntegrity()
        {
            maxDistanceFromCenter = planetCubes.CubeLayerCount;
            CalculateMaxStability(planetCubes.GetCubes());
            currentStability = maxStability;
            isInitialized = true;
        }
        
        private void CalculateMaxStability(Cube[][][] cubes)
        {
            maxStability = 0.0f;
        
            for (int x = 0; x < cubes.Length; x++)
            {
                for (int y = 0; y < cubes[x].Length; y++)
                {
                    for (int z = 0; z < cubes[x][y].Length; z++)
                    {
                        Cube currentCube = cubes[x][y][z];
                        if (currentCube != null)
                        {
                            maxStability += currentCube.resourceData.IntegrityCubeValue *
                                            Mathf.Pow(2f - (planetCubes.GetCubeLayerDistanceFromCenter(currentCube.planetPosition) 
                                                            / maxDistanceFromCenter), 2);
                        }
                    }
                }
            }
        }
    
        private void Update()
        {
            if (!isInitialized) return;
            
            currentIntegrity.Add((1 - currentStability / maxStability) * Time.deltaTime);
        
            if (currentIntegrity.Get() > integrityThreshold.Get())
            {
                onLaunchTriggered.Raise(new LaunchInformation());
                isInitialized = false;
            }
        }
    }
}

