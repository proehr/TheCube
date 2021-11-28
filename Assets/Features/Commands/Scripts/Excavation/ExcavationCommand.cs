using DataStructures.Variables;
using Features.ExtendedRandom;
using Features.Planet.Resources.Scripts;
using Features.Planet_Generation.Scripts;
using Features.WorkerAI.Scripts;
using UnityEngine;

namespace Features.Commands.Scripts.Excavation
{
    public class ExcavationCommand : Command
    {
        private readonly Cube targetCube;
        private readonly Vector3 cubeStartScale;
        private readonly int resourceAmount;
        private readonly IntVariable resourceSlot;
        private readonly CubeRemovedActionEvent onCubeRemoved;

        public ExcavationCommand(Cube targetCube, int cubeObjectId, Vector3 planeNormal, WorkerService_SO workerService,
            Command_SO excavationCommandData, Transform commandPostsParent,
            CubeRemovedActionEvent onCubeRemoved) :
            base(targetCube, cubeObjectId, planeNormal, workerService, excavationCommandData, commandPostsParent)
        {
            this.targetCube = targetCube;
            this.cubeStartScale = targetCube.transform.localScale;
            this.resourceSlot = targetCube.resourceData.InventoryResource;
            this.onCubeRemoved = onCubeRemoved;

            // Randomize how many resources actually going to be gathered
            this.resourceAmount = XRandom.Range(targetCube.resourceData.ExcavationAmountInterval);
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
                onCubeRemoved.Raise(targetCube);
                Object.Destroy(targetCube.gameObject);
                this.resourceSlot.Add(this.resourceAmount);
            }

            base.End();
        }
    }
}
