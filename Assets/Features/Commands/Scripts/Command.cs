using System.Collections.Generic;
using Features.Planet.Resources.Scripts;
using Features.WorkerAI.Scripts;
using Features.WorkerAI.Scripts.StateMachine;
using UnityEngine;

namespace Features.Commands.Scripts
{
    public abstract class Command
    {
        public enum Stage
        {
            STARTING,
            COLLECTING_WORKERS,
            RUNNING,
            CLEANING_UP,
            FINISHED
        };

        private Stage stage;
        public Vector3 location { get; private set; }
        private GameObject commandPost;
        protected readonly Command_SO commandData;
        private readonly WorkerService_SO workerService;
        private readonly ICollection<WorkerBehavior> workersForCommand;
        public int CubeObjectId { get; private set; }
        public bool Success { get; private set; }

        /**
         * squared for performance reasons
         */
        private readonly float sqrMaxWorkerDistance;

        protected float remainingDuration;

        protected Command(Cube targetCube, int cubeObjectId, Vector3 planeNormal, WorkerService_SO workerService,
            Command_SO commandData, Transform commandPostsParent)
        {
            this.workerService = workerService;
            this.CubeObjectId = cubeObjectId;
            // TODO Might want to offset according to plane normal
            this.location = targetCube.transform.position;
            this.commandData = commandData;
            this.workersForCommand = new List<WorkerBehavior>(commandData.RequiredWorkers);
            if (commandData.CommandPostPrefab != null)
            {
                Vector3 commandPostLocation = this.location;
                commandPostLocation += planeNormal * commandData.CommandPostPrefab.transform.localScale.y;
                commandPost = Object.Instantiate(commandData.CommandPostPrefab, commandPostLocation, Quaternion.identity,
                    commandPostsParent);
                commandPost.transform.up = planeNormal;
            }

            this.sqrMaxWorkerDistance = Mathf.Pow(commandData.MaxDistanceToLocation, 2);
            this.remainingDuration = commandData.Duration;

            Success = false;

            stage = Stage.STARTING;
        }

        public Stage Process()
        {
            // Not a switch as all stages could happen in the same frame
            if (stage == Stage.STARTING)
            {
                Start();
            }

            if (stage == Stage.COLLECTING_WORKERS)
            {
                CheckIfWorkersAreReady();
            }

            if (stage == Stage.RUNNING)
            {
                Update();
            }

            if (stage == Stage.CLEANING_UP)
            {
                End();
            }

            return stage;
        }

        public void Cancel()
        {
            this.Success = false;
            stage = Stage.CLEANING_UP;
            this.End();
        }

        protected virtual void Start()
        {
            stage = Stage.COLLECTING_WORKERS;
        }

        private bool CollectWorkers()
        {
            if (this.workersForCommand.Count < commandData.RequiredWorkers)
            {
                workerService.GetWorkersForCommand(commandData.RequiredWorkers - this.workersForCommand.Count,
                    this.workersForCommand);
            }

            foreach (var worker in workersForCommand)
            {
                worker.QueueCommand(this);
            }

            return workersForCommand.Count >= commandData.RequiredWorkers;
        }

        private void CheckIfWorkersAreReady()
        {
            if (!this.CollectWorkers())
            {
                // We don't have enough workers yet
                return;
            }

            foreach (var worker in workersForCommand)
            {
                // TODO maybe ask worker if they are "ready"? Can be a range check initially but later state/animation check
                if (Vector3.SqrMagnitude(worker.transform.position - location) > sqrMaxWorkerDistance)
                {
                    return;
                }
            }

            // Every worker is in range --> proceed to next stage
            stage = Stage.RUNNING;
        }

        protected virtual void Update()
        {
            this.remainingDuration -= Time.deltaTime;

            if (!(this.remainingDuration <= 0)) return;
            this.remainingDuration = 0;
            this.Success = true;
            this.stage = Stage.CLEANING_UP;
        }

        public virtual void End()
        {
            DestroyCommandPost();
            SendWorkersToIdle();

            stage = Stage.FINISHED;
        }

        private void SendWorkersToIdle()
        {
            foreach (var worker in workersForCommand)
            {
                workerService.UpdateWorkerState(worker, AbstractState.STATE.WANDER);
                worker.QueueWandering();
            }
        }

        private void DestroyCommandPost()
        {
            if (commandPost != null)
            {
                Object.Destroy(commandPost);
            }
        }
    }
}
