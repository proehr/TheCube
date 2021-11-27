using Features.Commands.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Features.WorkerAI.Scripts.StateMachine
{
    public class ExecutingCommand : AbstractState
    {
        private readonly Command command;

        public ExecutingCommand(GameObject worker, NavMeshAgent agent, float walkSpeedMultiplier,
            float runSpeedMultiplier, Command command) : base(worker, agent, walkSpeedMultiplier, runSpeedMultiplier)
        {
            name = STATE.COMMAND;
            this.command = command;
        }

        protected override void Enter()
        {
            agent.speed = runSpeedMultiplier * GetSpeedMultiplier();
            Seek(command.location);

            base.Enter();
        }
    }
}
