using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "new StringVariable", menuName = "Utils/Variables/StringVariable")]
    public class StringVariable : ScriptableObject
    {
        public string stringValue;
        [SerializeField] private string startValue;

        public string GetVariableValue() => stringValue;

        public void ResetVariable() => stringValue = startValue;
        
        public void SetValue(string value) => stringValue = value;

        public void SetValue(StringVariable value) => stringValue = value.stringValue;

        public void ApplyChange(string amount) => stringValue += amount;

        public void ApplyChange(StringVariable amount) => stringValue += amount.stringValue;
    }
}
