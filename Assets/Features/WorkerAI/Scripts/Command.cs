using System.Collections.Generic;
using System.Timers;
using Features.Commands.Scripts.ActionEvents;
using Features.WorkerAI.Scripts.StateMachine;
using Features.WorkerDTO;
using UnityEngine;

namespace Features.WorkerAI.Scripts
{
    public class Command
    {
        public Vector3 location { get; private set; }
        private GameObject commandPost;
        private readonly Command_SO commandData;
        private readonly CommandFinishedActionEvent commandFinishedActionEvent;
        private readonly WorkerBO_SO workerBO;
        private IEnumerable<WorkerBehavior> workersForCommand;
        private Timer timer;
        public int cubeObjectId { get; private set; }

        public Command(Cube targetCube, int cubeObjectId, Vector3 planeNormal, WorkerBO_SO workerBO,
            Command_SO commandData, CommandFinishedActionEvent commandFinishedActionEvent, Transform commandPostsParent)
        {
            this.workerBO = workerBO;
            this.cubeObjectId = cubeObjectId;
            this.location = targetCube.transform.position;
            this.commandData = commandData;
            this.commandFinishedActionEvent = commandFinishedActionEvent;
            if (commandData.CommandPostPrefab != null)
            {
                commandPost = Object.Instantiate(commandData.CommandPostPrefab, this.location, Quaternion.identity, commandPostsParent);
                commandPost.transform.up = planeNormal;
            }
        }

        private void Start()
        {
            this.CollectWorkers();

            // Little timer for testing purposes
            this.timer = new System.Timers.Timer(commandData.Duration);
            timer.AutoReset = false;
            timer.Elapsed += (sender, args) =>
            {
                this.End();
            };
            timer.Start();
        }

        private void CollectWorkers()
        {
            this.workersForCommand = workerBO.GetWorkersForCommand(commandData.RequiredWorkers);
            foreach (var worker in workersForCommand)
            {
                worker.QueueCommand(this);
            }
        }


        public void End()
        {
            timer.Stop();
            timer.Dispose();

            DestroyCommandPost();
            SendWorkersToIdle();
            commandFinishedActionEvent.Raise(this);
        }

        private void SendWorkersToIdle()
        {
            foreach (var worker in workersForCommand)
            {
                workerBO.UpdateWorkerState(worker, AbstractState.STATE.WANDER);
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
