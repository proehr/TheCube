using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraSnappingPosition", menuName = "Camera/SnappingPosition")]
    public class PosRotObject_SO : ScriptableObject
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private Quaternion rotation;

        public Vector3 Position() => position;
        public Quaternion Rotation() => rotation;
    }
}
