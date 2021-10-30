using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = "Utils/Variables/StringVariable")]
    public class StringVariable : AbstractVariable<string>
    {
        public void ApplyChange(string amount) => runtimeValue += amount;

        public void ApplyChange(StringVariable amount) => runtimeValue += amount.runtimeValue;
    }
}
