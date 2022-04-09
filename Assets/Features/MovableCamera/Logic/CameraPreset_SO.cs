using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraPreset", menuName = "Camera/Preset")]
    public class CameraPreset_SO : ScriptableObject
    {
        [Header("Smoothness for planar movement and Zoom in seconds")]
        [SerializeField, Range(0,2)] private float smoothTime;
        [Header("Planar rotation speed in seconds")]
        [SerializeField, Range(0,2)] private float planarRotationSpeed;
        [Header("Face rotations speed in seconds")]
        [SerializeField, Range(0,2)] private float faceRotationSpeed;
        [Header("Smoothness between Zooms in seconds")]
        [SerializeField, Range(0,100)] private float zoomSpeed;
        [Header("Default Speed for actions")]
        [SerializeField, Range(1,200)] private float normalSpeed;
        [Header("Faster Speed when pressing LEFT-SHIFT")]
        [SerializeField, Range(100,400)] private float fastSpeed;
        [Header("Circular Movement Clamp Radius")]
        [SerializeField, Range(0, 300)] private int clampMovement;
        [Header("Amount that will change cameras y and z per scroll (z should be -y)")]
        [SerializeField] private Vector3 zoomAmount;
        [Header("Zoom Clamp Y [MIN MAX] (this will be planet size based later)")]
        [SerializeField, MinMaxSlider(15, 200, true)] private Vector2 zoomYClamp;
        [Header("Zoom Clamp Z [MIN MAX] (this will be planet size based later)")]
        [SerializeField, MinMaxSlider(-175, 10, true)] private Vector2 zoomZClamp;

        public float SmoothTime => smoothTime;
        public float PlanarRotationSpeed => planarRotationSpeed;
        public float FaceRotationSpeed => faceRotationSpeed;
        public float ZoomSpeed => zoomSpeed;
        public float NormalSpeed => normalSpeed;
        public float FastSpeed => fastSpeed;
        public int ClampMovement => clampMovement;
        public Vector3 ZoomAmount => zoomAmount;
        public Vector2 ZoomYClamp => zoomYClamp;
        public Vector2 ZoomZClamp => zoomZClamp;
    }
}
