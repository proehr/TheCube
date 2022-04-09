using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCubeFace", menuName = "Camera/CubeFace")]
    public class CubeFace_SO : ScriptableObject
    {
        [SerializeField] private CubeCorner_SO[] adjacentCorners = new CubeCorner_SO[4];
        [SerializeField] private Vector3 offset;

        public CubeCorner_SO[] AdjacentCorners => adjacentCorners;

        public Vector3 Offset => offset;
    }
}