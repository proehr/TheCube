using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraPreset", menuName = "Camera/Preset")]
    public class CameraPreset_SO : ScriptableObject
    {
        [Header("Smoothness between planar movement in seconds")]
        public float movementTime;
        [Header("Smoothness between planar rotation in seconds")]
        public float planarRotationSpeed;
        [Header("Smoothness between face rotations in seconds")]
        public float faceRotationSpeed;
        [Header("Smoothness between Zooms in seconds")]
        public float zoomSpeed;
        [Header("Default Speed for actions")]
        public float normalSpeed;
        [Header("Faster Speed when pressing LEFT-SHIFT")]
        public float fastSpeed;
        [Header("Circular Movement Clamp Radius")][Range(0, 300)]
        public int clampMovement;
        [Header("Default Rotation Speed")]
        public float rotationAmount;
        [Header("Default Zoom Speed")]
        public Vector3 zoomAmount;
        [Header("Zoom Clamp Y (MIN MAX)")][MinMaxSlider(15, 100, true)]
        public Vector2 zoomYClamp;
        [Header("Zoom Clamp Z (MIN MAX)")][MinMaxSlider(-175, -90, true)]
        public Vector2 zoomZClamp;
    }
}
