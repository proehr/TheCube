using DataStructures.Variables;
using Features.LandingPod.Scripts;
using Features.Planet.Resources.Scripts;
using Features.Planet_Generation.Scripts.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Planet_Integrity
{
    public class Integrity : MonoBehaviour
    {
        [SerializeField] private PlanetGeneratedActionEvent onPlanetGenerated;
        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private PlanetCubes_SO planetCubes;

        [SerializeField] private FloatVariable integrityThreshold;
        [SerializeField] private FloatVariable currentIntegrity;
        [SerializeField][ReadOnly] private float maxStability;
        [SerializeField][ReadOnly] private float currentStability;

        private bool isInitialized;
        private float[][][] cubeDistanceFromCenter;
        private float maxDistanceFromCenter;
    
        private void Awake()
        {
            onPlanetGenerated.RegisterListener(InitializeIntegrity);
        }

        private void InitializeIntegrity(PlanetGenerator planetGenerator)
        {
            cubeDistanceFromCenter = CalculateDistanceFromCenter(planetCubes.GetCubes());
            maxStability = CalculateStability(planetCubes.GetCubes());
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
                        if (cubes[i][j][k] != null)
                        {
                            Vector3 blockPosition = new Vector3(
                                Mathf.Abs(i - cubes.Length / 2),
                                Mathf.Abs(j - cubes[i].Length / 2),
                                Mathf.Abs(k - cubes[i][j].Length / 2));
                            cubeDistance[i][j][k] = Mathf.Max(blockPosition.x, blockPosition.y, blockPosition.z);

                            maxDistanceFromCenter = Mathf.Max(maxDistanceFromCenter, cubeDistance[i][j][k]);
                        }
                    }
                }
            }

            return cubeDistance;
        }
    
        private void Update()
        {
            if (!isInitialized) return;
        
            currentStability = CalculateStability(planetCubes.GetCubes());
            currentIntegrity.Add(-(1 - currentStability / maxStability) * Time.deltaTime);
        
            if (currentIntegrity.Get() < integrityThreshold.Get())
            {
                onLaunchTriggered.Raise(new LaunchInformation());
                Debug.Log("Lost");
            }
        }

        private float CalculateStability(Cube[][][] cubes)
        {
            float newStability = default;
        
            for (int i = 0; i < cubes.Length; i++)
            {
                for (int j = 0; j < cubes[i].Length; j++)
                {
                    for (int k = 0; k < cubes[i][j].Length; k++)
                    {
                        if (cubes[i][j][k] != null)
                        {
                            newStability += cubes[i][j][k].resourceData.IntegrityBlockValue *
                                            Mathf.Pow(2f - (cubeDistanceFromCenter[i][j][k] / maxDistanceFromCenter), 2);
                        }
                    }
                }
            }

            return newStability;
        }
    }
}
