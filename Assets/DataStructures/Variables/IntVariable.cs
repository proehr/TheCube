using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Utils/Variables/IntVariable")]
    public class IntVariable : AbstractVariable<int>
    {
        public void AddValue(int value)
        {
            runtimeValue += value;
            onValueChanged.Raise();
        }

        public void AddValue(IntVariable value)
        {
            runtimeValue += value.runtimeValue;
            onValueChanged.Raise();
        }
    }
}
