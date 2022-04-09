using UnityEngine;

namespace Features.WorkerAI.Scripts
{
    public class WorkersParent : MonoBehaviour
    {
        [SerializeField] private WorkerService_SO workerService;

        private void Awake()
        {
            workerService.Init(this);
        }
    }
}