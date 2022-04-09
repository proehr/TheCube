using UnityEngine;

namespace DataStructures.Event
{
    public abstract class EventBase : ScriptableObject
    {
        public abstract void Raise();
    }
}
