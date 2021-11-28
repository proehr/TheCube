using System.Runtime.CompilerServices;
using DataStructures.Variables;
using Features.ExtendedRandom;
using Features.Planet.Resources.Scripts;
using Features.WorkerAI.Scripts;
using UnityEngine;

[assembly:InternalsVisibleTo("ExcavationCommandTests")]

namespace Features.Commands.Scripts.Excavation
{
    public class ExcavationCommand : Command
    {
        private readonly Cube targetCube;
        private readonly Vector3 cubeStartScale;
        private readonly int resourceAmount;
        private readonly IntVariable resourceSlot;
        private readonly int[] tickExcavations;

        public ExcavationCommand(Cube targetCube, int cubeObjectId, Vector3 planeNormal, WorkerService_SO workerService,
            ExcavationCommandData_SO excavationCommandData, Transform commandPostsParent) :
            base(targetCube, cubeObjectId, planeNormal, workerService, excavationCommandData, commandPostsParent)
        {
            this.targetCube = targetCube;
            this.cubeStartScale = targetCube.transform.localScale;
            this.resourceSlot = targetCube.resourceData.InventoryResource;
            // Calculate number of ticks = max resources excavated / excavation per tick
            int numberOfTicks = Mathf.CeilToInt((float) targetCube.resourceData.ExcavationAmountInterval.y /
                                                excavationCommandData.ExcavationAmountPerTick);
            this.tickExcavations = new int[numberOfTicks];

            // Randomize how many resources actually going to be gathered
            this.resourceAmount = XRandom.Range(targetCube.resourceData.ExcavationAmountInterval);
            // Determine which ticks will actually yield resources
            // how many ticks will yield resources = floor(resourceAmount / excavation per tick)
            int numberOfExcavationTicks =
                Mathf.FloorToInt((float) resourceAmount / excavationCommandData.ExcavationAmountPerTick);
            if (numberOfExcavationTicks > 0)
            {
                // distribute yielding and unyielding ticks evenly, always preferring
                // the last possible tick if multiple ticks could be yielding
                for (int i = 0; i < numberOfTicks; i++)
                {
                    if (i % numberOfExcavationTicks == 0)
                    {
                        this.tickExcavations[numberOfTicks - i - 1] = excavationCommandData.ExcavationAmountPerTick;
                    }
                }
            }

            // remaining resources are excavated in last tick
            this.tickExcavations[numberOfTicks - 1] +=
                resourceAmount % excavationCommandData.ExcavationAmountPerTick;
        }

        internal static bool Test()
        {
            return true;
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
                Object.Destroy(targetCube);
                this.resourceSlot.Add(this.resourceAmount);
            }

            base.End();
        }
    }
}
