using System;
using UnityEngine;

namespace Features.WorkerDTO
{
    [Serializable]
    public class WorkerVO
    {
        public Vector3 position;
        public Quaternion rotation;
        public int workerSize;
        

        public WorkerVO(Transform transform, int workerSize)
        {
            position = transform.position;
            rotation = transform.rotation;

            this.workerSize = workerSize;
        }
    }
}
