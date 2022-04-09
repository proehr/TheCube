using UnityEngine;

namespace Features.Commands.Scripts
{
    public struct SelectInfo
    {
        public bool Excavate { get; }
        public RaycastHit Hit { get; }

        public SelectInfo(bool excavationMode, RaycastHit hit)
        {
            this.Excavate = excavationMode;
            this.Hit = hit;
        }
        
    }
}