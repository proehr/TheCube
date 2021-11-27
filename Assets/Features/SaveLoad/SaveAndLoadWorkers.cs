using Features.WorkerAI.Scripts;
using UnityEngine;

namespace Features.SaveLoad
{
    public class SaveAndLoadWorkers : MonoBehaviour
    {
        [SerializeField] private WorkerService_SO workerService;
    
        public void Save()
        {
            workerService.Save();
        }

        public void Load()
        {
            workerService.Load();
        }
    }
}
