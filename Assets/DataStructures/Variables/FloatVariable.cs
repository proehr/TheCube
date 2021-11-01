using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Utils/Variables/Float Variable")]
    public class FloatVariable : AbstractVariable<float>
    {
        public void AddValue(float value)
        {
            runtimeValue += value;
            onValueChanged.Raise();
        }

        public void AddValue(FloatVariable value)
        {
            runtimeValue += value.runtimeValue;
            onValueChanged.Raise();
        }
    }
}
