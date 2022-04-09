using System;
using DataStructures.Variables;
using Features.Commands.Scripts;
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
            Default,
            Hovered,
            MarkedForExcavation
        }

        [SerializeField] private WorkerCommandActionEvent workerCommandActionEvent;
        [FormerlySerializedAs("renderer")] [SerializeField] private MeshRenderer cubeMeshRenderer;
        [SerializeField] private ColorVariable defaultMaterialColor;
        [SerializeField] private ColorVariable highlightMaterialColor;
        [SerializeField] private ColorVariable excavateMaterialColor;
        
        private CubeState state;

        public bool isMarkedForExcavation => this.state.HasFlag(CubeState.MarkedForExcavation);
        public Resource_SO resourceData { get; private set; }
        public Vector3Int planetPosition { get; private set; }
        public CubeState cubeState => this.state;   
        public Vector3 stateNormal { get; private set; }

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

        private void OnSelect(SelectInfo info)
        {
            stateNormal = info.Hit.normal;
            if (!this.isMarkedForExcavation && info.Excavate)
            {
                AddState(CubeState.MarkedForExcavation);
            }
            else if(!info.Excavate)
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
