using System;
using Features.PlanetGeneration.Scripts;
using UnityEngine;

namespace Features.Planet.Resources.Scripts
{
    [CreateAssetMenu]
    public class PlanetCubes_SO : ScriptableObject
    {
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;
        
        public int cubeLayerCount { get; private set; }
        
        private Cube[][][] cubes;

        public void Init(Planet_SO planetData)
        {
            cubes = new Cube[planetData.Size][][];
            for (int i = 0; i < planetData.Size; i++)
            {
                cubes[i] = new Cube[planetData.Size][];
                for (int j = 0; j < planetData.Size; j++)
                {
                    cubes[i][j] = new Cube[planetData.Size];
                }
            }
            onCubeRemoved.RegisterListener(RemoveCube);

            cubeLayerCount = planetData.Size / 2;
        }

        public void AddCube(Cube cube)
        {
            cubes[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] = cube;
        }

        public Cube GetCubeAt(Vector3Int position) => cubes[position.x][position.y][position.z];
        
        public Cube[][][] GetCubes() => cubes;

        private void RemoveCube(Cube cube)
        {
            cubes[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] = null;
        }
    }
}
