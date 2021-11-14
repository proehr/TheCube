using System;
using Features.WorkerAI.Scripts;
using UnityEngine;

namespace Features.Commands.Scripts.ActionEvents
{
    [CreateAssetMenu(fileName = "newCommandFinishedActionEvent", menuName = "Utils/ActionEvents/CommandFinished")]
    public class CommandFinishedActionEvent : ScriptableObject
    {
        Action<Command> listeners;

        public void Raise(Command caller)
        {
            this.listeners?.Invoke(caller);
        }

        public void RegisterListener(Action<Command> listener)
        {
            this.listeners += listener;
        }

        public void UnregisterListener(Action<Command> listener)
        {
            this.listeners -= listener;
        }
    }
}
