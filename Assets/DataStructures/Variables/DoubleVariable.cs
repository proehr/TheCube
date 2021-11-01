using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewDoubleVariable", menuName = "Utils/Variables/DoubleVariable")]
    public class DoubleVariable : AbstractVariable<double>
    {
        public void AddValue(double value)
        {
            runtimeValue += value;
            onValueChanged.Raise();
        }

        public void AddValue(DoubleVariable value)
        {
            runtimeValue += value.runtimeValue;
            onValueChanged.Raise();
        }
    }
}
