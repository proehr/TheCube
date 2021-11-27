using DataStructures.Variables;
using Features.Planet.Resources.Scripts;
using Features.WorkerAI.Scripts;
using UnityEngine;

namespace Features.Commands.Scripts
{
    public class ExcavationCommand : Command
    {
        private readonly Cube targetCube;
        private readonly Vector3 cubeStartScale;
        private readonly int resourceAmount;
        private readonly IntVariable resourceSlot;

        public ExcavationCommand(Cube targetCube, int cubeObjectId, Vector3 planeNormal, WorkerService_SO workerService,
            Command_SO commandData, Transform commandPostsParent) :
            base(targetCube, cubeObjectId, planeNormal, workerService, commandData, commandPostsParent)
        {
            this.targetCube = targetCube;
            this.cubeStartScale = targetCube.transform.localScale;
            this.resourceAmount = RandomFromRange(targetCube.resourceData.ExcavationAmountInterval);
            this.resourceSlot = targetCube.resourceData.InventoryResource;
        }

        /**
         * Vector2Int.x = minInclusive
         * Vector2Int.y = maxInclusive (different behavior than Random.Range!)
         */
        private static int RandomFromRange(Vector2Int range)
        {
            return Random.Range(range.x, range.y + 1);
        }

        protected override void Update()
        {
            /*
            * 0.0 = the command still starting
            * 1.0 = the command is fulfilled
            */
            float progress = 1 - remainingDuration / commandData.Duration;
            // The further the progress, the smaller the target cube (as it's getting excavated)
            // TODO use leantween otherwise Xyck will punch me
            targetCube.transform.localScale = this.cubeStartScale * (1 - progress);

            // TODO grant resources according to excavated cube

            base.Update();
        }

        public override void End()
        {
            if (this.Success)
            {
                this.resourceSlot.Add(this.resourceAmount);
            }
            base.End();
        }
    }
}
