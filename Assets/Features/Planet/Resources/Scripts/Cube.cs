using System;
using DataStructures.Variables;
using Features.Commands.Scripts.ActionEvents;
using Features.Planet_Generation.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Planet.Resources.Scripts
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Cube : MonoBehaviour
    {
        [Flags]
        public enum CubeState
        {
            Hovered = 1,
            MarkedForExcavation = 2,
            ExcavationStarted = 4
        }

        private CubeState state;
        public CubeState cubeState => this.state;

        [SerializeField] private WorkerCommandActionEvent workerCommandActionEvent;
        [FormerlySerializedAs("renderer")] [SerializeField] private MeshRenderer cubeMeshRenderer;
        [SerializeField] private ColorVariable defaultMaterialColor;
        [SerializeField] private ColorVariable highlightMaterialColor;
        [SerializeField] private ColorVariable excavateMaterialColor;

        public bool isMarkedForExcavation => this.state.HasFlag(CubeState.MarkedForExcavation);
        public bool hasExcavationStarted => this.state.HasFlag(CubeState.ExcavationStarted);
        public Resource_SO resourceData { get; private set; }
        public Vector3Int planetPosition { get; private set; }

        public void Init(Resource_SO resourceData, Vector3Int planetPosition)
        {
            this.resourceData = resourceData;
            this.planetPosition = planetPosition;
        }

        private void Awake()
        {
            if (this.cubeMeshRenderer == null)
            {
                this.cubeMeshRenderer = GetComponent<MeshRenderer>();
            }
        }

        private void OnHover()
        {
            if (this.state.HasFlag(CubeState.Hovered) || !this.cubeMeshRenderer) return;
            this.state |= CubeState.Hovered;
            UpdateMaterial();
        }

        private void OnHoverEnd()
        {
            if (!this.state.HasFlag(CubeState.Hovered) || !this.cubeMeshRenderer) return;
            this.state &= ~CubeState.Hovered;
            UpdateMaterial();
        }

        private void OnSelect(bool bExcavate)
        {
            if (!this.isMarkedForExcavation && bExcavate)
            {
                AddState(CubeState.MarkedForExcavation);
            }
            else if(!bExcavate && !this.hasExcavationStarted)
            {
                RemoveState(CubeState.MarkedForExcavation);
            }
        }
        
        public void OnStartExcavation()
        {
            if (!this.hasExcavationStarted)
            {
                AddState(CubeState.ExcavationStarted);
            }
        }

        private void UpdateMaterial()
        {
            switch (state)
            {
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
                    SetMaterialColor(this.defaultMaterialColor);
                    break;
            }
        }

        private void SetMaterialColor(ColorVariable colorVariable)
        {
            this.cubeMeshRenderer.material.color = colorVariable.Get();
        } 
        
        private void SetMaterialColorBlend(ColorVariable colorVariableA, ColorVariable colorVariableB)
        {
            this.cubeMeshRenderer.material.color = colorVariableA.Get() * colorVariableB.Get();
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
