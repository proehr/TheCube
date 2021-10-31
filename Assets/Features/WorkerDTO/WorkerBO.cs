using System;
using System.Collections.Generic;
using System.Linq;
using BayatGames.SaveGameFree;
using Features.WorkerAI;
using Features.WorkerAI.StateMachine;
using TMPro;
using UnityEngine;

namespace Features.WorkerDTO
{
    public class WorkerBo
    {
        private readonly Dictionary<int, WorkerBehavior> workerPrefabs;
        
        private readonly List<WorkerBehavior> workers = new List<WorkerBehavior>();
        private readonly Dictionary<AbstractState.STATE, List<WorkerBehavior>> workersPerState =
            new Dictionary<AbstractState.STATE, List<WorkerBehavior>>();
        
        public WorkerBo(Dictionary<int, WorkerBehavior> workerPrefabs)
        {
            this.workerPrefabs = workerPrefabs;
            
            foreach (AbstractState.STATE state in Enum.GetValues(typeof(AbstractState.STATE)))
            {
                workersPerState.Add(state, new List<WorkerBehavior>());
            }
        }

        public Dictionary<AbstractState.STATE, List<WorkerBehavior>> WorkersPerState => workersPerState;
        
        public void AddNewWorker(WorkerBehavior workerBehaviour)
        {
            workers.Add(workerBehaviour);
            workersPerState[workerBehaviour.currentState.name].Add(workerBehaviour);
        }

        public void InstantiateNewWorker(int workerSizeKey, Vector3 position, Quaternion quaternion)
        {
            if (workerPrefabs.TryGetValue(workerSizeKey, out WorkerBehavior workerBehaviorPrefab))
            {
                WorkerBehavior workerBehaviour = UnityEngine.Object.Instantiate(workerBehaviorPrefab, position, quaternion);
                AddNewWorker(workerBehaviour);
            }
            else
            {
                UnityEngine.Debug.Log($"The requested cube prefab doesnt exists in the {workerPrefabs}");
            }
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
            var workerVos = workers.Select(worker => worker.GetWorkerVo()).ToList();
            SaveGame.Save("worker", workerVos);
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
            foreach (WorkerVo workerVo in SaveGame.Load<List<WorkerVo>>("worker"))
            {
                InstantiateNewWorker(workerVo.workerSize, workerVo.position, workerVo.rotation);
            }
        }
    }
}
