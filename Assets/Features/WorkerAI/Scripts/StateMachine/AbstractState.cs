using UnityEngine;
using UnityEngine.AI;

namespace Features.WorkerAI.Scripts.StateMachine
{
    public abstract class AbstractState
    {
        public enum STATE
        {
            WANDER,
            COMMAND
        };


        protected enum STAGE
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

        protected AbstractState nextState;

        protected readonly float walkSpeedMultiplier;
        protected readonly float runSpeedMultiplier;

        public AbstractState(GameObject worker, NavMeshAgent agent, float walkSpeedMultiplier, float runSpeedMultiplier)
        {
            this.worker = worker;
            this.workerBehavior = worker.GetComponent<WorkerBehavior>();
            this.agent = agent;

            this.walkSpeedMultiplier = walkSpeedMultiplier;
            this.runSpeedMultiplier = runSpeedMultiplier;

            stage = STAGE.ENTER;
        }


        protected virtual void Enter()
        {
            stage = STAGE.UPDATE;
        }

        protected virtual void Update()
        {
            stage = STAGE.UPDATE;
        }

        protected virtual void Exit()
        {
            stage = STAGE.EXIT;
        }


        public AbstractState Process()
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

        public void SetNext(AbstractState nextState)
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
