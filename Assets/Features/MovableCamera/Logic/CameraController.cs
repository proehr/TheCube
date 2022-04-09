using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.MovableCamera.Logic
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform movementCameraTransform;
        [SerializeField] private CameraPreset_SO cameraPreset;
        [SerializeField] private CameraDebugViewer_SO cameraDebugViewer;

        [SerializeField, ReadOnly] private CubeCorner_SO activeCorner;
        [SerializeField, ReadOnly] private CubeFace_SO activeFace;

        private CubeFace_SO startFace;
        private CubeCorner_SO startCorner;

        private TheCube playerControls;
        private CameraLocation cameraLocation;
        
        private Vector3 newPosition;
        private Vector3 startPosition;
        private Quaternion startRotation;
        private Vector3 newZoom;
        private Vector3 startZoom;

        private Vector2 currentMovementInputVector;
        private Vector2 smoothMovementInputVelocity;
        
        private float currentZoomInputValue;
        private float smoothZoomInputVelocity;

        private float movementSpeed;

        private bool camRunning;

        private Vector2 movementInput;
        private float shiftInput;
        private float leftRotationInput;
        private float rightRotationInput;
        private float zoomInput;
        
        private Vector2 mousePosition;

        public CubeCorner_SO ActiveCorner
        {
            get => activeCorner;
            set => activeCorner = value;
        }

        public CubeFace_SO ActiveFace
        {
            get => activeFace;
            set => activeFace = value;
        }

        private void Awake()
        {
            playerControls = new TheCube();
            cameraLocation = new CameraLocation(this, activeCorner, activeFace);

            Transform cameraRigTransform = transform;
            newPosition = cameraRigTransform.position;
            
            newZoom = movementCameraTransform.localPosition;
        }

        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            startZoom = movementCameraTransform.localPosition;
            startFace = activeFace;
            startCorner = activeCorner;

            cameraDebugViewer.startPosition = startPosition;
            cameraDebugViewer.startRotation = startRotation.eulerAngles;
            cameraDebugViewer.startZoom = startZoom;
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
            if (!CheckForMouseInScreen()) return;
            
            HandleKeyboardInput();
        }
        
        private void HandleKeyboardInput()
        {
            // Get Inputs
            movementInput = playerControls.Camera.PlanarMovement.ReadValue<Vector2>();
            shiftInput = playerControls.Camera.ShiftSpeed.ReadValue<float>();
            leftRotationInput = playerControls.Camera.RotateLeft.ReadValue<float>();
            rightRotationInput = playerControls.Camera.RotateRight.ReadValue<float>();
            zoomInput = playerControls.Camera.Zoom.ReadValue<float>();
            
            // if shift is pressed add extraSpeed
            movementSpeed = shiftInput > 0 ? cameraPreset.FastSpeed : cameraPreset.NormalSpeed;

            if (!camRunning)
            {
                // Planar Movement
                currentMovementInputVector = Vector2.SmoothDamp(currentMovementInputVector, movementInput, ref smoothMovementInputVelocity, cameraPreset.SmoothTime);
                Vector3 moveDir = new Vector3(currentMovementInputVector.x, 0, currentMovementInputVector.y);
                moveDir = Quaternion.Euler(transform.rotation.eulerAngles) * moveDir;
                
                newPosition += moveDir * movementSpeed * Time.deltaTime;
            
                //Zoom
                currentZoomInputValue = Mathf.SmoothDamp(currentZoomInputValue, zoomInput, ref smoothZoomInputVelocity, cameraPreset.SmoothTime);
                float zoomDir = -currentZoomInputValue;
                
                newZoom += zoomDir * cameraPreset.ZoomAmount * cameraPreset.ZoomSpeed * Time.deltaTime;
            
                // Clamp movement
                float distance = Vector3.Distance(newPosition, activeFace.Offset);

                if (distance > cameraPreset.ClampMovement)
                {
                    Vector3 fromOriginToObject = newPosition - activeFace.Offset;
                    fromOriginToObject *= cameraPreset.ClampMovement / distance;
                    newPosition = activeFace.Offset + fromOriginToObject;
                }
                
                // Clamp Zoom
                newZoom.y = Mathf.Clamp(newZoom.y, cameraPreset.ZoomYClamp.x,cameraPreset.ZoomYClamp.y);
                newZoom.z = Mathf.Clamp(newZoom.z, cameraPreset.ZoomZClamp.x, cameraPreset.ZoomZClamp.y);

                transform.position = newPosition;
                movementCameraTransform.localPosition = newZoom;
            }
            
            // assign Values for Debug
            cameraDebugViewer.newPosition = newPosition;
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
            
            Quaternion startR = transform.rotation;
            Vector3 startP = newPosition;
            Vector3 startZ = newZoom;
            
            StartCoroutine(Rotate(transform, startR, startRotation, cameraPreset.FaceRotationSpeed));
            StartCoroutine(Move(transform, startP, startPosition, cameraPreset.FaceRotationSpeed));
            StartCoroutine(MoveLocal(movementCameraTransform, startZ, startZoom, cameraPreset.FaceRotationSpeed));

            newPosition = startPosition;
            newZoom = startZoom;

            activeFace = startFace;
            activeCorner = startCorner;
            cameraLocation.ActiveFace = startFace;
            cameraLocation.ActiveCorner = startCorner;
        }

        public void OnFaceRotate(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (camRunning) return;

                cameraLocation.SetActiveFace(out Vector3 newOffset);
                
                Quaternion startQ = transform.rotation;
                Quaternion endQ = startQ * Quaternion.Euler(45, 45, -90);
                StartCoroutine(Rotate(transform, startQ, endQ, cameraPreset.FaceRotationSpeed));
                StartCoroutine(Move(transform, newPosition, newOffset, cameraPreset.FaceRotationSpeed));
                newPosition = newOffset;
            }
        }

        public void OnRotateLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (camRunning) return;
                
                cameraLocation.SetActiveCornerOnLeftRotation();
                
                Quaternion startQ = transform.rotation;
                Quaternion endQ = startQ * Quaternion.Euler(0, 90,0);
                
                StartCoroutine(Rotate(transform, startQ, endQ, cameraPreset.PlanarRotationSpeed));
                StartCoroutine(MoveLocal(transform, newPosition, activeFace.Offset, cameraPreset.PlanarRotationSpeed));
                newPosition = activeFace.Offset;
            }
        }

        public void OnRotateRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (camRunning) return;
                
                cameraLocation.SetActiveCornerOnRightRotation();
                
                Quaternion startQ = transform.rotation;
                Quaternion endQ = startQ * Quaternion.Euler(0, -90, 0);
                
                StartCoroutine(Rotate(transform, startQ, endQ, cameraPreset.PlanarRotationSpeed));
                StartCoroutine(Move(transform, newPosition, activeFace.Offset, cameraPreset.PlanarRotationSpeed));
                newPosition = activeFace.Offset;
            }
        }

        private IEnumerator Rotate(Transform transform, Quaternion start, Quaternion end, float t)
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
        
        private IEnumerator Move(Transform transform, Vector3 start, Vector3 end, float t)
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
        
        private IEnumerator MoveLocal(Transform transform, Vector3 start, Vector3 end, float t)
        {
            camRunning = true;
            float time = 0;
            
            while (time < t)
            {
                transform.localPosition = Vector3.Lerp(start, end, time / t);
                time += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = end;
            camRunning = false;
        }
        
        private bool CheckForMouseInScreen()
        {
            mousePosition = playerControls.Camera.MousePosition.ReadValue<Vector2>();

            return !(mousePosition.y > Screen.height) 
                   && !(mousePosition.y < 0) 
                   && !(mousePosition.x > Screen.width) 
                   && !(mousePosition.x < 0);
        }
    }
}
