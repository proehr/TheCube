﻿using Features.WorkerAI.StateMachine;
using Features.WorkerDTO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utils.CrossSceneReference;

namespace Features.WorkerAI
{
    public class WorkerBehavior : MonoBehaviour
    {
        [Min(1)] [SerializeField] public int size;

        [Tooltip("Radius for our 'wander circle'")] [FormerlySerializedAs("wanderR")] [SerializeField]
        private float wanderRadius = 10;

        [FormerlySerializedAs("wanderD")] [Tooltip("Distance for our 'wander circle'")] [SerializeField]
        private float wanderDistance = 20;

        [Tooltip("In Radians")] [FormerlySerializedAs("change")] [SerializeField]
        private float maxAngleChange = 0.3f;

        [SerializeField] private float walkSpeedMultiplier = 3.5f;
        [SerializeField] private float runSpeedMultiplier = 7.0f;

        private Vector3 wanderTarget = Vector3.zero;
        private NavMeshAgent agent;
        private GuidComponent guidComponent;
        
        public AbstractState currentState { get; private set; }

        private void Awake()
        {
            guidComponent = GetComponent<GuidComponent>();
            agent = GetComponent<NavMeshAgent>();
            currentState = new Wander(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier, wanderRadius,
                wanderDistance, maxAngleChange);
        }

        private void Update()
        {
            currentState = currentState.Process();

            Debug.DrawRay(transform.position, this.agent.desiredVelocity, Color.red, 0.5f);
        }

        public void QueueCommand(Command command)
        {
            currentState.SetNext(new ExecutingCommand(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier,
                command));
        }

        public void QueueWandering()
        {
            currentState.SetNext(new Wander(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier, wanderRadius,
                wanderDistance, maxAngleChange));
        }
        
        public WorkerVo GetWorkerVo()
        {
            return new WorkerVo(transform, size, guidComponent.GetGuid());
        }
    }
}
