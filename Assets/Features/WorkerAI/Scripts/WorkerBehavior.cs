using Features.Commands.Scripts;
using Features.WorkerAI.Scripts.StateMachine;
using Features.WorkerDTO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Features.WorkerAI.Scripts
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

        private NavMeshAgent agent;
        /**
         * After full-filling a command, the work will be teleported back here to continue their life
         */
        private Vector3 idlePosition = Vector3.zero;

        public AbstractState currentState { get; private set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            currentState = new Wander(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier, wanderRadius,
                wanderDistance, maxAngleChange);
        }

        private void Update()
        {
            currentState = currentState.Process();
        }

        public void QueueCommand(Command command)
        {
            if (currentState.name == AbstractState.STATE.COMMAND) return;

            idlePosition = agent.destination;
            currentState.SetNext(new ExecutingCommand(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier,
                command));
        }

        public bool IsReadyForCommand(Vector3 commandLocation, float sqrMaxDistance)
        {
            // This is just the very basic implementation.
            // Later on a worker should know if he's in place and finished all animations so the actual command work
            // can start.
            return Vector3.SqrMagnitude(this.transform.position - commandLocation) <= sqrMaxDistance;
        }


        public void QueueWandering()
        {
            agent.Warp(idlePosition);
            currentState.SetNext(new Wander(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier, wanderRadius,
                wanderDistance, maxAngleChange));
        }

        public WorkerVO GetWorkerVO()
        {
            return new WorkerVO(transform, size);
        }
    }
}
