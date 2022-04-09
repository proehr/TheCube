using System;
using DataStructures.Variables;
using Features.Commands.Scripts;
using Features.Gui;
using Features.LandingPod.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CommandInputHandler : MonoBehaviour
{
    [SerializeField] private CommandModeVariable commandMode;
    [SerializeField] private MouseCursorHandler_SO mouseCursorHandler;
    [SerializeField] private string cubeTag;
    [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;

    private Camera cachedCamera;
    private bool selectorInputDown;
    private bool excavationMode;
    private GameObject hoveredObject;
    private string cubeRemoveHighlightMethod = "OnHoverEnd";
    private string cubeHighlightMethodName = "OnHover";
    private string cubeSelectMethodName = "OnSelect";

    private void Awake()
    {
        this.commandMode.Set(CommandMode.Excavate);
    }

    private void OnDisable()
    {
        RemoveCurrentHighlight();
        selectorInputDown = false;
    }

    private void LateUpdate()
    {
        switch (this.commandMode.Get())
        {
            case CommandMode.Excavate:
                HandleExcavationInput();
                break;
            case CommandMode.TransportLine:
                RemoveCurrentHighlight();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleExcavationInput()
    {
        if (!this.cachedCamera)
        {
            this.cachedCamera = Camera.current;
        }
        if (!this.cachedCamera
            || Mouse.current == null)
            return;
        if (this.mouseCursorHandler && EventSystem.current && EventSystem.current.IsPointerOverGameObject())
        {
            if (this.mouseCursorHandler)
            {
                this.mouseCursorHandler.SetCursor(MouseCursorLook.Default);
            }
            RemoveCurrentHighlight();
            return;
        }
        Ray ray = this.cachedCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            var targetObject = hit.transform.gameObject;
            if (targetObject.CompareTag(cubeTag))
            {
                if (this.mouseCursorHandler)
                {
                    this.mouseCursorHandler.SetCursor(MouseCursorLook.Excavate);
                }
                if (this.hoveredObject && this.hoveredObject != targetObject)
                {
                    this.hoveredObject.SendMessage(this.cubeRemoveHighlightMethod);
                }
                if (!this.hoveredObject || this.hoveredObject != targetObject)
                {
                    if (this.selectorInputDown)
                    {
                        targetObject.SendMessage(this.cubeSelectMethodName, new SelectInfo(excavationMode, hit));
                    }
                    else
                    {
                        targetObject.SendMessage(this.cubeHighlightMethodName);
                    }
                    this.hoveredObject = targetObject;
                }

                return;
            }
        }
        RemoveCurrentHighlight();
        if (this.mouseCursorHandler)
        {
            this.mouseCursorHandler.SetCursor(MouseCursorLook.Default);
        }
    }

    private void RemoveCurrentHighlight()
    {
        if (!this.hoveredObject) return;
        this.hoveredObject.SendMessage(this.cubeRemoveHighlightMethod); // , SendMessageOptions.DontRequireReceiver
        this.hoveredObject = null;
    }

    public void OnIssueCommand(InputAction.CallbackContext context)
    {
        if (this.mouseCursorHandler && EventSystem.current && EventSystem.current.IsPointerOverGameObject())
        {
            if (this.mouseCursorHandler)
            {
                this.mouseCursorHandler.SetCursor(MouseCursorLook.Default);
            }
            return;
        }
        if (context.started)
        {
            this.selectorInputDown = true;
            if (!this.hoveredObject) return;
            excavationMode = HoverState.currentHoverState != HoverState.State.CubeExcavate;
            Ray ray = this.cachedCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit))
            {
                this.hoveredObject.SendMessage(this.cubeSelectMethodName, new SelectInfo(excavationMode, hit));
            }
            
        }
        if (context.canceled)
        {
            this.selectorInputDown = false;
        }
    }
    
    public void OnCommandModeExcavate(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (!commandMode) return;
            this.commandMode.Set(CommandMode.Excavate);
        }
    }
    
    public void OnCommandModeTransport(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (!commandMode) return;
            this.commandMode.Set(CommandMode.TransportLine);
            this.mouseCursorHandler.SetCursor(MouseCursorLook.Default);
        }
    }

    public void OnCommandLaunch(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onLaunchTriggered.Raise(new LaunchInformation());
        }
    }
}
