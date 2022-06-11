using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Utils/Variables/IntVariable")]
    public class IntVariable : AbstractVariable<int>
    {
        public virtual void Add(int value)
        {
            runtimeValue += value;
            if (onValueChanged != null) onValueChanged.Raise();
        }

        public virtual void Add(IntVariable value)
        {
            runtimeValue += value.runtimeValue;
            if (onValueChanged != null) onValueChanged.Raise();
        }
    }
}
