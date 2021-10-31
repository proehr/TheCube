using System.Collections.Generic;
using Features.Input;
using UnityEngine;

namespace Features.Camera.Logic
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform movementCameraTransform;
        [SerializeField] private CameraPreset_SO cameraPreset;

        [SerializeField] private List<PosRotObject_SO> snappingPositions = new List<PosRotObject_SO>(6);
        
        private PlayerControls playerControls;

        private bool isSnapping;
        private void Awake()
        {
            playerControls = new PlayerControls();
            
            Transform cameraRigTransform = transform;
            cameraPreset.newPosition = cameraRigTransform.position;
            cameraPreset.newRotation = cameraRigTransform.rotation;
            
            cameraPreset.newZoom = movementCameraTransform.localPosition;
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
            Vector2 movementInput = playerControls.CameraActionMap.PlanarMovement.ReadValue<Vector2>();
            float shiftInput = playerControls.CameraActionMap.ShiftSpeed.ReadValue<float>();
            float leftRotationInput = playerControls.CameraActionMap.LeftRotationMovement.ReadValue<float>();
            float rightRotationInput = playerControls.CameraActionMap.RigthRotationMovement.ReadValue<float>();
            float zoomInput = playerControls.CameraActionMap.Zoom.ReadValue<float>();
            
            // if shift is pressed add extraSpeed
            cameraPreset.movementSpeed = shiftInput > 0 ? cameraPreset.fastSpeed : cameraPreset.normalSpeed;
            
            // assign Values for Debug
            cameraPreset.movementInput = movementInput;
            cameraPreset.shiftInput = shiftInput;
            cameraPreset.leftRotationInput = leftRotationInput;
            cameraPreset.rightRotationInput = rightRotationInput;
            cameraPreset.zoomInput = zoomInput;

            if (movementInput.x < 0)
            {
                cameraPreset.newPosition += (transform.right * -cameraPreset.movementSpeed);
            }

            if (movementInput.x > 0)
            {
                cameraPreset.newPosition += (transform.right * cameraPreset.movementSpeed);
            }

            if (movementInput.y < 0)
            {
                cameraPreset.newPosition += (transform.forward * -cameraPreset.movementSpeed);
            }

            if (movementInput.y > 0)
            {
                cameraPreset.newPosition += (transform.forward * cameraPreset.movementSpeed);
            }

            if (leftRotationInput > 0)
            {
                cameraPreset.newRotation *= Quaternion.Euler(Vector3.up * cameraPreset.rotationAmount);
            }

            if (rightRotationInput > 0)
            {
                cameraPreset.newRotation *= Quaternion.Euler(Vector3.up * -cameraPreset.rotationAmount);
            }
            
            if (zoomInput > 0)
            {
                cameraPreset.newZoom -= cameraPreset.zoomAmount;
            }

            if (zoomInput < 0)
            {
                cameraPreset.newZoom += cameraPreset.zoomAmount;
            }
            
            transform.position = Vector3.Lerp(transform.position, cameraPreset.newPosition, Time.deltaTime * cameraPreset.movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPreset.newRotation, Time.deltaTime * cameraPreset.movementTime);
            movementCameraTransform.localPosition = Vector3.Lerp(movementCameraTransform.localPosition, cameraPreset.newZoom, Time.deltaTime * cameraPreset.movementTime);
        }

        private void HandleSnappingInput()
        {
            float snapAInput = playerControls.CameraActionMap.SnapSideA.ReadValue<float>();
            float snapBInput = playerControls.CameraActionMap.SnapSideB.ReadValue<float>();
            float snapCInput = playerControls.CameraActionMap.SnapSideC.ReadValue<float>();
            float snapDInput = playerControls.CameraActionMap.SnapSideD.ReadValue<float>();
            float snapEInput = playerControls.CameraActionMap.SnapSideE.ReadValue<float>();
            float snapFInput = playerControls.CameraActionMap.SnapSideF.ReadValue<float>();

            if (isSnapping) return;
            isSnapping = true;

            if (snapAInput > 0)
            {
                cameraPreset.newPosition = snappingPositions[0].Pos();
                cameraPreset.newRotation = snappingPositions[0].Rot();
            }

            if (snapBInput > 0)
            {
                cameraPreset.newPosition = snappingPositions[1].Pos();
                cameraPreset.newRotation = snappingPositions[1].Rot();
            }

            if (snapCInput > 0)
            {
                cameraPreset.newPosition = snappingPositions[2].Pos();
                cameraPreset.newRotation = snappingPositions[2].Rot();
            }

            if (snapDInput > 0)
            {
                cameraPreset.newPosition = snappingPositions[3].Pos();
                cameraPreset.newRotation = snappingPositions[3].Rot();
            }

            if (snapEInput > 0)
            {
                cameraPreset.newPosition = snappingPositions[4].Pos();
                cameraPreset.newRotation = snappingPositions[4].Rot();
            }

            if (snapFInput > 0)
            {
                cameraPreset.newPosition = snappingPositions[5].Pos();
                cameraPreset.newRotation = snappingPositions[5].Rot();
            }
            isSnapping = false;
        }
    }
}
