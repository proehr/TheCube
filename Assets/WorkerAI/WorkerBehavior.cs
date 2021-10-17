﻿using UnityEngine;
using UnityEngine.AI;
using WorkerAI.StateMachine;

namespace WorkerAI
{
    public class WorkerBehavior : MonoBehaviour
    {
        [Min(1)] [SerializeField] public int size;

        [SerializeField] private float wanderR = 10; // Radius for our "wander circle"
        [SerializeField] private float wanderD = 20; // Distance for our "wander circle"
        [SerializeField] private float change = 0.3f;

        private Vector3 wanderTarget = Vector3.zero;
        private NavMeshAgent agent;
        public State currentState { get; private set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            currentState = new Wander(gameObject, agent, wanderR, wanderD, change);
        }

        private void Update()
        {
            currentState = currentState.Process();
        }

        public void QueueCommand(Command command)
        {
            currentState.SetNext(new ExecutingCommand(gameObject, agent, command));
        }

        public void QueueWandering()
        {
            currentState.SetNext(new Wander(gameObject, agent, wanderR, wanderD, change));
        }
    }
}
