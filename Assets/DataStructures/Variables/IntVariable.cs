using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "new IntVariable", menuName = "Utils/Variables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int intValue;
        [SerializeField] private int startValue;

        public int GetVariableValue() => intValue;

        public void ResetVariable() => intValue = startValue;

        public void SetValue(int value) => intValue = value;

        public void SetValue(IntVariable value) => this.intValue = value.intValue;

        public void ApplyChange(int amount) => intValue += amount;

        public void ApplyChange(IntVariable amount) => intValue += amount.intValue;
    }
}
