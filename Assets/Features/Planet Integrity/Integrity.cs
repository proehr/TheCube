using Features.Planet_Generation.Scripts;
using Features.Planet_Generation.Scripts.Events;
using Sirenix.OdinInspector;
using UnityEngine;

public class Integrity : MonoBehaviour
{
    [SerializeField] private PlanetGeneratedActionEvent onPlanetGenerated;

    [SerializeField] private float integrityThreshold = -1f;
    [SerializeField][ReadOnly] private float integrity;
    [SerializeField][ReadOnly] private float maxStability;
    [SerializeField][ReadOnly] private float currentStability;

    private bool isInitialized;
    private float[][][] cubeDistanceFromCenter;
    private float maxDistanceFromCenter;
    private Resource_SO[][][] resourceArrangement;
    
    private void Awake()
    {
        onPlanetGenerated.RegisterListener(InitializeIntegrity);
    }

    private void InitializeIntegrity(PlanetGenerator planetGenerator)
    {
        resourceArrangement = planetGenerator.GetRessources();
        cubeDistanceFromCenter = CalculateDistanceFromCenter(resourceArrangement);
        maxStability = CalculateStability();
        currentStability = maxStability;
        isInitialized = true;
    }

    private float[][][] CalculateDistanceFromCenter(Resource_SO[][][] resourceArrangement)
    {
        float[][][] cubeDistance = new float[resourceArrangement.Length][][];
        
        for (int i = 0; i < resourceArrangement.Length; i++)
        {
            cubeDistance[i] = new float[resourceArrangement.Length][];
            for (int j = 0; j < resourceArrangement[i].Length; j++)
            {
                cubeDistance[i][j] = new float[resourceArrangement.Length];
                for (int k = 0; k < resourceArrangement[i][j].Length; k++)
                {
                    if (resourceArrangement[i][j][k] != null)
                    {
                        Vector3 blockPosition = new Vector3(
                            Mathf.Abs(i - resourceArrangement.Length / 2),
                            Mathf.Abs(j - resourceArrangement[i].Length / 2),
                            Mathf.Abs(k - resourceArrangement[i][j].Length / 2));
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
        
        currentStability = CalculateStability();
        integrity -= (1 - currentStability / maxStability) * Time.deltaTime;
        
        if (integrity < integrityThreshold)
        {
            Debug.Log("Lost");
        }
    }

    private float CalculateStability()
    {
        float newStability = default;
        
        for (int i = 0; i < resourceArrangement.Length; i++)
        {
            for (int j = 0; j < resourceArrangement[i].Length; j++)
            {
                for (int k = 0; k < resourceArrangement[i][j].Length; k++)
                {
                    if (resourceArrangement[i][j][k] != null)
                    {
                        newStability += resourceArrangement[i][j][k].IntegrityBlockValue *
                                        Mathf.Pow(2f - (cubeDistanceFromCenter[i][j][k] / maxDistanceFromCenter), 2);
                    }
                }
            }
        }

        return newStability;
    }
    
    public void RemoveRandomCube()
    {
        while (true)
        {
            int firstPosition = Random.Range(0, resourceArrangement.Length);
            int secondPosition = Random.Range(0, resourceArrangement[firstPosition].Length);
            int thirdPosition = Random.Range(0, resourceArrangement[firstPosition][secondPosition].Length);
            if (resourceArrangement[firstPosition][secondPosition][thirdPosition] != null)
            {
                resourceArrangement[firstPosition][secondPosition][thirdPosition] = null;
                break;
            }
        }
    }
}
