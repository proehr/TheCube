using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCubeCorner", menuName = "Camera/CubeCorner")]
    public class CubeCorner_SO : ScriptableObject
    {
        [SerializeField] private CubeFace_SO[] adjacentFaces = new CubeFace_SO[3];

        public CubeFace_SO[] AdjacentFaces => adjacentFaces;
    }
}
