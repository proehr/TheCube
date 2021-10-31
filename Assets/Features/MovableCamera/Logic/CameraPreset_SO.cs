using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraPreset", menuName = "Camera/Preset")]
    public class CameraPreset_SO : ScriptableObject
    {
        [Tooltip("Smoothness (Time it takes to Lerp between two values)")]
        public float movementTime;
        [Tooltip("Default Speed for actions")]
        public float normalSpeed;
        [Tooltip("Faster Speed when pressing LEFT-SHIFT")]
        public float fastSpeed;
        [Tooltip("Default Rotation Speed")]
        public float rotationAmount;
        [Tooltip("Default Zoom Speed")]
        public Vector3 zoomAmount;
    }
}
