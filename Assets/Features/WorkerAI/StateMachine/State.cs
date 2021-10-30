using UnityEngine;
using UnityEngine.AI;

namespace Features.WorkerAI.StateMachine
{
    public class State
    {
        public enum STATE
        {
            WANDER,
            COMMAND
        };


        public enum STAGE
        {
            ENTER,
            UPDATE,
            EXIT
        };

        public STATE name { get; protected set; }
        protected STAGE stage;

        protected GameObject worker;
        protected WorkerBehavior workerBehavior;
        protected NavMeshAgent agent;

        protected State nextState;

        public State(GameObject worker, NavMeshAgent agent)
        {
            this.worker = worker;
            // TODO
            this.workerBehavior = worker.GetComponent<WorkerBehavior>();
            this.agent = agent;
            stage = STAGE.ENTER;
        }


        public virtual void Enter()
        {
            stage = STAGE.UPDATE;
        }

        public virtual void Update()
        {
            stage = STAGE.UPDATE;
        }

        public virtual void Exit()
        {
            stage = STAGE.EXIT;
        }


        public State Process()
        {
            if (stage == STAGE.ENTER) Enter();
            if (stage == STAGE.UPDATE) Update();
            if (stage == STAGE.EXIT)
            {
                Exit();
                return nextState;
            }

            return this;
        }

        public void SetNext(State nextState)
        {
            this.nextState = nextState;
            this.stage = STAGE.EXIT;
        }

        protected void Seek(Vector3 target)
        {
            agent.SetDestination(target);
        }

        protected float GetSpeedMultiplier()
        {
            return 1f / workerBehavior.size;
        }
    }
}
