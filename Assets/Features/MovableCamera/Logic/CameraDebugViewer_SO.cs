using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.MovableCamera.Logic
{
    [CreateAssetMenu(fileName = "NewCameraDebugViewer", menuName = "Camera/DebugViewer")]
    public class CameraDebugViewer_SO : ScriptableObject
    {
        [ReadOnly] public Vector3 newPosition;
        [ReadOnly] public Quaternion newRotation;
        [ReadOnly] public Vector3 newZoom;

        [Header("Current movementSpeed")]
        [ReadOnly] public float movementSpeed;

        [ReadOnly] public Vector2 movementInput;
        [ReadOnly] public float shiftInput;
        [ReadOnly] public float leftRotationInput;
        [ReadOnly] public float rightRotationInput;
        [ReadOnly] public float zoomInput;

        public void ResetValues()
        {
            newPosition = new Vector3(0, 0, 0);
            newRotation.x = 0;
            newRotation.y = 0;
            newRotation.z = 0;

            zoomInput = 0;
            movementInput = new Vector2(0, 0);
        }

        private void OnEnable()
        {
            ResetValues();
        }
    }
}
