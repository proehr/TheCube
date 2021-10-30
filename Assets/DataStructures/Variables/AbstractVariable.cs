using Sirenix.OdinInspector;
using UnityEngine;
using Utils.Event;

namespace Utils.Variables
{
    public abstract class AbstractVariable<T> : ScriptableObject
    {
        [ShowInInspector, ReadOnly] protected T runtimeValue;
        [SerializeField] private T storedValue;
        [SerializeField] private GameEvent onValueChanged;

        private void OnEnable()
        {
            Restore();
        }

        public void Restore() => runtimeValue = storedValue;

        public T Get() => runtimeValue;

        public void Set(T value)
        {
            runtimeValue = value;
            if(onValueChanged != null) onValueChanged.Raise();
        }

        public void Copy(AbstractVariable<T> other) => runtimeValue = other.runtimeValue;
    }
}
