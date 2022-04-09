using System;
using UnityEngine;

namespace DataStructures.Event
{
    [CreateAssetMenu(fileName = "new ActionEvent", menuName = "Utils/Action Event")]
    public class ActionEvent : EventBase
    {
        private Action listeners;
    
        public override void Raise()
        {
            this.listeners?.Invoke();
        }

        public void RegisterListener(Action listener)
        {
            this.listeners += listener;
        }

        public void UnregisterListener(Action listener)
        {
            this.listeners -= listener;
        }
    }
}