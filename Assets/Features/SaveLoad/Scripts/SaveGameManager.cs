using Features.WorkerAI.Scripts;
using UnityEngine;

namespace Features.SaveLoad.Scripts
{
    public class SaveGameManager : MonoBehaviour
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
