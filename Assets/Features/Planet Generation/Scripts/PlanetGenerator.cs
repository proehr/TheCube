using Features.Planet_Generation.Scripts;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    [SerializeField] private Planet_SO planetData;

    private Resource_SO[][][] resourceArrangement;

    void Start()
    {
        Generate();
        Load();
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
        if (planetData.getSeed != 0)
        {
            Random.InitState(planetData.getSeed);
        }
    }

    private void InitWithDefaultResource()
    {
        resourceArrangement = new Resource_SO[planetData.getSize][][];
        for (int i = 0; i < planetData.getSize; i++)
        {
            resourceArrangement[i] = new Resource_SO[planetData.getSize][];
            for (int j = 0; j < planetData.getSize; j++)
            {
                resourceArrangement[i][j] = new Resource_SO[planetData.getSize];
                for (int k = 0; k < planetData.getSize; k++)
                {
                    resourceArrangement[i][j][k] = planetData.getDefaultResource;
                }
            }
        }
    }

    private void ApplyPlanetModifiers()
    {
        foreach (var planetModifier in planetData.getPlanetModifiers)
        {
            planetModifier.ModifyPlanet(resourceArrangement);
        }
    }

    private void PlaceRelics()
    {
        int i = 0;
        while (i < planetData.getRelic.getAmount)
        {
            int relicX = Random.Range(0, planetData.getSize);
            int relicY = Random.Range(0, planetData.getSize);
            int relicZ = Random.Range(0, planetData.getSize);
            if (resourceArrangement[relicX][relicY][relicZ] != null)
            {
                resourceArrangement[relicX][relicY][relicZ] = planetData.getRelic;
                i++;
            }
        }
    }

    private void Load()
    {
        for (int i = 0; i < resourceArrangement.Length; i++)
        {
            for (int j = 0; j < resourceArrangement[i].Length; j++)
            {
                for (int k = 0; k < resourceArrangement[i][j].Length; k++)
                {
                    if (resourceArrangement[i][j][k] != null)
                    {
                        GameObject resource = Instantiate(resourceArrangement[i][j][k].getResourcePrefab, transform);
                        resource.transform.localPosition = new Vector3(10 * (i - resourceArrangement.Length / 2),
                            10 * (j - resourceArrangement[i].Length / 2),
                            10 * (k - resourceArrangement[i][j].Length / 2));
                    }
                }
            }
        }
    }
}