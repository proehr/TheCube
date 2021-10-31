using System;
using DataStructures.RuntimeSet;
using DataStructures.Variables;
using Features.Gui;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandInputHandler : MonoBehaviour
{
    [SerializeField] private CommandModeVariable commandMode;
    [SerializeField] private MouseCursorHandler_SO mouseCursorHandler;
    [SerializeField] private string cubeTag;

    private bool selectorInputDown;
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
        var camera = Camera.current;
        if (!camera) return;
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit))
        {
            var targetObject = hit.transform.gameObject;
            if (targetObject.CompareTag(cubeTag))
            {
                if (this.mouseCursorHandler)
                {
                    this.mouseCursorHandler.SetCursor(MouseCursorLook.Excavate);
                }
                if (this.hoveredObject)
                {
                    if (this.hoveredObject != targetObject)
                    {
                        this.hoveredObject.SendMessage(this.cubeRemoveHighlightMethod);
                        targetObject.SendMessage(this.cubeHighlightMethodName);
                        this.hoveredObject = targetObject;
                    }
                }
                else
                {
                    targetObject.SendMessage(this.cubeHighlightMethodName);
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
        if (context.started)
        {
            this.selectorInputDown = true;
            if (!this.hoveredObject) return;
            this.hoveredObject.SendMessage(this.cubeSelectMethodName, true);
            
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
}
