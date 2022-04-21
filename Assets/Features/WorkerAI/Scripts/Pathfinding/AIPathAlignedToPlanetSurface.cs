using System;
using Features.Physics;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;
using Object = System.Object;

namespace Features.WorkerAI.Scripts.Pathfinding
{
    public class AIPathAlignedToPlanetSurface : AIPathAlignedToSurface
    {
        // private Vector3 lastSimulatedPosition;
        private RaycastHit lastEvaluatedRaycastHit;
        public string status;
        public string lastStatus;

        // protected override void Start()
        // {
        //     base.Start();
        //
        //     this.lastSimulatedPosition = this.transform.position;
        // }

        private void UpdateStatus(string newStatus, Object extraInfo = null)
        {
            if (newStatus == status) return; // no change
            if (lastStatus == newStatus &&
                status.Contains("falling") && 
                newStatus.StartsWith("left")) return; // falling loop

            lastStatus = status;
            status = newStatus;
            Debug.Log("Agent is " + newStatus + (extraInfo == null ? string.Empty : " " + extraInfo));
        }

        protected override void UpdateMovementPlane () {
            if (prevPosition2 == position)
            {
                UpdateStatus("100% not moving");
                // Agent hasn't moved, so it can't be falling
                return;
            }
            if (prevPosition2.EqualEnough(position, Vector3.kEpsilon))
            {
                UpdateStatus("not moving enough", (prevPosition2 - position));
                // Agent hasn't moved, so it can't be falling
                return;
            }

            // lastSimulatedPosition = simulatedPosition;

            // if (lastEvaluatedRaycastHit.point != Vector3.zero &&
            //     lastEvaluatedRaycastHit.point == lastRaycastHit.point)
            if (lastRaycastHit.point == Vector3.zero)
            {
                // Seems like the lastRaycastHit is still the same
                // --> no ground was found this update
                // --> agent is falling
                UpdateStatus("Left the movement plane, try to grab onto the next wall." , (prevPosition2 - position));
                // float elevation;
                //
                // movementPlane.ToPlane(position, out elevation);
                // Half height of the agent, downwards
                Vector3 rayOffset = movementPlane.ToWorld(Vector2.zero, tr.localScale.y * height * -0.5f);
                Debug.DrawLine(position, position+rayOffset, Color.blue, 30);
                var tangent = new Vector3();
                Vector3.OrthoNormalize(ref rayOffset, ref tangent);
                Debug.DrawLine(position+rayOffset, position+rayOffset + tangent, Color.magenta, 30);

                // Check all 4 directions for a possible wall to grab
                float shortestDistance = Single.PositiveInfinity;
                // RaycastHit closestCubeHit;
                for (int i = 0; i < 4; i++)
                {
                    var direction = /*position
                                    + */Quaternion.AngleAxis(90 * i, rayOffset)
                                    * tangent;
                    // Debug.Log(direction);
                    Debug.DrawLine(position+rayOffset, position+rayOffset + (direction * (maxSpeed * 2)), Color.green, 30);
                    if (UnityEngine.Physics.Raycast(
                            position + rayOffset,
                            direction,
                            out var cubeHit,
                            maxSpeed * 2,
                            groundMask,
                            QueryTriggerInteraction.Ignore))
                    {
                        if (cubeHit.distance < shortestDistance)
                        {
                            shortestDistance = cubeHit.distance;
                            lastRaycastHit = cubeHit;
                            // closestCubeHit = cubeHit;
                        }
                    }
                }

                if (float.IsPositiveInfinity(shortestDistance))
                {
                    // GG we didn't find a wall
                    // TODO what now??
                    UpdateStatus("falling aaaaAAAAAHHHHHH");
                }
            }
            else
            {
                UpdateStatus("grounded");
            }
            // lastEvaluatedRaycastHit = lastRaycastHit;
            
            // TODO how to walk across inner corners (e.g. from a higher cube onto an orthogonal lower cube)
            
            // base.UpdateMovementPlane();
            var normal = lastRaycastHit.normal;
            if (normal == Vector3.right)
            {
                normal = new Vector3(1, 0.01f, 0);
            }
            if (normal != Vector3.zero) {
                var fwd = Vector3.Cross(movementPlane.rotation * Vector3.right, normal);
                movementPlane = new SimpleMovementPlane(Quaternion.LookRotation(fwd, normal));
            }
            if (rvoController != null) rvoController.movementPlane = movementPlane;
        }
    }
}
