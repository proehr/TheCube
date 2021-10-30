using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "new DoubleVariable", menuName = "Utils/Variables/DoubleVariable")]
    public class DoubleVariable : ScriptableObject
    {
        public double doubleValue;
        [SerializeField] private double startValue;

        public double GetVariableValue() => doubleValue;

        public void ResetVariable() => doubleValue = startValue;

        public void SetValue(double value) => doubleValue = value;

        public void SetValue(DoubleVariable value) => doubleValue = value.doubleValue;
        
        public void ApplyChange(double amount) => doubleValue += amount;

        public void ApplyChange(DoubleVariable amount) => doubleValue += amount.doubleValue;
    }
}
