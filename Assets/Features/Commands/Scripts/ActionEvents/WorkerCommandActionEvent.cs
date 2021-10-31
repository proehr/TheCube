using System;
using UnityEngine;

namespace Features.Commands.Scripts.ActionEvents
{
    [CreateAssetMenu(fileName = "newWorkerCommandActionEvent", menuName = "Utils/ActionEvents")]
    public class WorkerCommandActionEvent : ScriptableObject
    {
        Action<Cube> listeners;

        public void Raise(Cube caller)
        {
            this.listeners?.Invoke(caller);
        }

        public void RegisterListener(Action<Cube> listener)
        {
            this.listeners += listener;
        }

        public void UnregisterListener(Action<Cube> listener)
        {
            this.listeners -= listener;
        }
    }
}
