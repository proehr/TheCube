using System;
using Features.Planet_Generation.Scripts;
using UnityEngine;

namespace Features.Planet.Resources.Scripts
{
    [CreateAssetMenu]
    public class PlanetCubes_SO : ScriptableObject
    {
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;

        private Cube[][][] cubes;
        private int[][][] cubeLayerDistanceFromCenter;
        private int size;

        public int CubeLayerCount { get; private set; }
        public int Size => size;

        public void Init(Planet_SO planetData)
        {
            this.size = planetData.Size;
            this.cubes = new Cube[size][][];
            cubeLayerDistanceFromCenter = new int[planetData.Size][][];
            for (int i = 0; i < size; i++)
            {
                cubes[i] = new Cube[size][];
                cubeLayerDistanceFromCenter[i] = new int[planetData.Size][];
                for (int j = 0; j < size; j++)
                {
                    cubes[i][j] = new Cube[size];
                    cubeLayerDistanceFromCenter[i][j] = new int[planetData.Size];
                }
            }
            this.onCubeRemoved.RegisterListener(RemoveCube);

            CubeLayerCount = planetData.Size / 2;
        }

        public void AddCube(Cube cube)
        {
            Vector3Int planetPos = cube.planetPosition;
            cubes[planetPos.x][planetPos.y][planetPos.z] = cube;

            cubeLayerDistanceFromCenter[planetPos.x][planetPos.y][planetPos.z] =
                Mathf.Max(Mathf.Abs(planetPos.x - cubes.Length / 2),
                    Mathf.Abs(planetPos.y - cubes[planetPos.x].Length / 2),
                    Mathf.Abs(planetPos.z - cubes[planetPos.x][planetPos.y].Length / 2));
        }

        public Cube GetCubeAt(Vector3Int position) => cubes[position.x][position.y][position.z];

        public Cube GetCubeAt(int x, int y, int z) => cubes[x][y][z];

        public bool HasCubeAt(int x, int y, int z)
        {
            if (x < 0 || x >= size) return false;
            if (y < 0 || y >= size) return false;
            if (z < 0 || z >= size) return false;

            return cubes[x][y][z] != null;
        }

        [Obsolete("Use GetCubes(Action<Cube>) instead.", false)]
        public Cube[][][] GetCubes() => cubes;

        public int GetCubeLayerDistanceFromCenter(Vector3Int position) =>  cubeLayerDistanceFromCenter[position.x][position.y][position.z];

        /// <summary>
        /// Calls a delegate with all cubes in the planet.
        /// This is the primary way of iterating through all cubes in a planet.
        ///
        /// The action gets the cube and the x, y, z index as parameters provided.
        /// </summary>
        public void GetCubes(Action<Cube, int, int, int> action)
        {
            if (cubes == null) return;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (cubes[x][y][z] == null) continue;

                        action(cubes[x][y][z], x, y, z);
                    }
                }
            }
        }

        private void RemoveCube(Cube cube)
        {
            cubes[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] = null;
            cubeLayerDistanceFromCenter[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] = 0;
        }

        public void RemoveAllCubes()
        {
            if (cubes == null) return;

            for (int x = 0; x < cubes.Length; x++)
            {
                for (int y = 0; y < cubes[x].Length; y++)
                {
                    for (int z = 0; z < cubes[x][y].Length; z++)
                    {
                        if (cubes[x][y][z] == null) continue;

                        if (Application.isPlaying)
                        {
                            Destroy(cubes[x][y][z].gameObject);
                        }
                        else
                        {
                            // In Editor Mode
                            DestroyImmediate(cubes[x][y][z].gameObject);
                        }
                        cubes[x][y][z] = null;
                        cubeLayerDistanceFromCenter[x][y][z] = 0;
                        CubeLayerCount = 0;
                    }
                }
            }
        }
    }
}
