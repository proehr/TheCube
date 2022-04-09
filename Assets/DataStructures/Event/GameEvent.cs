using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataStructures.Event
{
    [CreateAssetMenu(fileName = "new GameEvent", menuName = "Utils/Game Event")]
    public class GameEvent : EventBase
    {
        [ShowInInspector, ReadOnly] private List<GameEventListener> listeners = new List<GameEventListener>();

        public override void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}