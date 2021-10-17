using UnityEngine;
using UnityEngine.AI;

namespace WorkerAI.StateMachine
{
    public class ExecutingCommand : State
    {
        private readonly Command command;

        public ExecutingCommand(GameObject worker, NavMeshAgent agent, Command command) : base(worker, agent)
        {
            this.command = command;
        }

        public override void Enter()
        {
            agent.speed = 7f * GetSpeedMultiplier();
            Seek(command.location);

            base.Enter();
        }
    }
}
