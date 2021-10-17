using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WorkerAI.StateMachine;
using Random = UnityEngine.Random;

namespace WorkerAI
{
    public class WorkerInputManager : MonoBehaviour
    {
        [SerializeField] private WorkerBehavior[] workerPrefabs;
        [SerializeField] private GameObject commandPostPrefab;
        [SerializeField] private int workerPerCommand = 4;

        [Header("UI")] [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text workerInfoText;

        private readonly List<WorkerBehavior> weightedWorkerPrefabs = new List<WorkerBehavior>();
        private readonly List<WorkerBehavior> usedWorkerPrefabs = new List<WorkerBehavior>();
        private readonly List<WorkerBehavior> workers = new List<WorkerBehavior>();

        private readonly Dictionary<State.STATE, List<WorkerBehavior>> workersPerState =
            new Dictionary<State.STATE, List<WorkerBehavior>>();

        private readonly List<Command> runningCommands = new List<Command>();

        private void Start()
        {
            foreach (State.STATE state in Enum.GetValues(typeof(State.STATE)))
            {
                workersPerState.Add(state, new List<WorkerBehavior>());
            }

            var workersInScene = GameObject.FindGameObjectsWithTag("Worker");
            foreach (var workerGO in workersInScene)
            {
                var worker = workerGO.GetComponent<WorkerBehavior>();
                AddWorker(worker);
            }

            foreach (var workerPrefab in workerPrefabs)
            {
                var count = Mathf.Pow(9, 3 - workerPrefab.size);
                for (int i = 0; i < count; i++)
                {
                    weightedWorkerPrefabs.Add(workerPrefab);
                }
            }

            slider.value = workerPerCommand;
            text.text = workerPerCommand.ToString();
            Debug();
        }

        private void AddWorker(WorkerBehavior worker)
        {
            workers.Add(worker);
            workersPerState[worker.currentState.name].Add(worker);
        }

        private WorkerBehavior PickWorkerPrefab()
        {
            if (weightedWorkerPrefabs.Count == 0)
            {
                weightedWorkerPrefabs.AddRange(usedWorkerPrefabs);
                usedWorkerPrefabs.Clear();
            }

            var index = Random.Range(0, weightedWorkerPrefabs.Count);
            var pickedPrefab = weightedWorkerPrefabs[index];
            usedWorkerPrefabs.Add(pickedPrefab);
            weightedWorkerPrefabs.RemoveAt(index);
            return pickedPrefab;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out var hit, 100);
            var transformPosition = hit.point;
            transformPosition.y = 0.5f;
            var worker = Instantiate(PickWorkerPrefab(), transformPosition,
                Quaternion.Euler(0, Random.Range(0, 360), 0));
            AddWorker(worker);
            Debug();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;

            Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out var hit, 100);

            var command = new Command(hit.point, commandPostPrefab);
            runningCommands.Add(command);

            var idleWorkers = workersPerState[State.STATE.WANDER];
            for (int i = 0; i < workerPerCommand; i++)
            {
                if (idleWorkers.Count == 0) break;

                var worker = idleWorkers[Random.Range(0, idleWorkers.Count)];
                worker.QueueCommand(command);
                idleWorkers.Remove(worker);
                workersPerState[State.STATE.COMMAND].Add(worker);
            }

            Debug();
        }

        public void OnSpaceKey(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;

            var commandedWorkers = workersPerState[State.STATE.COMMAND];
            foreach (var worker in commandedWorkers)
            {
                worker.QueueWandering();
                workersPerState[State.STATE.WANDER].Add(worker);
            }

            foreach (var command in runningCommands)
            {
                command.End();
            }

            runningCommands.Clear();
            commandedWorkers.Clear();

            Debug();
        }

        public void OnWorkersPerCommandValueChange()
        {
            var value = Mathf.RoundToInt(slider.value);
            workerPerCommand = value;
            text.text = value.ToString();
        }

        private void Debug()
        {
            UnityEngine.Debug.Log("Workers: " + "ON THE JOB " + workersPerState[State.STATE.COMMAND].Count +
                                  " / IDLE " +
                                  workersPerState[State.STATE.WANDER].Count + " / TOTAL " + workers.Count);
            workerInfoText.text = "<align=\"left\"><u>Workers</u></align>" +
                                  "\nON THE JOB: " + workersPerState[State.STATE.COMMAND].Count +
                                  "\nIDLE: " + workersPerState[State.STATE.WANDER].Count +
                                  "\nTOTAL: " + workers.Count;
        }
    }
}