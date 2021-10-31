using UnityEngine;

namespace Features.Camera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraSnappingPosition", menuName = "Camera/SnappingPosition")]
    public class PosRotObject_SO : ScriptableObject
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private Quaternion rotation;

        public Vector3 Pos() => position;
        public Quaternion Rot() => rotation;
    }
}