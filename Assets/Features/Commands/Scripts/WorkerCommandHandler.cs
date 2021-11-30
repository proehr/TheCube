using System;
using System.Collections.Generic;
using DataStructures.Variables;
using Features.Commands.Scripts.ActionEvents;
using Features.Commands.Scripts.Excavation;
using Features.Planet.Resources.Scripts;
using Features.PlanetGeneration.Scripts;
using Features.WorkerAI.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Features.Commands.Scripts
{
    public class WorkerCommandHandler : MonoBehaviour
    {
        [SerializeField] private WorkerService_SO workerService;
        [SerializeField] private CommandModeReference commandMode;
        [SerializeField] private WorkerCommandActionEvent workerCommandEvent;
        [SerializeField] private CommandFinishedActionEvent commandFinishedEvent;
        [SerializeField] private Transform commandPostsParent;
        [SerializeField] private UnityEvent onCommandAction;
        [SerializeField] private CubeRemovedActionEvent onCubeRemoved;


        private readonly Dictionary<int, Command> activeCommandsPerCube = new Dictionary<int, Command>();

        /**
         * Required to have an insertion ordered collection of active commands
         */
        [ShowInInspector, ReadOnly] private readonly List<Command> activeCommands = new List<Command>();

        private readonly List<Command> commandsFlaggedForRemoval = new List<Command>();

        private void OnEnable()
        {
            workerCommandEvent.RegisterListener(OnNewWorkerCubeCommand);
        }

        private void OnDisable()
        {
            workerCommandEvent.UnregisterListener(OnNewWorkerCubeCommand);
        }

        private void OnNewWorkerCubeCommand(Cube cube)
        {
            var cubeGameObject = cube.gameObject;
            var cubeObjectId = cubeGameObject.GetInstanceID();
            switch ((CommandMode) commandMode)
            {
                case CommandMode.Excavate:
                {
                    OnNewExcavationCommand(cube, cubeObjectId);
                    break;
                }
                case CommandMode.TransportLine:
                {
                    throw new NotImplementedException();
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnNewExcavationCommand(Cube cube, int cubeObjectId)
        {
            if (cube.isMarkedForExcavation)
            {
                // Create worker command; TODO: planeNormal
                var pCommand = new ExcavationCommand(cube, cubeObjectId, Vector3.up, workerService,
                    cube.resourceData.ExcavationCommandData, commandPostsParent, onCubeRemoved);
                if (!this.activeCommandsPerCube.ContainsKey(cubeObjectId))
                {
                    activeCommandsPerCube.Add(cubeObjectId, pCommand);
                    activeCommands.Add(pCommand);
                }
            }
            else
            {
                if (this.activeCommandsPerCube.TryGetValue(cubeObjectId, out var command))
                {
                    command.Cancel();
                    RemoveCommand(command);
                }
            }
            onCommandAction?.Invoke();
        }

        private void Update()
        {
            foreach (var command in this.activeCommands)
            {
                if (command.Process() != Command.Stage.FINISHED) continue;

                commandsFlaggedForRemoval.Add(command);
                if (command.Success)
                {
                    this.commandFinishedEvent.Raise(command);
                }
            }

            if (commandsFlaggedForRemoval.Count == 0) return;

            foreach (var command in commandsFlaggedForRemoval)
            {
                RemoveCommand(command);
            }

            commandsFlaggedForRemoval.Clear();
        }

        private void RemoveCommand(Command command)
        {
            this.activeCommandsPerCube.Remove(command.CubeObjectId);
            this.activeCommands.Remove(command);
        }
    }
}
