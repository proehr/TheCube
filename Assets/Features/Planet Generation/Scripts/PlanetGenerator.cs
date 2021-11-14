using Features.Planet_Generation.Scripts;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlanetGenerator : MonoBehaviour
{
    [SerializeField] private Planet_SO planetData;
    [SerializeField] private NavMeshSurface[] navMeshSurfaces = new NavMeshSurface[6];

    private Resource_SO[][][] resourceArrangement;

    void Start()
    {
        Generate();
        CreateGameObjects();
        GenerateNavMesh();
    }

    private void Generate()
    {
        InitSeededRandomization();
        InitWithDefaultResource();
        ApplyPlanetModifiers();
        PlaceRelics();
    }

    private void InitSeededRandomization()
    {
        if (planetData.Seed != 0)
        {
            Random.InitState(planetData.Seed);
        }
    }

    private void InitWithDefaultResource()
    {
        resourceArrangement = new Resource_SO[planetData.Size][][];
        for (int i = 0; i < planetData.Size; i++)
        {
            resourceArrangement[i] = new Resource_SO[planetData.Size][];
            for (int j = 0; j < planetData.Size; j++)
            {
                resourceArrangement[i][j] = new Resource_SO[planetData.Size];
                for (int k = 0; k < planetData.Size; k++)
                {
                    resourceArrangement[i][j][k] = planetData.DefaultResource;
                }
            }
        }
    }

    private void ApplyPlanetModifiers()
    {
        foreach (var planetModifier in planetData.PlanetModifiers)
        {
            planetModifier.ModifyPlanet(resourceArrangement);
        }
    }

    private void PlaceRelics()
    {
        int i = 0;
        while (i < planetData.Relic.Amount)
        {
            int distance = Mathf.Min(planetData.RelicDistanceToSurface, planetData.Size / 2);
            int relicX = Random.Range(0 + distance, planetData.Size - distance);
            int relicY = Random.Range(0 + distance, planetData.Size - distance);
            int relicZ = Random.Range(0 + distance, planetData.Size - distance);
            if (resourceArrangement[relicX][relicY][relicZ] != null)
            {
                resourceArrangement[relicX][relicY][relicZ] = planetData.Relic;
                i++;
            }
        }
    }

    private void GenerateNavMesh()
    {
        Debug.Assert(navMeshSurfaces != null && navMeshSurfaces.Length == 6);
        for (int i = 0; i < 6; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

    private void CreateGameObjects()
    {
        for (int i = 0; i < resourceArrangement.Length; i++)
        {
            for (int j = 0; j < resourceArrangement[i].Length; j++)
            {
                for (int k = 0; k < resourceArrangement[i][j].Length; k++)
                {
                    if (resourceArrangement[i][j][k] != null)
                    {
                        GameObject resource = Instantiate(resourceArrangement[i][j][k].ResourcePrefab, transform);
                        float resourceScale = resource.transform.localScale.x;
                        resource.transform.localPosition = new Vector3(
                            resourceScale * (i - resourceArrangement.Length / 2),
                            resourceScale * (j - resourceArrangement[i].Length / 2),
                            resourceScale * (k - resourceArrangement[i][j].Length / 2));
                    }
                }
            }
        }
    }
}
