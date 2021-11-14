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

        private Vector3 wanderTarget = Vector3.zero;
        private NavMeshAgent agent;

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
            currentState.SetNext(new ExecutingCommand(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier,
                command));
        }

        public void QueueWandering()
        {
            currentState.SetNext(new Wander(gameObject, agent, walkSpeedMultiplier, runSpeedMultiplier, wanderRadius,
                wanderDistance, maxAngleChange));
        }

        public WorkerVO GetWorkerVO()
        {
            return new WorkerVO(transform, size);
        }
    }
}
