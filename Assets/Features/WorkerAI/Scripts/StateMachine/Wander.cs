using Features.Physics;
using UnityEngine;
using UnityEngine.AI;

namespace Features.WorkerAI.Scripts.StateMachine
{
    public class Wander : AbstractState
    {
        private float wanderTheta = 0;
        private readonly float wanderRadius;
        private readonly float wanderDistance;
        private readonly float maxAngleChange;

        public Wander(GameObject worker, NavMeshAgent agent, float walkSpeedMultiplier, float runSpeedMultiplier,
            float wanderRadius, float wanderDistance, float maxAngleChange)
            : base(worker, agent, walkSpeedMultiplier, runSpeedMultiplier)
        {
            name = STATE.WANDER;

            this.wanderRadius = wanderRadius;
            this.wanderDistance = wanderDistance;
            this.maxAngleChange = maxAngleChange;
        }

        protected override void Enter()
        {
            agent.speed = this.walkSpeedMultiplier * GetSpeedMultiplier();

            base.Enter();
        }

        protected override void Update()
        {
            WanderNOC();
        }

        /**
         * Courtesy of "Nature of Code"
         */
        private void WanderNOC()
        {
            wanderTheta += Random.Range(-maxAngleChange, maxAngleChange); // Randomly change wander theta

            // Now we have to calculate the new position to steer towards on the wander circle
            // Vector3 circlepos = agent.velocity.normalized; // Start with velocity
            Vector3 circlepos = agent.transform.forward; // Start with velocity
            // circlepos.normalize();            // Normalize to get heading
            circlepos *= wanderDistance; // Multiply by distance
            circlepos += worker.transform.position; // Make it relative to boid's position


            float h = agent.velocity.Heading(); // We need to know the heading to offset wandertheta

            var up = agent.transform.up;
            // Debug.DrawRay(agent.transform.position, up, new Color(1, 0.5f, 0), 0.5f);

            Vector3 circleOffSet;
            // var circleOffSet =
            // new Vector3(wanderR * Mathf.Cos(wanderTheta + h), 0, wanderR * Mathf.Sin(wanderTheta + h));
            var x = wanderRadius * Mathf.Cos(wanderTheta + h);
            var y = wanderRadius * Mathf.Sin(wanderTheta + h);

            // FIXME this doesn't work yet. What's supposed to happen: The wandering adjusts to the plane orientation.
            // What actually happens: ¯\_(ツ)_/¯
            if (up.y > 0)
            {
                circleOffSet = new Vector3(x, 0, y);
            }
            else if (up.y < 0)
            {
                circleOffSet = new Vector3(-x, 0, -y);
            }
            else
            {
                if (up.x > 0)
                {
                    circleOffSet = new Vector3(0, y, x);
                }
                else if (up.x < 0)
                {
                    circleOffSet = new Vector3(0, -y, -x);
                }
                else
                {
                    if (up.z > 0)
                    {
                        circleOffSet = new Vector3(x, y, 0);
                    }
                    else if (up.z < 0)
                    {
                        circleOffSet = new Vector3(-x, -y, 0);
                    }
                    else
                    {
                        Debug.LogError("Up doesn't have a direction? " + up);
                        circleOffSet = new Vector3(0, 0, 0);
                    }
                }
            }

            // Debug.DrawRay(agent.transform.position, circlepos, Color.green, 0.5f);
            // Debug.DrawRay(circlepos, circleOffSet, Color.blue, 0.5f);

            var target = circlepos + circleOffSet;
            Seek(target);
        }
    }
}
