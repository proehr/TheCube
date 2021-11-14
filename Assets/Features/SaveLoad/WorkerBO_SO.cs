using System;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using Features.WorkerAI.Scripts;
using Features.WorkerAI.Demo;
using Features.WorkerAI.Scripts.StateMachine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace Features.WorkerDTO
{
    [CreateAssetMenu(fileName = "WorkerBO", menuName = "WorkerBO")]
    public class WorkerBO_SO : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<int, WorkerBehavior> workerPrefabs;

        private readonly List<WorkerBehavior> workers = new List<WorkerBehavior>();

        private readonly Dictionary<AbstractState.STATE, List<WorkerBehavior>> workersPerState =
            new Dictionary<AbstractState.STATE, List<WorkerBehavior>>();

        // FIXME - no constructor for SO
        public WorkerBO_SO(Dictionary<int, WorkerBehavior> workerPrefabs)
        {
            this.workerPrefabs = workerPrefabs;

            foreach (AbstractState.STATE state in Enum.GetValues(typeof(AbstractState.STATE)))
            {
                workersPerState.Add(state, new List<WorkerBehavior>());
            }
        }

        public IEnumerable<WorkerBehavior> GetWorkersForCommand(int count)
        {
            var foundWorkers = new List<WorkerBehavior>(count);
            var idleWorkers = workersPerState[AbstractState.STATE.WANDER];
            for (int i = 0; i < count; i++)
            {
                if (idleWorkers.Count == 0) break;

                var worker = idleWorkers[Random.Range(0, idleWorkers.Count)];
                foundWorkers.Add(worker);
                idleWorkers.Remove(worker);
                workersPerState[AbstractState.STATE.COMMAND].Add(worker);
            }

            return foundWorkers;
        }

        public void UpdateWorkerState(WorkerBehavior worker, AbstractState.STATE newState)
        {
            workersPerState[worker.currentState.name].Remove(worker);
            workersPerState[newState].Add(worker);
        }

        public void AddNewWorker(WorkerBehavior workerBehaviour)
        {
            workers.Add(workerBehaviour);
            workersPerState[workerBehaviour.currentState.name].Add(workerBehaviour);
        }

        public void InstantiateNewWorker(int workerSizeKey, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (workerPrefabs.TryGetValue(workerSizeKey, out WorkerBehavior workerBehaviorPrefab))
            {
                InstantiateNewWorker(workerBehaviorPrefab, position, rotation, parent);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"The requested cube prefab doesnt exists in the {workerPrefabs}");
            }
        }

        public void InstantiateNewWorker(WorkerBehavior workerBehaviorPrefab, Vector3 position, Quaternion rotation,
            Transform parent)
        {
            WorkerBehavior workerBehaviour =
                UnityEngine.Object.Instantiate(workerBehaviorPrefab, position, rotation, parent);
            AddNewWorker(workerBehaviour);
        }

        public void DestroyWorker(WorkerBehavior worker)
        {
            if (workersPerState.TryGetValue(worker.currentState.name, out List<WorkerBehavior> workersByState))
            {
                workersByState.Remove(worker);
            }

            workers.Remove(worker);

            UnityEngine.Object.Destroy(worker.gameObject);
        }

        public void Debug(TMP_Text workerInfoText)
        {
            UnityEngine.Debug.Log("Workers: " + "ON THE JOB " + workersPerState[AbstractState.STATE.COMMAND].Count +
                                  " / IDLE " +
                                  workersPerState[AbstractState.STATE.WANDER].Count + " / TOTAL " + workers.Count);
            workerInfoText.text = "<align=\"left\"><u>Workers</u></align>" +
                                  "\nON THE JOB: " + workersPerState[AbstractState.STATE.COMMAND].Count +
                                  "\nIDLE: " + workersPerState[AbstractState.STATE.WANDER].Count +
                                  "\nTOTAL: " + workers.Count;
        }

        public void Save()
        {
            var workerVOs = workers.Select(worker => worker.GetWorkerVO()).ToList();
            SaveGame.Save("worker", workerVOs);
        }

        public void Load()
        {
            //Reset Instantiated Workers
            for (int i = workers.Count - 1; i >= 0; i--)
            {
                WorkerBehavior worker = workers[i];
                DestroyWorker(worker);
            }

            //Load them
            var parent = Object.FindObjectOfType<WorkerInputManager>().transform;
            foreach (WorkerVO workerVO in SaveGame.Load<List<WorkerVO>>("worker"))
            {
                InstantiateNewWorker(workerVO.workerSize, workerVO.position, workerVO.rotation, parent);
            }
        }
    }
}
