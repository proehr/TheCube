using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "new BoolVariable", menuName = "Utils/Variables/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool boolValue;
        [SerializeField] private bool startValue;

        public bool GetVariableValue() => boolValue;

        public void ResetVariable() => boolValue = startValue;

        public void SetValue(bool value) => boolValue = value;
        
        public void SetValue(BoolVariable value) => boolValue = value.boolValue;

        public void SetTrue() => boolValue = true;

        public void SetFalse() => boolValue = false;
    }
}