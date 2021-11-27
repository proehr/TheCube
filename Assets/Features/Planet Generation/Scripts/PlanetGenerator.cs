using System;
using System.Collections.Generic;
using Features.Planet.Resources.Scripts;
using Features.Planet_Generation.Scripts;
using Features.Planet_Generation.Scripts.Events;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetGenerator : MonoBehaviour
{
    [SerializeField] private Planet_SO planetData;
    [SerializeField] private NavMeshSurface[] navMeshSurfaces = new NavMeshSurface[6];
    [SerializeField] private PlanetGeneratedActionEvent onPlanetGenerated;

    private Resource_SO[][][] resourceArrangement;
    private Dictionary<Surface, List<GameObject>> surfaces;

    void Start()
    {
        Generate();
        CreateGameObjects();
        GenerateNavMesh();
        onPlanetGenerated.Raise(this);
    }

    private void Generate()
    {
        InitSeededRandomization();
        InitSurfaces();
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

    private void InitSurfaces()
    {
        surfaces = new Dictionary<Surface, List<GameObject>>();
        foreach (Surface surface in Enum.GetValues(typeof(Surface)))
        {
            surfaces.Add(surface, new List<GameObject>());
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
        while (i < planetData.Relic.Count)
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
                    var resourceData = resourceArrangement[i][j][k];
                    if (resourceData != null)
                    {
                        GameObject resource = Instantiate(resourceData.ResourcePrefab, transform);
                        var cube = resource.GetComponent<Cube>();
                        if (cube != null)
                        {
                            cube.Init(resourceData);
                        }
                        float resourceScale = resource.transform.localScale.x;
                        resource.transform.localPosition = new Vector3(
                            resourceScale * (i - resourceArrangement.Length / 2),
                            resourceScale * (j - resourceArrangement[i].Length / 2),
                            resourceScale * (k - resourceArrangement[i][j].Length / 2));

                        // TODO generalize for all surfaces
                        if (j == resourceArrangement[i].Length - 1)
                        {
                            surfaces[Surface.POSITIVE_Y].Add(resource);
                        }
                    }
                }
            }
        }
    }

    public GameObject[] GetSurface(Surface surface)
    {
        return surfaces[surface].ToArray();
    }
}
