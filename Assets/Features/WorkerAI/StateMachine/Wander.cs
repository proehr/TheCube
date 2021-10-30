using Features.Physics;
using UnityEngine;
using UnityEngine.AI;

namespace Features.WorkerAI.StateMachine
{
    public class Wander : State
    {
        float wanderTheta = 0;
        private readonly float wanderR;
        private readonly float wanderD;
        private readonly float change;

        public Wander(GameObject worker, NavMeshAgent agent, float wanderR, float wanderD, float change)
            : base(worker, agent)
        {
            name = STATE.WANDER;

            this.wanderR = wanderR;
            this.wanderD = wanderD;
            this.change = change;
        }

        public override void Enter()
        {
            agent.speed = 3.5f * GetSpeedMultiplier();

            base.Enter();
        }

        public override void Update()
        {
            WanderNOC();
            // TODO detect new command?
        }

        private void WanderNOC()
        {
            wanderTheta += Random.Range(-change, change); // Randomly change wander theta

            // Now we have to calculate the new position to steer towards on the wander circle
            Vector3 circlepos = agent.velocity.normalized; // Start with velocity
            // circlepos.normalize();            // Normalize to get heading
            circlepos *= wanderD; // Multiply by distance
            circlepos += worker.transform.position; // Make it relative to boid's position


            float h = agent.velocity.Heading(); // We need to know the heading to offset wandertheta

            var circleOffSet =
                new Vector3(wanderR * Mathf.Cos(wanderTheta + h), 0, wanderR * Mathf.Sin(wanderTheta + h));
            var target = circlepos + circleOffSet;
            Seek(target);
        }
    }
}
