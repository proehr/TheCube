using UnityEngine;

namespace Features.Camera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraPreset", menuName = "Camera/Preset")]
    public class CameraPreset_SO : ScriptableObject
    {
        [Header("Design Related")]
        [Tooltip("Current movementSpeed")]
        public float movementSpeed;
        [Tooltip("Smoothness (Time it takes to Lerp between two values)")]
        public float movementTime;
        [Tooltip("Default Speed for actions")]
        public float normalSpeed;
        [Tooltip("Faster Speed when pressing LEFT-SHIFT")]
        public float fastSpeed;
        [Tooltip("idk")]
        public float rotationAmount;
        [Tooltip("idk")]
        public Vector3 zoomAmount;
        
        public Vector3 defaultZoom;
        
        [Header("Debug Related")]
        public Vector3 newPosition;
        public Quaternion newRotation;
        public Vector3 newZoom;

        public bool resetDragPositions;
        public Vector3 dragStartPosition;
        public Vector3 dragCurrentPosition;
        public Vector3 dragPositionOffset;
        
        [Header("Debug Input Related")]
        public Vector2 mousePosition;
        public Vector2 clickMousePosition;
        public Vector2 movementInput;
        public float shiftInput;
        public float leftRotationInput;
        public float rightRotationInput;
        public float zoomInput;

        public void ResetValues()
        {
            newPosition = new Vector3(0, 0, 0);
            newRotation.x = 0;
            newRotation.y = 0;
            newRotation.z = 0;
            newZoom = defaultZoom;
            
            if (resetDragPositions)
            {
                dragStartPosition = new Vector3(0, 0, 0);
                dragCurrentPosition = new Vector3(0, 0, 0);
                dragPositionOffset = new Vector3(0, 0, 0);
            }

            mousePosition = new Vector2(0, 0);
            clickMousePosition = new Vector2(0, 0);
            zoomInput = 0;
            movementInput = new Vector2(0, 0);
        }
        private void OnEnable()
        {
            ResetValues();
        }
        private void OnDisable()
        {
            ResetValues();
        }
    }
}
