using Features.WorkerAI.Demo;
using UnityEngine;
using Utils.CrossSceneReference;

namespace Features.WorkerDTO
{
    public class SaveAndLoadWorkers : MonoBehaviour
    {
        [SerializeField] private GuidReference guidReference;
    
        public void Save()
        {
            guidReference.gameObject.GetComponent<WorkerInputManager>().WorkerBO.Save();
        }

        public void Load()
        {
            guidReference.gameObject.GetComponent<WorkerInputManager>().WorkerBO.Load();
        }
    }
}
