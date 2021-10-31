using System.Collections.Generic;
using Features.Input;
using UnityEngine;

namespace Features.MovableCamera.Logic
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform movementCameraTransform;
        [SerializeField] private CameraPreset_SO cameraPreset;
        [SerializeField] private CameraDebugViewer_SO cameraDebugViewer;

        [SerializeField] private List<PosRotObject_SO> snappingPositions = new List<PosRotObject_SO>(6);
        
        private PlayerControls playerControls;
        
        private Vector3 newPosition;
        private Quaternion newRotation;
        private Vector3 newZoom;

        [Header("Current movementSpeed")]
        private float movementSpeed;

        private Vector2 movementInput;
        private float shiftInput;
        private float leftRotationInput;
        private float rightRotationInput;
        private float zoomInput;
        
        private void Awake()
        {
            playerControls = new PlayerControls();
            
            Transform cameraRigTransform = transform;
            newPosition = cameraRigTransform.position;
            newRotation = cameraRigTransform.rotation;
            
            newZoom = movementCameraTransform.localPosition;
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }
        
        private void Update()
        {
            HandleSnappingInput();
            HandleKeyboardInput();
        }
        
        private void HandleKeyboardInput()
        {
            // Get Inputs
            movementInput = playerControls.CameraActionMap.PlanarMovement.ReadValue<Vector2>();
            shiftInput = playerControls.CameraActionMap.ShiftSpeed.ReadValue<float>();
            leftRotationInput = playerControls.CameraActionMap.LeftRotationMovement.ReadValue<float>();
            rightRotationInput = playerControls.CameraActionMap.RigthRotationMovement.ReadValue<float>();
            zoomInput = playerControls.CameraActionMap.Zoom.ReadValue<float>();
            
            // if shift is pressed add extraSpeed
            movementSpeed = shiftInput > 0 ? cameraPreset.fastSpeed : cameraPreset.normalSpeed;

            if (movementInput.x < 0)
            {
                newPosition += (transform.right * -movementSpeed);
            }

            if (movementInput.x > 0)
            {
                newPosition += (transform.right * movementSpeed);
            }

            if (movementInput.y < 0)
            {
                newPosition += (transform.forward * -movementSpeed);
            }

            if (movementInput.y > 0)
            {
                newPosition += (transform.forward * movementSpeed);
            }

            if (leftRotationInput > 0)
            {
                newRotation *= Quaternion.Euler(Vector3.up * cameraPreset.rotationAmount);
            }

            if (rightRotationInput > 0)
            {
                newRotation *= Quaternion.Euler(Vector3.up * -cameraPreset.rotationAmount);
            }
            
            if (zoomInput > 0)
            {
                newZoom -= cameraPreset.zoomAmount;
            }

            if (zoomInput < 0)
            {
                newZoom += cameraPreset.zoomAmount;
            }
            
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * cameraPreset.movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * cameraPreset.movementTime);
            movementCameraTransform.localPosition = Vector3.Lerp(movementCameraTransform.localPosition, newZoom, Time.deltaTime * cameraPreset.movementTime);
            
            // assign Values for Debug
            cameraDebugViewer.newPosition = newPosition;
            cameraDebugViewer.newRotation = newRotation;
            cameraDebugViewer.newZoom = newZoom;
            cameraDebugViewer.movementSpeed = movementSpeed;
            
            cameraDebugViewer.movementInput = movementInput;
            cameraDebugViewer.shiftInput = shiftInput;
            cameraDebugViewer.leftRotationInput = leftRotationInput;
            cameraDebugViewer.rightRotationInput = rightRotationInput;
            cameraDebugViewer.zoomInput = zoomInput;
        }

        private void HandleSnappingInput()
        {
            float snapAInput = playerControls.CameraActionMap.SnapSideA.ReadValue<float>();
            float snapBInput = playerControls.CameraActionMap.SnapSideB.ReadValue<float>();
            float snapCInput = playerControls.CameraActionMap.SnapSideC.ReadValue<float>();
            float snapDInput = playerControls.CameraActionMap.SnapSideD.ReadValue<float>();
            float snapEInput = playerControls.CameraActionMap.SnapSideE.ReadValue<float>();
            float snapFInput = playerControls.CameraActionMap.SnapSideF.ReadValue<float>();

            if (snapAInput > 0)
            {
                newPosition = snappingPositions[0].Position();
                newRotation = snappingPositions[0].Rotation();
            }

            if (snapBInput > 0)
            {
                newPosition = snappingPositions[1].Position();
                newRotation = snappingPositions[1].Rotation();
            }

            if (snapCInput > 0)
            {
                newPosition = snappingPositions[2].Position();
                newRotation = snappingPositions[2].Rotation();
            }

            if (snapDInput > 0)
            {
                newPosition = snappingPositions[3].Position();
                newRotation = snappingPositions[3].Rotation();
            }

            if (snapEInput > 0)
            {
                newPosition = snappingPositions[4].Position();
                newRotation = snappingPositions[4].Rotation();
            }

            if (snapFInput > 0)
            {
                newPosition = snappingPositions[5].Position();
                newRotation = snappingPositions[5].Rotation();
            }
        }
    }
}
