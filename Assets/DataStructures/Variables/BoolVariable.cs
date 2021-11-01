using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewBoolVariable", menuName = "Utils/Variables/BoolVariable")]
    public class BoolVariable : AbstractVariable<bool>
    {
        public void SetTrue()
        {
            runtimeValue = true;
            onValueChanged.Raise();
        }

        public void SetFalse()
        {
            runtimeValue = false;
            onValueChanged.Raise();
        }
    }
}