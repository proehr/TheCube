// GENERATED AUTOMATICALLY FROM 'Assets/Features/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Features.Input
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""CameraActionMap"",
            ""id"": ""d0b334d9-6f5a-45e1-b0b2-21dee834994f"",
            ""actions"": [
                {
                    ""name"": ""DragMovement"",
                    ""type"": ""Button"",
                    ""id"": ""4ab44b82-f8a5-44d9-b8e9-70ff9ffbf1bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetCamera"",
                    ""type"": ""Button"",
                    ""id"": ""b6ec6571-e5cc-485e-a737-532c5fe3f3db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""ed156b36-ecbf-42fc-bba4-a1737ddccd2c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotationMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b7395fbe-991b-4397-ae2a-c0a1d7a18ca0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""18c181a1-1af3-4871-9a8a-e0bb448b6947"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""afdbe713-4162-47aa-ac91-661775da3e21"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DragMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28cd0ccf-5e0c-4cd3-bd11-af76a56897b4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5723f055-4b19-4310-8a79-39881c902d67"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1cc8cd0-41b0-4c20-beba-09614cb8442d"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""7a05dc49-c2e9-4617-9b3d-5d31145ce831"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6c71d741-69d2-4bbd-8623-8b071b0413c3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6d9c8d98-60f1-4c84-b8a8-77b5a171e8d5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c8d5f4b1-b9a8-4dc3-9bc9-4ba596b1f207"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""52a88f85-fb79-4671-a54f-fd301e4babda"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""f0e05850-a492-4607-a616-38d842543145"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e70f4a95-e4df-467a-8fd8-e372ba693cbc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5818069f-0ce1-48af-87da-ef38334c7ce0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fdbc88cf-a7b1-442c-8ccf-d1e26cda70ef"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c1df8bac-b9ae-48de-88b0-64f51374ad0f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""QE"",
                    ""id"": ""97c47856-09d0-42e4-bf92-69bf8f3a69d5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""021da61b-4149-49b9-a08b-4ff8bf1fd247"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a3cf5411-6453-41eb-bfd6-89a286c2754f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0e1beba5-06b4-46bb-96a1-017f08dace4e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5afe4849-b40a-4244-8e3c-5567298cf9f0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a84cf04c-24c9-426b-9c75-32061407b8a4"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // CameraActionMap
            m_CameraActionMap = asset.FindActionMap("CameraActionMap", throwIfNotFound: true);
            m_CameraActionMap_DragMovement = m_CameraActionMap.FindAction("DragMovement", throwIfNotFound: true);
            m_CameraActionMap_ResetCamera = m_CameraActionMap.FindAction("ResetCamera", throwIfNotFound: true);
            m_CameraActionMap_MousePosition = m_CameraActionMap.FindAction("MousePosition", throwIfNotFound: true);
            m_CameraActionMap_RotationMovement = m_CameraActionMap.FindAction("RotationMovement", throwIfNotFound: true);
            m_CameraActionMap_Zoom = m_CameraActionMap.FindAction("Zoom", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // CameraActionMap
        private readonly InputActionMap m_CameraActionMap;
        private ICameraActionMapActions m_CameraActionMapActionsCallbackInterface;
        private readonly InputAction m_CameraActionMap_DragMovement;
        private readonly InputAction m_CameraActionMap_ResetCamera;
        private readonly InputAction m_CameraActionMap_MousePosition;
        private readonly InputAction m_CameraActionMap_RotationMovement;
        private readonly InputAction m_CameraActionMap_Zoom;
        public struct CameraActionMapActions
        {
            private @PlayerControls m_Wrapper;
            public CameraActionMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @DragMovement => m_Wrapper.m_CameraActionMap_DragMovement;
            public InputAction @ResetCamera => m_Wrapper.m_CameraActionMap_ResetCamera;
            public InputAction @MousePosition => m_Wrapper.m_CameraActionMap_MousePosition;
            public InputAction @RotationMovement => m_Wrapper.m_CameraActionMap_RotationMovement;
            public InputAction @Zoom => m_Wrapper.m_CameraActionMap_Zoom;
            public InputActionMap Get() { return m_Wrapper.m_CameraActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraActionMapActions set) { return set.Get(); }
            public void SetCallbacks(ICameraActionMapActions instance)
            {
                if (m_Wrapper.m_CameraActionMapActionsCallbackInterface != null)
                {
                    @DragMovement.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnDragMovement;
                    @DragMovement.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnDragMovement;
                    @DragMovement.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnDragMovement;
                    @ResetCamera.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnResetCamera;
                    @ResetCamera.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnResetCamera;
                    @ResetCamera.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnResetCamera;
                    @MousePosition.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnMousePosition;
                    @RotationMovement.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnRotationMovement;
                    @RotationMovement.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnRotationMovement;
                    @RotationMovement.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnRotationMovement;
                    @Zoom.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnZoom;
                }
                m_Wrapper.m_CameraActionMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @DragMovement.started += instance.OnDragMovement;
                    @DragMovement.performed += instance.OnDragMovement;
                    @DragMovement.canceled += instance.OnDragMovement;
                    @ResetCamera.started += instance.OnResetCamera;
                    @ResetCamera.performed += instance.OnResetCamera;
                    @ResetCamera.canceled += instance.OnResetCamera;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                    @RotationMovement.started += instance.OnRotationMovement;
                    @RotationMovement.performed += instance.OnRotationMovement;
                    @RotationMovement.canceled += instance.OnRotationMovement;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                }
            }
        }
        public CameraActionMapActions @CameraActionMap => new CameraActionMapActions(this);
        public interface ICameraActionMapActions
        {
            void OnDragMovement(InputAction.CallbackContext context);
            void OnResetCamera(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnRotationMovement(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
        }
    }
}
