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
                    ""name"": ""PlanarMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b7395fbe-991b-4397-ae2a-c0a1d7a18ca0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftRotationMovement"",
                    ""type"": ""Button"",
                    ""id"": ""fa374db7-576c-4ffc-8ee7-60a38775e35d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RigthRotationMovement"",
                    ""type"": ""Button"",
                    ""id"": ""93a600fd-1da6-4a1f-b4be-b08463276b58"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": ""ShiftSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""657ff4af-15ed-43f9-8b7c-303a5bfbfc0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SnapSideA"",
                    ""type"": ""Button"",
                    ""id"": ""4735328c-c157-442e-9050-6e4121c4031f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SnapSideB"",
                    ""type"": ""Button"",
                    ""id"": ""099c716f-6a12-42e7-ad70-50067bf60365"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SnapSideC"",
                    ""type"": ""Button"",
                    ""id"": ""5f0929d4-5b48-405e-88ca-76145971c00a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SnapSideD"",
                    ""type"": ""Button"",
                    ""id"": ""0da709b3-64e2-442d-8edf-716726ce2384"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SnapSideE"",
                    ""type"": ""Button"",
                    ""id"": ""c6b8efa9-65dc-4224-ba05-b6204205d86a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SnapSideF"",
                    ""type"": ""Button"",
                    ""id"": ""8bc8f4df-e265-4f5f-888a-48f69ce43f56"",
                    ""expectedControlType"": ""Button"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                    ""action"": ""PlanarMovement"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""f2297b3e-a72e-41ac-9fde-fe453218555d"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9ce431f-0de7-43ec-812e-b1a94360d373"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RigthRotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f057062-440c-45a6-86c5-fe651eb51a25"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftRotationMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57bc4bc9-e862-4f05-b941-480b26c6d231"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapSideA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d46adb9-31c6-4080-9527-902d97fcc2f5"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapSideB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06706887-0fcd-4e20-b274-884adc6b0c7c"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapSideC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef77fddf-ad38-4eee-8085-fa348e24bf69"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapSideD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e31e9f76-2a54-44f8-b5c3-628987f40f90"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapSideE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""542f3569-414e-445b-9c98-eb313b9fd225"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapSideF"",
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
            m_CameraActionMap_PlanarMovement = m_CameraActionMap.FindAction("PlanarMovement", throwIfNotFound: true);
            m_CameraActionMap_LeftRotationMovement = m_CameraActionMap.FindAction("LeftRotationMovement", throwIfNotFound: true);
            m_CameraActionMap_RigthRotationMovement = m_CameraActionMap.FindAction("RigthRotationMovement", throwIfNotFound: true);
            m_CameraActionMap_Zoom = m_CameraActionMap.FindAction("Zoom", throwIfNotFound: true);
            m_CameraActionMap_ShiftSpeed = m_CameraActionMap.FindAction("ShiftSpeed", throwIfNotFound: true);
            m_CameraActionMap_SnapSideA = m_CameraActionMap.FindAction("SnapSideA", throwIfNotFound: true);
            m_CameraActionMap_SnapSideB = m_CameraActionMap.FindAction("SnapSideB", throwIfNotFound: true);
            m_CameraActionMap_SnapSideC = m_CameraActionMap.FindAction("SnapSideC", throwIfNotFound: true);
            m_CameraActionMap_SnapSideD = m_CameraActionMap.FindAction("SnapSideD", throwIfNotFound: true);
            m_CameraActionMap_SnapSideE = m_CameraActionMap.FindAction("SnapSideE", throwIfNotFound: true);
            m_CameraActionMap_SnapSideF = m_CameraActionMap.FindAction("SnapSideF", throwIfNotFound: true);
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
        private readonly InputAction m_CameraActionMap_PlanarMovement;
        private readonly InputAction m_CameraActionMap_LeftRotationMovement;
        private readonly InputAction m_CameraActionMap_RigthRotationMovement;
        private readonly InputAction m_CameraActionMap_Zoom;
        private readonly InputAction m_CameraActionMap_ShiftSpeed;
        private readonly InputAction m_CameraActionMap_SnapSideA;
        private readonly InputAction m_CameraActionMap_SnapSideB;
        private readonly InputAction m_CameraActionMap_SnapSideC;
        private readonly InputAction m_CameraActionMap_SnapSideD;
        private readonly InputAction m_CameraActionMap_SnapSideE;
        private readonly InputAction m_CameraActionMap_SnapSideF;
        public struct CameraActionMapActions
        {
            private @PlayerControls m_Wrapper;
            public CameraActionMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @DragMovement => m_Wrapper.m_CameraActionMap_DragMovement;
            public InputAction @ResetCamera => m_Wrapper.m_CameraActionMap_ResetCamera;
            public InputAction @MousePosition => m_Wrapper.m_CameraActionMap_MousePosition;
            public InputAction @PlanarMovement => m_Wrapper.m_CameraActionMap_PlanarMovement;
            public InputAction @LeftRotationMovement => m_Wrapper.m_CameraActionMap_LeftRotationMovement;
            public InputAction @RigthRotationMovement => m_Wrapper.m_CameraActionMap_RigthRotationMovement;
            public InputAction @Zoom => m_Wrapper.m_CameraActionMap_Zoom;
            public InputAction @ShiftSpeed => m_Wrapper.m_CameraActionMap_ShiftSpeed;
            public InputAction @SnapSideA => m_Wrapper.m_CameraActionMap_SnapSideA;
            public InputAction @SnapSideB => m_Wrapper.m_CameraActionMap_SnapSideB;
            public InputAction @SnapSideC => m_Wrapper.m_CameraActionMap_SnapSideC;
            public InputAction @SnapSideD => m_Wrapper.m_CameraActionMap_SnapSideD;
            public InputAction @SnapSideE => m_Wrapper.m_CameraActionMap_SnapSideE;
            public InputAction @SnapSideF => m_Wrapper.m_CameraActionMap_SnapSideF;
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
                    @PlanarMovement.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnPlanarMovement;
                    @PlanarMovement.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnPlanarMovement;
                    @PlanarMovement.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnPlanarMovement;
                    @LeftRotationMovement.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnLeftRotationMovement;
                    @LeftRotationMovement.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnLeftRotationMovement;
                    @LeftRotationMovement.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnLeftRotationMovement;
                    @RigthRotationMovement.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnRigthRotationMovement;
                    @RigthRotationMovement.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnRigthRotationMovement;
                    @RigthRotationMovement.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnRigthRotationMovement;
                    @Zoom.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnZoom;
                    @ShiftSpeed.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnShiftSpeed;
                    @ShiftSpeed.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnShiftSpeed;
                    @ShiftSpeed.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnShiftSpeed;
                    @SnapSideA.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideA;
                    @SnapSideA.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideA;
                    @SnapSideA.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideA;
                    @SnapSideB.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideB;
                    @SnapSideB.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideB;
                    @SnapSideB.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideB;
                    @SnapSideC.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideC;
                    @SnapSideC.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideC;
                    @SnapSideC.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideC;
                    @SnapSideD.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideD;
                    @SnapSideD.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideD;
                    @SnapSideD.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideD;
                    @SnapSideE.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideE;
                    @SnapSideE.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideE;
                    @SnapSideE.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideE;
                    @SnapSideF.started -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideF;
                    @SnapSideF.performed -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideF;
                    @SnapSideF.canceled -= m_Wrapper.m_CameraActionMapActionsCallbackInterface.OnSnapSideF;
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
                    @PlanarMovement.started += instance.OnPlanarMovement;
                    @PlanarMovement.performed += instance.OnPlanarMovement;
                    @PlanarMovement.canceled += instance.OnPlanarMovement;
                    @LeftRotationMovement.started += instance.OnLeftRotationMovement;
                    @LeftRotationMovement.performed += instance.OnLeftRotationMovement;
                    @LeftRotationMovement.canceled += instance.OnLeftRotationMovement;
                    @RigthRotationMovement.started += instance.OnRigthRotationMovement;
                    @RigthRotationMovement.performed += instance.OnRigthRotationMovement;
                    @RigthRotationMovement.canceled += instance.OnRigthRotationMovement;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @ShiftSpeed.started += instance.OnShiftSpeed;
                    @ShiftSpeed.performed += instance.OnShiftSpeed;
                    @ShiftSpeed.canceled += instance.OnShiftSpeed;
                    @SnapSideA.started += instance.OnSnapSideA;
                    @SnapSideA.performed += instance.OnSnapSideA;
                    @SnapSideA.canceled += instance.OnSnapSideA;
                    @SnapSideB.started += instance.OnSnapSideB;
                    @SnapSideB.performed += instance.OnSnapSideB;
                    @SnapSideB.canceled += instance.OnSnapSideB;
                    @SnapSideC.started += instance.OnSnapSideC;
                    @SnapSideC.performed += instance.OnSnapSideC;
                    @SnapSideC.canceled += instance.OnSnapSideC;
                    @SnapSideD.started += instance.OnSnapSideD;
                    @SnapSideD.performed += instance.OnSnapSideD;
                    @SnapSideD.canceled += instance.OnSnapSideD;
                    @SnapSideE.started += instance.OnSnapSideE;
                    @SnapSideE.performed += instance.OnSnapSideE;
                    @SnapSideE.canceled += instance.OnSnapSideE;
                    @SnapSideF.started += instance.OnSnapSideF;
                    @SnapSideF.performed += instance.OnSnapSideF;
                    @SnapSideF.canceled += instance.OnSnapSideF;
                }
            }
        }
        public CameraActionMapActions @CameraActionMap => new CameraActionMapActions(this);
        public interface ICameraActionMapActions
        {
            void OnDragMovement(InputAction.CallbackContext context);
            void OnResetCamera(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnPlanarMovement(InputAction.CallbackContext context);
            void OnLeftRotationMovement(InputAction.CallbackContext context);
            void OnRigthRotationMovement(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnShiftSpeed(InputAction.CallbackContext context);
            void OnSnapSideA(InputAction.CallbackContext context);
            void OnSnapSideB(InputAction.CallbackContext context);
            void OnSnapSideC(InputAction.CallbackContext context);
            void OnSnapSideD(InputAction.CallbackContext context);
            void OnSnapSideE(InputAction.CallbackContext context);
            void OnSnapSideF(InputAction.CallbackContext context);
        }
    }
}
