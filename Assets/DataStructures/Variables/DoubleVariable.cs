using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewDoubleVariable", menuName = "Utils/Variables/DoubleVariable")]
    public class DoubleVariable : AbstractVariable<double>
    {
        public void ApplyChange(double amount) => runtimeValue += amount;

        public void ApplyChange(DoubleVariable amount) => runtimeValue += amount.runtimeValue;
    }
}
