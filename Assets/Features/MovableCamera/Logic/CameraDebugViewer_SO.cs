using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraDebugViewer", menuName = "Camera/DebugViewer")]
    public class CameraDebugViewer_SO : ScriptableObject
    {
        [ReadOnly] public Vector3 newPosition;
        [ReadOnly] public Vector3 newZoom;

        [Header("Current movementSpeed")]
        [ReadOnly] public float movementSpeed;

        [ReadOnly] public Vector2 movementInput;
        [ReadOnly] public float shiftInput;
        [ReadOnly] public float leftRotationInput;
        [ReadOnly] public float rightRotationInput;
        [ReadOnly] public float zoomInput;
        
        [ReadOnly] public Vector3 startPosition;
        [ReadOnly] public Vector3 startRotation;
        [ReadOnly] public Vector3 startZoom;

        public void ResetValues()
        {
            newPosition = Vector3.zero;
            newZoom = Vector3.zero;
            
            startPosition = Vector3.zero;
            startRotation = Vector3.zero;
            startZoom = Vector3.zero;

            zoomInput = 0;
            movementInput = Vector2.zero;
            
            
        }

        private void OnEnable()
        {
            ResetValues();
        }
    }
}
