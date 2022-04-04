using System;
using System.Collections;
using System.Collections.Generic;
using Features.Input;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private Vector3 startPosition;
        private Quaternion newRotation;
        private Quaternion startRotation;
        private Vector3 newZoom;

        private float movementSpeed;

        private bool camRunning;

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

        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
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

            // if (leftRotationInput > 0)
            // {
            //     newRotation *= Quaternion.Euler(Vector3.up * cameraPreset.rotationAmount);
            // }
            //
            // if (rightRotationInput > 0)
            // {
            //     newRotation *= Quaternion.Euler(Vector3.up * -cameraPreset.rotationAmount);
            // }
            
            if (zoomInput > 0)
            {
                newZoom -= cameraPreset.zoomAmount;
            }

            if (zoomInput < 0)
            {
                newZoom += cameraPreset.zoomAmount;
            }
            
            // Clamp movement
            float distance = Vector3.Distance(newPosition, Vector3.zero);

            if (distance > cameraPreset.clampMovement)
            {
                Vector3 fromOriginToObject = newPosition - Vector3.zero;
                fromOriginToObject *= cameraPreset.clampMovement / distance;
                newPosition = Vector3.zero + fromOriginToObject;
            }
            
            // Clamp Zoom
            newZoom.y = Mathf.Clamp(newZoom.y, cameraPreset.zoomYClamp.x,cameraPreset.zoomYClamp.y);
            newZoom.z = Mathf.Clamp(newZoom.z, cameraPreset.zoomZClamp.x, cameraPreset.zoomZClamp.y);

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * cameraPreset.movementTime);
            // transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * cameraPreset.movementTime);
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

        public void OnResetCamera(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ResetCamera();
            }
        }

        public void ResetCamera()
        {
            if (camRunning) return;
            
            Quaternion startQ = transform.rotation;
            
            StartCoroutine(Rotate(startQ, startRotation, cameraPreset.faceRotationSpeed));
            newPosition = startPosition;
        }

        public void OnRotateLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (camRunning) return;
                
                Quaternion startQ = transform.rotation;
                Quaternion endQ = startQ * Quaternion.Euler(0, 90,0);

                Vector3 startVec = transform.position;
                Vector3 endVec = Vector3.zero;

                if (startVec.x >= 0 && startVec.z <= 0)
                {
                    endVec = new Vector3(-startVec.x, startVec.y, startVec.z);
                }
                else if (startVec.x <= 0 && startVec.z <= 0)
                {
                    endVec = new Vector3(startVec.x, startVec.y, -startVec.z);
                }
                else if (startVec.x <= 0 && startVec.z >= 0)
                {
                    endVec = new Vector3(-startVec.x, startVec.y, startVec.z);
                }
                else if (startVec.x >= 0 && startVec.z >= 0)
                {
                    endVec = new Vector3(startVec.x, startVec.y, -startVec.z);
                }
                
                StartCoroutine(Rotate(startQ, endQ, cameraPreset.planarRotationSpeed));
            }
        }

        public void OnRotateRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (camRunning) return;
                
                Quaternion startQ = transform.rotation;
                Quaternion endQ = startQ * Quaternion.Euler(0, -90, 0);

                Vector3 startVec = transform.position;
                Vector3 endVec = Vector3.zero;

                if (startVec.x >= 0 && startVec.z <= 0)
                {
                    endVec = new Vector3(startVec.x, startVec.y, -startVec.z);
                }
                else if (startVec.x <= 0 && startVec.z <= 0)
                {
                    endVec = new Vector3(-startVec.x, startVec.y, startVec.z);
                }
                else if (startVec.x <= 0 && startVec.z >= 0)
                {
                    endVec = new Vector3(startVec.x, startVec.y, -startVec.z);
                }
                else if (startVec.x >= 0 && startVec.z >= 0)
                {
                    endVec = new Vector3(-startVec.x, startVec.y, startVec.z);
                }
                
                StartCoroutine(Rotate(startQ, endQ, cameraPreset.planarRotationSpeed));
            }
        }

        public void OnFaceRotate(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (camRunning) return;
                
                Quaternion start = transform.rotation;
                Quaternion end = start * Quaternion.Euler(45, 45, -90);
                StartCoroutine(Rotate(start, end, cameraPreset.faceRotationSpeed));
            }
        }

        private IEnumerator Rotate(Quaternion start, Quaternion end, float t)
        {
            camRunning = true;
            float time = 0;
            
            while (time < t)
            {
                transform.rotation = Quaternion.Lerp(start, end, time / t);
                time += Time.deltaTime;
                yield return null;
            }
            transform.rotation = end;
            camRunning = false;
        }
        
        private IEnumerator Move(Vector3 start, Vector3 end, float t)
        {
            camRunning = true;
            float time = 0;
            
            while (time < t)
            {
                transform.position = Vector3.Lerp(start, end, time / t);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = end;
            camRunning = false;
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
                transform.rotation = snappingPositions[0].Rotation();
            }

            if (snapBInput > 0)
            {
                newPosition = snappingPositions[1].Position();
                transform.rotation = snappingPositions[1].Rotation();
            }

            if (snapCInput > 0)
            {
                newPosition = snappingPositions[2].Position();
                transform.rotation = snappingPositions[2].Rotation();
            }

            if (snapDInput > 0)
            {
                newPosition = snappingPositions[3].Position();
                transform.rotation = snappingPositions[3].Rotation();
            }

            if (snapEInput > 0)
            {
                newPosition = snappingPositions[4].Position();
                transform.rotation = snappingPositions[4].Rotation();
            }

            if (snapFInput > 0)
            {
                newPosition = snappingPositions[5].Position();
                transform.rotation = snappingPositions[5].Rotation();
            }
        }
    }
}
