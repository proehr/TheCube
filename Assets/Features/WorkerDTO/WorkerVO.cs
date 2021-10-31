using System;
using UnityEngine;

namespace Features.WorkerDTO
{
    [Serializable]
    public class WorkerVo
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public int workerSize;
        
        private Guid guid;

        public WorkerVo(Transform transform, int workerSize, Guid guid)
        {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;

            this.workerSize = workerSize;
            this.guid = guid;
        }

        public Guid Guid 
        {
            get => guid;
            set => guid = value;
        }
    }
}
