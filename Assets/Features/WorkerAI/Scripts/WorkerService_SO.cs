using System;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using Features.WorkerAI.Scripts.StateMachine;
using Features.WorkerDTO;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.WorkerAI.Scripts
{
    [CreateAssetMenu(fileName = "WorkerBO", menuName = "WorkerBO")]
    public class WorkerService_SO : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<int, WorkerBehavior> workerPrefabs;

        private readonly List<WorkerBehavior> workers = new List<WorkerBehavior>();

        private readonly Dictionary<AbstractState.STATE, List<WorkerBehavior>> workersPerState =
            new Dictionary<AbstractState.STATE, List<WorkerBehavior>>();

        private Transform cachedWorkersParent = null;
        private bool cachedWorkersParentLoaded = false;
        private Transform workersParent
        {
            get
            {
                if (cachedWorkersParent == null && cachedWorkersParentLoaded == false)
                {
                    var foundWorkersParent = GameObject.FindGameObjectWithTag("WorkersParent");
                    if (foundWorkersParent == null)
                    {
                        UnityEngine.Debug.Assert(true, "Missing a game object tagged 'WorkersParent' in the scene!");
                    }
                    else
                    {
                        cachedWorkersParent = foundWorkersParent.transform;
                    }

                    cachedWorkersParentLoaded = true;
                }

                return cachedWorkersParent;
            }
        }

        private void OnEnable()
        {
            foreach (AbstractState.STATE state in Enum.GetValues(typeof(AbstractState.STATE)))
            {
                if (!workersPerState.ContainsKey(state))
                {
                    workersPerState.Add(state, new List<WorkerBehavior>());
                }
            }
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

        public void AddNewWorker(WorkerBehavior workerBehaviour)
        {
            workers.Add(workerBehaviour);
            workersPerState[workerBehaviour.currentState.name].Add(workerBehaviour);
        }

        private void InstantiateNewWorker(int workerSizeKey, Vector3 position, Quaternion rotation)
        {
            if (workerPrefabs.TryGetValue(workerSizeKey, out WorkerBehavior workerBehaviorPrefab))
            {
                InstantiateNewWorker(workerBehaviorPrefab, position, rotation);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"The requested cube prefab doesnt exists in the {workerPrefabs}");
            }
        }

        public void InstantiateNewWorker(WorkerBehavior workerBehaviorPrefab, Vector3 position, Quaternion rotation)
        {
            WorkerBehavior workerBehaviour =
                UnityEngine.Object.Instantiate(workerBehaviorPrefab, position, rotation, workersParent);
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
            foreach (WorkerVO workerVO in SaveGame.Load<List<WorkerVO>>("worker"))
            {
                InstantiateNewWorker(workerVO.workerSize, workerVO.position, workerVO.rotation);
            }
        }
    }
}
