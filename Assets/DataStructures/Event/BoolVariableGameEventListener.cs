using DataStructures.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace DataStructures.Event
{
    public class BoolVariableGameEventListener : GameEventListener
    {
        [SerializeField] private BoolVariable boolVariable;
        [SerializeField] private UnityEvent responseFalse;

        public override void OnEventRaised()
        {
            if (this.boolVariable == null)
            {
                return;
            }

            if (this.boolVariable.Get())
            {
                base.OnEventRaised();
            }
            else
            {
                this.responseFalse?.Invoke();
            }
        }
    }
}
