using Features.Planet_Generation.Scripts;
using UnityEngine;

namespace Features.Planet.Resources.Scripts
{
    [CreateAssetMenu]
    public class PlanetCubes_SO : ScriptableObject
    {
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;
        
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

        public void RemoveAllCubes()
        {
            for (int x = 0; x < cubes.Length; x++)
            {
                for (int y = 0; y < cubes[x].Length; y++)
                {
                    for (int z = 0; z < cubes[x][y].Length; z++)
                    {
                        if (cubes[x][y][z] == null) continue;

                        Destroy(cubes[x][y][z].gameObject);
                        cubes[x][y][z] = null;
                    }
                }
            }
        }
    }
}
