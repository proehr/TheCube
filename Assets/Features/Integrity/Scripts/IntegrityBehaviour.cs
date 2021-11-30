using DataStructures.Variables;
using Features.LandingPod.Scripts;
using Features.Planet.Resources.Scripts;
using Features.PlanetGeneration.Scripts;
using Features.PlanetGeneration.Scripts.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Integrity.Scripts
{
    public class IntegrityBehaviour : MonoBehaviour
    {
        [SerializeField] private PlanetGeneratedActionEvent onPlanetGenerated;
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;
        [SerializeField] private PlanetCubes_SO planetCubes;

        [SerializeField] private FloatVariable integrityThreshold;
        [SerializeField] private FloatVariable currentIntegrity;
        [SerializeField, ReadOnly] private float maxStability;
        [SerializeField, ReadOnly] private float currentStability;

        private bool isInitialized;
        private float[][][] cubeDistanceFromCenter;
        private float maxDistanceFromCenter;
    
        private void Awake()
        {
            onPlanetGenerated.RegisterListener(InitializeIntegrity);
            onCubeRemoved.RegisterListener(UpdateCurrentStability);
        }

        private void UpdateCurrentStability(Cube cube)
        {
            currentStability -= cube.resourceData.IntegrityBlockValue * Mathf.Pow(
                2f - cubeDistanceFromCenter[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] /
                maxDistanceFromCenter, 2);
        }

        private void InitializeIntegrity(PlanetGenerator planetGenerator)
        {
            cubeDistanceFromCenter = CalculateDistanceFromCenter(planetCubes.GetCubes());
            maxDistanceFromCenter = planetCubes.cubeLayerCount;
            CalculateMaxStability(planetCubes.GetCubes());
            currentStability = maxStability;
            isInitialized = true;
        }

        private float[][][] CalculateDistanceFromCenter(Cube[][][] cubes)
        {
            float[][][] cubeDistance = new float[cubes.Length][][];
        
            for (int i = 0; i < cubes.Length; i++)
            {
                cubeDistance[i] = new float[cubes.Length][];
                for (int j = 0; j < cubes[i].Length; j++)
                {
                    cubeDistance[i][j] = new float[cubes.Length];
                    for (int k = 0; k < cubes[i][j].Length; k++)
                    {
                        Cube currentCube = cubes[i][j][k];
                        if (currentCube != null)
                        {
                            cubeDistance[i][j][k] = Mathf.Max(currentCube.planetPosition.x, currentCube.planetPosition.y, currentCube.planetPosition.z);
                        }
                    }
                }
            }
            return cubeDistance;
        }
        
        private void CalculateMaxStability(Cube[][][] cubes)
        {
            maxStability = 0.0f;
        
            for (int i = 0; i < cubes.Length; i++)
            {
                for (int j = 0; j < cubes[i].Length; j++)
                {
                    for (int k = 0; k < cubes[i][j].Length; k++)
                    {
                        if (cubes[i][j][k] != null)
                        {
                            maxStability += cubes[i][j][k].resourceData.IntegrityBlockValue *
                                            Mathf.Pow(2f - (cubeDistanceFromCenter[i][j][k] / maxDistanceFromCenter), 2);
                        }
                    }
                }
            }
        }
    
        private void Update()
        {
            if (!isInitialized) return;
            
            currentIntegrity.Add((1 - currentStability / maxStability) * Time.deltaTime);
        
            if (currentIntegrity.Get() < integrityThreshold.Get())
            {
                onLaunchTriggered.Raise(new LaunchInformation());
                Debug.Log("Lost");
                isInitialized = false;
            }
        }
    }
}
