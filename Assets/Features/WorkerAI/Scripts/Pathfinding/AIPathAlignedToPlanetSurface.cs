using Pathfinding;
using UnityEngine;

namespace Features.WorkerAI.Scripts.Pathfinding
{
    public class AIPathAlignedToPlanetSurface : AIPathAlignedToSurface
    {
        protected new Vector3 RaycastPosition (Vector3 position, float lastElevation) {
            float elevation;

            movementPlane.ToPlane(position, out elevation);
            float rayLength = tr.localScale.y * height * 0.5f + Mathf.Max(0, lastElevation-elevation);
            Vector3 rayOffset = movementPlane.ToWorld(Vector2.zero, rayLength);

            if (UnityEngine.Physics.Raycast(position + rayOffset, -rayOffset, out lastRaycastHit, rayLength, groundMask, QueryTriggerInteraction.Ignore)) {
                // Grounded
                // Make the vertical velocity fall off exponentially. This is reasonable from a physical standpoint as characters
                // are not completely stiff and touching the ground will not immediately negate all velocity downwards. The AI will
                // stop moving completely due to the raycast penetration test but it will still *try* to move downwards. This helps
                // significantly when moving down along slopes as if the vertical velocity would be set to zero when the character
                // was grounded it would lead to a kind of 'bouncing' behavior (try it, it's hard to explain). Ideally this should
                // use a more physically correct formula but this is a good approximation and is much more performant. The constant
                // '5' in the expression below determines how quickly it converges but high values can lead to too much noise.
                verticalVelocity *= System.Math.Max(0, 1 - 5 * lastDeltaTime);
                return lastRaycastHit.point;
            }
            else
            {
                Debug.Log("Falling");
            }
            return position;
        }
    }
}
