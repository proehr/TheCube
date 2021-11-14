using System;
using DataStructures.Variables;
using Features.Commands.Scripts.ActionEvents;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [Flags]
    public enum CubeState
    {
        Default,
        Hovered,
        MarkedForExcavation
    }
    
    private CubeState state;
    public CubeState cubeState => this.state;

    [SerializeField] private WorkerCommandActionEvent workerCommandActionEvent;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private ColorVariable defaultMaterialColor;
    [SerializeField] private ColorVariable highlightMaterialColor;
    [SerializeField] private ColorVariable excavateMaterialColor;

    public bool isMarkedForExcavation => this.state == CubeState.MarkedForExcavation;

    private void Awake()
    {
        if (this.renderer == null)
        {
            this.renderer = GetComponent<MeshRenderer>();
        }
    }

    private void OnHover()
    {
        if (this.state.HasFlag(CubeState.Hovered) || !renderer) return;
        SetMaterialColor(this.highlightMaterialColor);
        this.state |= CubeState.Hovered;
    }
    
    private void OnHoverEnd()
    {
        if (!this.state.HasFlag(CubeState.Hovered) || !this.renderer) return;
        if (this.state.HasFlag(CubeState.MarkedForExcavation))
        {
            SetMaterialColor(this.excavateMaterialColor);
        }
        else
        {
            SetMaterialColor(this.defaultMaterialColor);
        }

        this.state &= ~CubeState.Hovered;
    }
    
    private void OnSelect()
    {
        if (this.state.HasFlag(CubeState.MarkedForExcavation))
        {
            RemoveState(CubeState.MarkedForExcavation);
        }
        else
        {
            AddState(CubeState.MarkedForExcavation);
        }
    }

    private void SetMaterialColor(ColorVariable colorVariable)
    {
        this.renderer.material.color = colorVariable.Get();
    }
    
    private void AddState(CubeState newCubeState)
    {
        if (this.state.HasFlag(newCubeState)) return;
        this.state |= newCubeState;
        this.OnStateChange();
    }
    
    private void RemoveState(CubeState newCubeState)
    {
        if (!this.state.HasFlag(newCubeState)) return;
        this.state &= ~newCubeState;
        this.OnStateChange();
    }

    private void OnStateChange()
    {
        if (this.workerCommandActionEvent == null) return;
        this.workerCommandActionEvent.Raise(this);
    }
}
