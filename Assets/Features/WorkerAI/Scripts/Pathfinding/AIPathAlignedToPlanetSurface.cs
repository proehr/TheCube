#if PATHFINDING
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
        public string status;
        public string lastStatus;

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

        public Vector3 desiredMovement => lastDeltaTime > 0.00001f
            ? movementPlane.ToWorld(lastDeltaPosition)
            : Vector3.zero;

        protected override void UpdateMovementPlane()
        {
            if (prevPosition2.EqualEnough(position, 1E-04f))
            {
                UpdateStatus("not moving enough", (prevPosition2 - position));
                // Agent hasn't moved, so it can't be falling
                return;
            }

            if (lastRaycastHit.point == Vector3.zero)
            {
                // lastRaycastHit didn't hit anything
                // --> no ground was found this update
                // --> agent is falling
                UpdateStatus("Left the movement plane, try to grab onto the next wall.", (prevPosition2 - position));
                // Half height of the agent, downwards
                Vector3 rayOffset = movementPlane.ToWorld(Vector2.zero, tr.localScale.y * height * -0.5f);
                Debug.DrawLine(position, position + rayOffset, Color.blue, 30);
                var tangent = new Vector3();
                Vector3.OrthoNormalize(ref rayOffset, ref tangent);
                Debug.DrawLine(position + rayOffset, position + rayOffset + tangent, Color.magenta, 30);

                // Check all 4 directions for a possible wall to grab
                // TODO only use invert (plane) movement
                float shortestDistance = Single.PositiveInfinity;
                for (int i = 0; i < 4; i++)
                {
                    var direction = Quaternion.AngleAxis(90 * i, rayOffset) * tangent;
                    Debug.DrawLine(position + rayOffset, position + rayOffset + (direction * (maxSpeed * 2)),
                        Color.green, 30);
                    if (!UnityEngine.Physics.Raycast(
                            position + rayOffset,
                            direction,
                            out var cubeHit,
                            maxSpeed * 2,
                            groundMask,
                            QueryTriggerInteraction.Ignore)) continue;
                    if (!(cubeHit.distance < shortestDistance)) continue;
                    shortestDistance = cubeHit.distance;
                    lastRaycastHit = cubeHit;
                }

                if (float.IsPositiveInfinity(shortestDistance))
                {
                    // GG we didn't find a wall
                    // TODO what now??
                    UpdateStatus("falling aaaaAAAAAHHHHHH");
                }
            }
            // Check for inner corners/edges
            // e.g. from a higher cube onto an orthogonal lower cube
            else
            {
                var desiredV = desiredMovement;
                Vector3 rayOffset = movementPlane.ToWorld(Vector2.zero, tr.localScale.y * height * 0.5f);
                Debug.DrawRay(position, rayOffset, Color.blue, 30);
                var dir = desiredV;
                var maxDistance = tr.localScale.x * radius + desiredV.magnitude * 2;
                dir *= (maxDistance) / dir.magnitude;
                Debug.DrawRay(position + rayOffset - desiredV, dir, Color.red, 30);
                if (UnityEngine.Physics.Raycast(
                        position + rayOffset - desiredV, // start from negative desired velocity to ensure we didn't miss the cube wall
                        desiredV, // TODO remove verticalVelocity?
                        out var cubeHit,
                        maxDistance, // Look ahead the expected future position
                        groundMask,
                        QueryTriggerInteraction.Ignore))
                {
                    UpdateStatus("Moving onto orthogonal wall.");
                    lastRaycastHit = cubeHit;
                }
                else
                {
                    UpdateStatus("grounded");
                }
            }

            var normal = lastRaycastHit.normal;
            if (normal == Vector3.right)
            {
                // TODO FIXME this prevents fwd to be (0,0,0) if
                // movementPlane.rotation = (0,0,0)
                normal = new Vector3(1, 0.01f, 0);
            }

            if (normal != Vector3.zero)
            {
                var fwd = Vector3.Cross(movementPlane.rotation * Vector3.right, normal);
                movementPlane = new SimpleMovementPlane(Quaternion.LookRotation(fwd, normal));
            }

            if (rvoController != null) rvoController.movementPlane = movementPlane;
        }
    }
}
#endif
