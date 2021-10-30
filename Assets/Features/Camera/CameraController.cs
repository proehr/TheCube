using Features.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraPreset_SO cameraPreset;
        [SerializeField] private Transform movementCameraTransform;
        [SerializeField] private GameObject dragPlane;

        private UnityEngine.Camera movementCamera;
        
        private PlayerControls playerControlsActionMaps;

        private bool isDragged;
        private void Awake()
        {
            playerControlsActionMaps = new PlayerControls();
            
            movementCamera = UnityEngine.Camera.main;

            Transform cameraRigTransform = transform;
            cameraPreset.newPosition = cameraRigTransform.position;
            cameraPreset.newRotation = cameraRigTransform.rotation;
            cameraPreset.newZoom = movementCameraTransform.localPosition;
            
            playerControlsActionMaps.CameraActionMap.ResetCamera.performed += _ => ResetCamera();
        }
        
        public void StartDrag(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Vector2 mousePosition = playerControlsActionMaps.CameraActionMap.MousePosition.ReadValue<Vector2>();

                cameraPreset.clickMousePosition = mousePosition;
                
                mousePosition.x /= 100;
                mousePosition.y /= 100;
                
                cameraPreset.dragStartPosition = mousePosition;
            
                // Plane plane = new Plane(Vector3.forward, Vector3.zero);
                //
                // Ray ray = movementCamera.ScreenPointToRay(mousePosition);
                // // Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green, 2f);
                //
                // if (plane.Raycast(ray, out var entry))
                // {
                //     cameraPreset.dragStartPosition = ray.GetPoint(entry);
                // }
            
                isDragged = true;
            }
            
            if (context.canceled)
            {
                isDragged = false;
            }
        }

        private void OnEnable()
        {
            playerControlsActionMaps.Enable();
        }

        private void OnDisable()
        {
            playerControlsActionMaps.Disable();
        }

        private void Update()
        {
            HandleMouseInput();
            HandleMovementInput();
        }

        private void ResetCamera()
        {
            cameraPreset.ResetValues();
        }

        private void HandleMouseInput()
        {
            // Zooming
            float zoomInput = playerControlsActionMaps.CameraActionMap.Zoom.ReadValue<float>();

            cameraPreset.zoomInput = zoomInput;

            if (zoomInput > 0)
            {
                cameraPreset.newZoom += cameraPreset.zoomSpeed;
            }

            if (zoomInput < 0)
            {
                cameraPreset.newZoom -= cameraPreset.zoomSpeed;
            }
            
            // Apply movement change to move Camera
            movementCameraTransform.localPosition = Vector3.Lerp(movementCameraTransform.localPosition, cameraPreset.newZoom,
                Time.deltaTime * cameraPreset.movementTime);
            
            // Dragging part two
            
            
            if (isDragged)
            {
                Vector2 mousePosition = playerControlsActionMaps.CameraActionMap.MousePosition.ReadValue<Vector2>();

                cameraPreset.mousePosition = mousePosition;
                
                mousePosition.x /= 100;
                mousePosition.y /= 100;
                
                cameraPreset.dragCurrentPosition = mousePosition;
                
                cameraPreset.dragPositionOffset = cameraPreset.dragStartPosition - cameraPreset.dragCurrentPosition;
                
                cameraPreset.newPosition = transform.position + cameraPreset.dragPositionOffset;
                
                // Plane plane = new Plane(Vector3.forward, Vector3.zero);
                //
                // Ray ray = movementCamera.ScreenPointToRay(mousePosition);
                // // Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 0.5f);
                //
                // if (plane.Raycast(ray, out var entry))
                // {
                //     cameraPreset.dragCurrentPosition = ray.GetPoint(entry);
                //
                //     cameraPreset.dragPositionOffset = cameraPreset.dragStartPosition - cameraPreset.dragCurrentPosition;
                //     Debug.Log("maous wird nicht bewegt");
                //
                //     cameraPreset.newPosition = transform.position + cameraPreset.dragPositionOffset;
                // }
            }
            
            // Apply movement change to move Camera
            transform.position = Vector3.Lerp(transform.position, cameraPreset.newPosition, Time.deltaTime * cameraPreset.movementTime);
        }

        private void HandleMovementInput()
        {
            // Rotating
            Vector2 movementInput = playerControlsActionMaps.CameraActionMap.RotationMovement.ReadValue<Vector2>();

            cameraPreset.movementInput = movementInput;

            if (movementInput.x < 0)
            {
                cameraPreset.newRotation *= Quaternion.Euler(Vector3.up * cameraPreset.rotationSpeed);
            }

            if (movementInput.x > 0)
            {
                cameraPreset.newRotation *= Quaternion.Euler(Vector3.up * -cameraPreset.rotationSpeed);
            }

            if (movementInput.y < 0)
            {
                cameraPreset.newRotation *= Quaternion.Euler(Vector3.right * cameraPreset.rotationSpeed);
            }

            if (movementInput.y > 0)
            {
                cameraPreset.newRotation *= Quaternion.Euler(Vector3.right * -cameraPreset.rotationSpeed);
            }
            
            // Apply movement change to move Camera
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraPreset.newRotation,
                Time.deltaTime * cameraPreset.movementTime);
        }
    }
}
