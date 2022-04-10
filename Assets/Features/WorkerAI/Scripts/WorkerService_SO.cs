using System;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using DataStructures.Variables;
using Features.WorkerAI.Scripts.StateMachine;
using Features.WorkerDTO;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.WorkerAI.Scripts
{
    [CreateAssetMenu(fileName = "WorkerService", menuName = "WorkerService")]
    public class WorkerService_SO : SerializedScriptableObject
    {
        [SerializeField] private IntVariable workerAmount;
        [SerializeField] private Dictionary<int, WorkerBehavior> workerPrefabs;

        private readonly List<WorkerBehavior> workers = new List<WorkerBehavior>();

        private readonly Dictionary<AbstractState.STATE, List<WorkerBehavior>> workersPerState =
            new Dictionary<AbstractState.STATE, List<WorkerBehavior>>();

        private Transform workersParent;

        public void Init(WorkersParent workersParent)
        {
            this.workersParent = workersParent.transform;
        }

        public void OnLevelStart()
        {
            foreach (AbstractState.STATE state in Enum.GetValues(typeof(AbstractState.STATE)))
            {
                if (!workersPerState.ContainsKey(state))
                {
                    workersPerState.Add(state, new List<WorkerBehavior>());
                }
            }
        }

        public void DestroyAllWorkers()
        {
            foreach (var worker in workers)
            {
                Destroy(worker.gameObject);
            }

            workers.Clear();
            foreach (var entry in workersPerState)
            {
                entry.Value.Clear();
            }
            UpdateWorkerAmount();
        }

        public ICollection<WorkerBehavior> GetWorkersForCommand(int count)
        {
            return GetWorkersForCommand(count, new List<WorkerBehavior>(count));
        }

        public ICollection<WorkerBehavior> GetWorkersForCommand(int count, ICollection<WorkerBehavior> workersForCommand)
        {
            var idleWorkers = workersPerState[AbstractState.STATE.WANDER];
            for (int i = 0; i < count; i++)
            {
                if (idleWorkers.Count == 0) break;

                var worker = idleWorkers[Random.Range(0, idleWorkers.Count)];
                workersForCommand.Add(worker);
                idleWorkers.Remove(worker);
                workersPerState[AbstractState.STATE.COMMAND].Add(worker);
            }

            return workersForCommand;
        }

        public void UpdateWorkerState(WorkerBehavior worker, AbstractState.STATE newState)
        {
            workersPerState[worker.currentState.name].Remove(worker);
            workersPerState[newState].Add(worker);
        }

        private void UpdateWorkerAmount()
        {
            this.workerAmount.Set(this.workers.Count);
        }

        public void AddNewWorker(WorkerBehavior workerBehaviour)
        {
            workers.Add(workerBehaviour);
            workersPerState[workerBehaviour.currentState.name].Add(workerBehaviour);
            UpdateWorkerAmount();
        }

        private void InstantiateNewWorker(int workerSizeKey, Vector3 position, Quaternion rotation)
        {
            if (workerPrefabs.TryGetValue(workerSizeKey, out WorkerBehavior workerBehaviorPrefab))
            {
                InstantiateNewWorker(workerBehaviorPrefab, position, rotation);
            }
            else
            {
                Debug.LogWarning($"The requested cube prefab doesnt exists in the {workerPrefabs}");
            }
        }

        public void InstantiateNewWorker(WorkerBehavior workerBehaviorPrefab, Vector3 position, Quaternion rotation)
        {
            WorkerBehavior workerBehaviour = Instantiate(workerBehaviorPrefab, position, rotation, workersParent);
            AddNewWorker(workerBehaviour);
        }

        public void DestroyWorker(WorkerBehavior worker)
        {
            if (workersPerState.TryGetValue(worker.currentState.name, out List<WorkerBehavior> workersByState))
            {
                workersByState.Remove(worker);
            }

            workers.Remove(worker);

            Destroy(worker.gameObject);
            UpdateWorkerAmount();
        }

        public void Save()
        {
            var workerVOs = workers.Select(worker => worker.GetWorkerVO()).ToList();
            SaveGame.Save("worker", workerVOs);
        }

        public void Load()
        {
            //Reset Instantiated Workers
            DestroyAllWorkers();

            //Load them
            foreach (WorkerVO workerVO in SaveGame.Load<List<WorkerVO>>("worker"))
            {
                InstantiateNewWorker(workerVO.workerSize, workerVO.position, workerVO.rotation);
            }
        }
    }
}
