using UnityEngine;
using UnityEngine.Events;

namespace DataStructures.Event
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent @event;
        public UnityEvent Response;

        private void OnEnable()
        {
            @event.RegisterListener(this);
        }

        private void OnDisable()
        {
            @event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
