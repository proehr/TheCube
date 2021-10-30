using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Utils/Variables/Float Variable")]
    public class FloatVariable : AbstractVariable<float>
    {
        public void ApplyChange(float amount) => runtimeValue += amount;

        public void ApplyChange(FloatVariable amount) => runtimeValue += amount.runtimeValue;
    }
}
