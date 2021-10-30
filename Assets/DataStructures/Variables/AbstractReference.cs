using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils.Variables
{
    [Serializable]
    public abstract class AbstractReference<T>
    {
        [ShowInInspector, ReadOnly] private bool useOverride;
        [ShowInInspector, ReadOnly] private T overrideValue;
        [SerializeField] private AbstractVariable<T> variable;

        protected AbstractReference(T value)
        {
            useOverride = true;
            overrideValue = value;
        }

        public void SetOverride(T value)
        {
            useOverride = true;
            overrideValue = value;
        }

        public void ResetOverride()
        {
            useOverride = false;
        }
        
        protected T value => useOverride ? overrideValue : variable.Get();

        public static implicit operator T(AbstractReference<T> reference)
        {
            return reference.value;
        }
    }
}