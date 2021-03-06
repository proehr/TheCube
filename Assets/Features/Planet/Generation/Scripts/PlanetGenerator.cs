using System;
using System.Collections.Generic;
using Features.Commands.Scripts.ActionEvents;
using Features.ExtendedRandom;
using Features.Planet.Resources.Scripts;
using Features.Planet_Generation.Scripts;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Features.Planet.Generation.Scripts
{
    public class PlanetGenerator : MonoBehaviour
    {
        [SerializeField] private Planet_SO planetData;
        [SerializeField] private PlanetCubes_SO planetCubes;
        [SerializeField] private NavMeshSurface[] navMeshSurfaces = new NavMeshSurface[6];
        [SerializeField] private ExcavationStartedActionEvent onExcavationStarted;
    
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;
        /**
     * 1-4: top
     * 5-8: middle
     * 9-12: bottom
     */
        [SerializeField] private NavMeshLink[] navMeshLinks = new NavMeshLink[12];
        [SerializeField] private Transform obstaclesParent;
        [SerializeField] private NavMeshObstacle obstaclePrefab;

        [Tooltip("Just for preview purposes. Your workers will get stuck :(")]
        [SerializeField] private bool updateNavMeshOnCubeRemoval = false;

        private Resource_SO[][][] resourceArrangement;
        private Dictionary<Surface, List<GameObject>> surfaces;
        private Vector3 edgesMin;
        private Vector3 edgesMax;

        public void Generate()
        {
            InitSeededRandomization();
            InitSurfaces();
            InitWithDefaultResource();
            ApplyPlanetModifiers();
            PlaceRelics();

            planetCubes.Init(planetData);
            CreateGameObjects();
            GenerateNavMesh();
            onExcavationStarted.RegisterListener(PlaceNavMeshObstacle);
            onCubeRemoved.RegisterListener(UpdateNavMesh);
        }

        internal void SetPlanetData(Planet_SO planetData)
        {
            this.planetData = planetData;
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
                if (surface == Surface.NONE) continue;

                surfaces.Add(surface, new List<GameObject>());
            }
        }

        private void InitWithDefaultResource()
        {
            var resourceChoices = new List<Resource_SO>();
            for (int i = 0; i < planetData.DefaultResource.Count; i++)
            {
                resourceChoices.Add(planetData.DefaultResource);
            }
            foreach (var resource in planetData.Resources)
            {
                for (int i = 0; i < resource.Count; i++)
                {
                    resourceChoices.Add(resource);
                }
            }
            var resourceRandomSet = new RandomListSet<Resource_SO>(resourceChoices);
            resourceArrangement = new Resource_SO[planetData.Size][][];
            for (int i = 0; i < planetData.Size; i++)
            {
                resourceArrangement[i] = new Resource_SO[planetData.Size][];
                for (int j = 0; j < planetData.Size; j++)
                {
                    resourceArrangement[i][j] = new Resource_SO[planetData.Size];
                    for (int k = 0; k < planetData.Size; k++)
                    {
                        resourceArrangement[i][j][k] = resourceRandomSet.PickOne();
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
                int distance = Mathf.Min(planetData.MaxRelicDistanceToCore, planetData.Size / 2);
                int relicX = Random.Range(planetData.Size / 2 - distance, planetData.Size / 2 + distance);
                int relicY = Random.Range(planetData.Size / 2 - distance, planetData.Size / 2 + distance);
                int relicZ = Random.Range(planetData.Size / 2 - distance, planetData.Size / 2 + distance);
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
            //
            // Debug.Log("Created " + NavMeshSurface.activeSurfaces.Count + " navMeshSurfaces.");
            // int j = 1;
            // foreach (var navMeshSurface in NavMeshSurface.activeSurfaces)
            // {
            //     Debug.Log("Surface #" + j + ": " + navMeshSurface.center + " size: " + navMeshSurface.size + " => " + new Bounds(navMeshSurface.center, navMeshSurface.size));
            //     j++;
            // }

            // Debug.Log("edgesMin: " + edgesMin);
            // Debug.Log("edgesMax: " + edgesMax);

            // TODO clean-up after MVP - neither for the "Simple Worker Solution" nor the updatable nav mesh, those links are needed
            for (int i = 0; i < navMeshLinks.Length; i++)
            {
                Destroy(navMeshLinks[i]);
            }
        
            // navMeshLinks[0].transform.localPosition = new Vector3(0, edgesMax.y, edgesMin.z);
            // navMeshLinks[0].width = edgesMax.x - edgesMin.x;
            // navMeshLinks[1].transform.localPosition = new Vector3(0, edgesMax.y, edgesMax.z);
            // navMeshLinks[1].width = edgesMax.x - edgesMin.x;
            // navMeshLinks[2].transform.localPosition = new Vector3(edgesMin.x, edgesMax.y, 0);
            // navMeshLinks[2].width = edgesMax.z - edgesMin.z;
            // navMeshLinks[3].transform.localPosition = new Vector3(edgesMax.x, edgesMax.y, 0);
            // navMeshLinks[3].width = edgesMax.z - edgesMin.z;
            //
            // navMeshLinks[4].transform.localPosition = new Vector3(edgesMin.x, 0, edgesMin.z);
            // navMeshLinks[4].width = edgesMax.y - edgesMin.y;
            // navMeshLinks[5].transform.localPosition = new Vector3(edgesMax.x, 0, edgesMin.z);
            // navMeshLinks[5].width = edgesMax.y - edgesMin.y;
            // navMeshLinks[6].transform.localPosition = new Vector3(edgesMax.x, 0, edgesMax.z);
            // navMeshLinks[6].width = edgesMax.y - edgesMin.y;
            // navMeshLinks[7].transform.localPosition = new Vector3(edgesMin.x, 0, edgesMax.z);
            // navMeshLinks[7].width = edgesMax.y - edgesMin.y;
            //
            // navMeshLinks[8].transform.localPosition = new Vector3(0, edgesMin.y, edgesMin.z);
            // navMeshLinks[8].width = edgesMax.x - edgesMin.x;
            // navMeshLinks[9].transform.localPosition = new Vector3(0, edgesMin.y, edgesMax.z);
            // navMeshLinks[9].width = edgesMax.x - edgesMin.x;
            // navMeshLinks[10].transform.localPosition = new Vector3(edgesMin.x, edgesMin.y, 0);
            // navMeshLinks[10].width = edgesMax.z - edgesMin.z;
            // navMeshLinks[11].transform.localPosition = new Vector3(edgesMax.x, edgesMin.y, 0);
            // navMeshLinks[11].width = edgesMax.z - edgesMin.z;
        }

        private void CreateGameObjects()
        {
            this.edgesMin = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            this.edgesMax = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

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
                                cube.Init(resourceData, new Vector3Int(i, j, k));
                            }
                            planetCubes.AddCube(cube);
                        
                            float resourceScale = resource.transform.localScale.x;
                            var localPosition = new Vector3(
                                resourceScale * (i - resourceArrangement.Length / 2),
                                resourceScale * (j - resourceArrangement[i].Length / 2),
                                resourceScale * (k - resourceArrangement[i][j].Length / 2));
                            resource.transform.localPosition = localPosition;

                            var resourceRadius = resourceScale / 2;
                            if (localPosition.x - resourceRadius < edgesMin.x) edgesMin.x = localPosition.x - resourceRadius;
                            if (localPosition.y - resourceRadius < edgesMin.y) edgesMin.y = localPosition.y - resourceRadius;
                            if (localPosition.z - resourceRadius < edgesMin.z) edgesMin.z = localPosition.z - resourceRadius;
                            if (localPosition.x + resourceRadius > edgesMax.x) edgesMax.x = localPosition.x + resourceRadius;
                            if (localPosition.y + resourceRadius > edgesMax.y) edgesMax.y = localPosition.y + resourceRadius;
                            if (localPosition.z + resourceRadius > edgesMax.z) edgesMax.z = localPosition.z + resourceRadius;

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

        public Resource_SO[][][] GetRessources() => resourceArrangement;

        public GameObject[] GetSurface(Surface surface)
        {
            return surfaces[surface].ToArray();
        }

        private void PlaceNavMeshObstacle(Cube targetCube)
        {
            if (!surfaces[Surface.POSITIVE_Y].Contains(targetCube.gameObject) ) return;

            var targetTransform = targetCube.transform;
            Instantiate(obstaclePrefab, targetTransform.position, targetTransform.rotation, obstaclesParent);
        }

        private void UpdateNavMesh(Cube obj)
        {
            // TODO create navMeshLinks at edges of removed cube
            if (!updateNavMeshOnCubeRemoval) return;

            for (int i = 0; i < 6; i++)
            {
                navMeshSurfaces[i].UpdateNavMesh(navMeshSurfaces[i].navMeshData);
            }
        }

        public void Destroy()
        {
            planetCubes.RemoveAllCubes();
            foreach (Transform obstacles in obstaclesParent) {
                Destroy(obstacles.gameObject);
            }
        }
    }
}
