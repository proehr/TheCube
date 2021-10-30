using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewBoolVariable", menuName = "Utils/Variables/BoolVariable")]
    public class BoolVariable : AbstractVariable<bool>
    {
        public void SetTrue() => runtimeValue = true;

        public void SetFalse() => runtimeValue = false;
    }
}