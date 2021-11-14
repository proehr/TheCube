using System.Collections.Generic;
using Features.Commands.Scripts.ActionEvents;
using Features.WorkerAI.Scripts;
using Features.WorkerDTO;
using UnityEngine;

namespace Features.Commands.Scripts
{
    public class WorkerCommandHandler : MonoBehaviour
    {
        [SerializeField] private WorkerBO_SO workerBo;
        [SerializeField] private WorkerCommandActionEvent workerCommandEvent;
        [SerializeField] private CommandFinishedActionEvent commandFinishedEvent;
        private readonly Dictionary<int, Command> activeCommands = new Dictionary<int, Command>();
        [SerializeField] private Command_SO excavationCommandData;
        [SerializeField] private Transform commandPostsParent;

        private void OnEnable()
        {
            workerCommandEvent.RegisterListener(OnNewWorkerCubeCommand);
            commandFinishedEvent.RegisterListener(OnCommandFinished);
        }

        private void OnDisable()
        {
            workerCommandEvent.UnregisterListener(OnNewWorkerCubeCommand);
            commandFinishedEvent.UnregisterListener(OnCommandFinished);
        }

        private void OnNewWorkerCubeCommand(Cube cube)
        {
            // Example code
            var cubeGameObject = cube.gameObject;
            var cubeObjectId = cubeGameObject.GetInstanceID();
            if (cube.isMarkedForExcavation)
            {
                // Create worker command; TODO: planeNormal
                var pCommand = new Command(cube, cubeObjectId, Vector3.up,workerBo, excavationCommandData, commandFinishedEvent, commandPostsParent);
                // UpdateActiveCommands();
                if (!this.activeCommands.ContainsKey(cubeObjectId))
                {
                    activeCommands.Add(cubeObjectId, pCommand);
                }
            }
            else
            {
                if (this.activeCommands.TryGetValue(cubeObjectId, out var command))
                {
                    command.End();
                    this.activeCommands.Remove(cubeObjectId);
                }
            }
        }

        private void OnCommandFinished(Command finishedCommand)
        {
            this.activeCommands.Remove(finishedCommand.cubeObjectId);
        }
    }
}
