using System;
using System.Collections.Generic;
using Features.WorkerAI.StateMachine;
using Features.WorkerDTO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Features.WorkerAI.Demo
{
    public class WorkerInputManager : SerializedMonoBehaviour
    {
        [DictionaryDrawerSettings(KeyLabel = "Worker Size", ValueLabel = "Worker Prefab")]
        [OdinSerialize] private Dictionary<int, WorkerBehavior> workerPrefabs;
        
        [SerializeField] private GameObject commandPostPrefab;
        [Min(0)] [SerializeField] private int workerPerCommand = 4;
        [Min(0)] [SerializeField] private int spawnedWorkersPerClick = 1;

        [Header("UI")] [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text workerInfoText;

        private readonly List<WorkerBehavior> weightedWorkerPrefabs = new List<WorkerBehavior>();
        private readonly List<WorkerBehavior> usedWorkerPrefabs = new List<WorkerBehavior>();

        private readonly List<Command> runningCommands = new List<Command>();
        
        private WorkerBO workerBO;
        public WorkerBO WorkerBO => workerBO;

        private void Start()
        {
            workerBO = new WorkerBO(workerPrefabs);

            var workersInScene = GameObject.FindGameObjectsWithTag("Worker");
            foreach (var workerGO in workersInScene)
            {
                var worker = workerGO.GetComponent<WorkerBehavior>();
                workerBO.AddNewWorker(worker);
            }

            foreach (var workerPrefab in workerPrefabs)
            {
                var count = Mathf.Pow(9, 3 - workerPrefab.Value.size);
                for (int i = 0; i < count; i++)
                {
                    weightedWorkerPrefabs.Add(workerPrefab.Value);
                }
            }

            slider.value = workerPerCommand;
            text.text = workerPerCommand.ToString();
            workerBO.Debug(workerInfoText);
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

            if (!UnityEngine.Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()),
                out var hit)) return;

            UnityEngine.Debug.Log("Left Click at " + hit.point);
            var transformPosition = hit.point;
            // transformPosition.y = 0.5f;
            for (int i = 0; i < spawnedWorkersPerClick; i++)
            {
                workerBO.InstantiateNewWorker(PickWorkerPrefab().size - 1, transformPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
            }

            workerBO.Debug(workerInfoText);
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;

            if (!UnityEngine.Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()),
                out var hit)) return;
            var planeNormal = hit.collider.transform.up;
            UnityEngine.Debug.Log("Right Click at " + hit.point + ". Up: " + planeNormal);

            var command = new Command(hit.point, commandPostPrefab, planeNormal);
            runningCommands.Add(command);

            var idleWorkers = workerBO.GetWorkersWithState(AbstractState.STATE.WANDER);
            for (int i = 0; i < workerPerCommand; i++)
            {
                if (idleWorkers.Count == 0) break;

                var worker = idleWorkers[Random.Range(0, idleWorkers.Count)];
                worker.QueueCommand(command);
                idleWorkers.Remove(worker);
                workerBO.GetWorkersWithState(AbstractState.STATE.COMMAND).Add(worker);
            }

            workerBO.Debug(workerInfoText);
        }

        public void OnSpaceKey(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed) return;

            var commandedWorkers = workerBO.GetWorkersWithState(AbstractState.STATE.COMMAND);
            foreach (var worker in commandedWorkers)
            {
                worker.QueueWandering();
                workerBO.GetWorkersWithState(AbstractState.STATE.WANDER).Add(worker);
            }

            foreach (var command in runningCommands)
            {
                command.End();
            }

            runningCommands.Clear();
            commandedWorkers.Clear();

            workerBO.Debug(workerInfoText);
        }

        public void OnWorkersPerCommandValueChange()
        {
            var value = Mathf.RoundToInt(slider.value);
            workerPerCommand = value;
            text.text = value.ToString();
        }
    }
}
