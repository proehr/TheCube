using System;
using System.Diagnostics;
using DataStructures.Variables;
using Features.Commands.Scripts.ActionEvents;
using Features.Planet_Generation.Scripts;
using UnityEngine;

namespace Features.Planet.Resources.Scripts
{
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
        [SerializeField] private new MeshRenderer renderer;
        [SerializeField] private ColorVariable defaultMaterialColor;
        [SerializeField] private ColorVariable highlightMaterialColor;
        [SerializeField] private ColorVariable excavateMaterialColor;

        public bool isMarkedForExcavation => this.state.HasFlag(CubeState.MarkedForExcavation);
        public Resource_SO resourceData { get; private set; }

        public void Init(Resource_SO resourceData)
        {
            this.resourceData = resourceData;
        }

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
            this.state |= CubeState.Hovered;
            UpdateMaterial();
        }

        private void OnHoverEnd()
        {
            if (!this.state.HasFlag(CubeState.Hovered) || !this.renderer) return;
            this.state &= ~CubeState.Hovered;
            UpdateMaterial();
        }

        private void OnSelect(bool bExcavate)
        {
            if (!this.isMarkedForExcavation && bExcavate)
            {
                AddState(CubeState.MarkedForExcavation);
            }
            else if(!bExcavate)
            {
                RemoveState(CubeState.MarkedForExcavation);
            }
        }

        private void UpdateMaterial()
        {
            switch (state)
            {
                case CubeState.Default:
                    SetMaterialColor(this.defaultMaterialColor);
                    break;
                case CubeState.Hovered:
                    HoverState.SetState(HoverState.State.Cube);
                    SetMaterialColor(this.highlightMaterialColor);
                    break;
                case CubeState.MarkedForExcavation:
                    HoverState.SetState(HoverState.State.CubeExcavate);
                    SetMaterialColor(this.excavateMaterialColor);
                    break;
                case CubeState.Hovered | CubeState.MarkedForExcavation:
                    HoverState.SetState(HoverState.State.CubeExcavate);
                    SetMaterialColorBlend(this.highlightMaterialColor, this.excavateMaterialColor);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetMaterialColor(ColorVariable colorVariable)
        {
            this.renderer.material.color = colorVariable.Get();
        } 
        
        private void SetMaterialColorBlend(ColorVariable colorVariableA, ColorVariable colorVariableB)
        {
            this.renderer.material.color = colorVariableA.Get() * colorVariableB.Get();
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
            UpdateMaterial();
            if (this.workerCommandActionEvent == null) return;
            this.workerCommandActionEvent.Raise(this);
        }
    }
}
