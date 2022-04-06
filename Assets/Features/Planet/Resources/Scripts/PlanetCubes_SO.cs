using Features.Planet_Generation.Scripts;
using UnityEngine;

namespace Features.Planet.Resources.Scripts
{
    [CreateAssetMenu]
    public class PlanetCubes_SO : ScriptableObject
    {
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;
        
        public int CubeLayerCount { get; private set; }
        
        private Cube[][][] cubes;
        private int[][][] cubeLayerDistanceFromCenter;

        public void Init(Planet_SO planetData)
        {
            cubes = new Cube[planetData.Size][][];
            cubeLayerDistanceFromCenter = new int[planetData.Size][][];
            for (int i = 0; i < planetData.Size; i++)
            {
                cubes[i] = new Cube[planetData.Size][];
                cubeLayerDistanceFromCenter[i] = new int[planetData.Size][];
                for (int j = 0; j < planetData.Size; j++)
                {
                    cubes[i][j] = new Cube[planetData.Size];
                    cubeLayerDistanceFromCenter[i][j] = new int[planetData.Size];
                }
            }
            onCubeRemoved.RegisterListener(RemoveCube);
            
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
        
        public int GetCubeLayerDistanceFromCenter(Vector3Int position) =>  cubeLayerDistanceFromCenter[position.x][position.y][position.z];
        
        public Cube[][][] GetCubes() => cubes;

        private void RemoveCube(Cube cube)
        {
            cubes[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] = null;
            cubeLayerDistanceFromCenter[cube.planetPosition.x][cube.planetPosition.y][cube.planetPosition.z] = 0;
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
