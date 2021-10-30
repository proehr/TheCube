using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = "Utils/Variables/StringVariable")]
    public class StringVariable : AbstractVariable<string>
    {
        public void Append(string value) => runtimeValue += value;

        public void Append(StringVariable value) => runtimeValue += value.runtimeValue;
    }
}
