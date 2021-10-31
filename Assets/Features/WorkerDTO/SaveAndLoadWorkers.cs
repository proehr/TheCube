using Features.WorkerAI.Demo;
using Features.WorkerDTO;
using UnityEngine;
using Utils.CrossSceneReference;

public class SaveAndLoadWorkers : MonoBehaviour
{
    [SerializeField] private GuidReference guidReference;

    private WorkerBo workerBo;
    
    private void Start()
    {
        workerBo = guidReference.gameObject.GetComponent<WorkerInputManager>().WorkerBo;
    }

    public void Save()
    {
        workerBo.Save();
    }

    public void Load()
    {
        workerBo.Load();
    }
}
