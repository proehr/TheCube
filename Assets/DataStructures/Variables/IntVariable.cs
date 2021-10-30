using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Utils/Variables/IntVariable")]
    public class IntVariable : AbstractVariable<int>
    {
        public void ApplyChange(int amount) => runtimeValue += amount;

        public void ApplyChange(IntVariable amount) => runtimeValue += amount.runtimeValue;
    }
}
